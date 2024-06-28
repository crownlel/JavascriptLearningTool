using JavascriptLearningTool.Models;
using JavascriptLearningTool.Repositories;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JavascriptLearningTool.Helpers
{
    public class UserAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
        private IHttpContextAccessor _httpContextAccessor;

        public UserAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = _httpContextAccessor.GetAuthToken();
                if (string.IsNullOrEmpty(token))
                {
                    return Task.FromResult(new AuthenticationState(_anonymous));
                }

                var userClaims = DecryptToken(token);
                if (userClaims == null) return Task.FromResult(new AuthenticationState(_anonymous));

                var claimsPrincipal = SetClaimsPrincipal(userClaims);
                return Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        private User DecryptToken(string jWTToken)
        {
            if (string.IsNullOrEmpty(jWTToken)) return new User { Username = string.Empty, Password = string.Empty };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jWTToken);

            return new User
            {
                Username = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? string.Empty,
                Password = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Hash)?.Value ?? string.Empty,
            };
        }

        private ClaimsPrincipal SetClaimsPrincipal(User userClaims)
        {
            if (string.IsNullOrEmpty(userClaims.Username)) return new ClaimsPrincipal();

            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new (ClaimTypes.Name, userClaims.Username),
                    new (ClaimTypes.Hash, userClaims.Password),
                }, "JwtAuth"));
        }

        public void NotifyChanged(string token)
        {
            var principal = new ClaimsPrincipal();
            if (!string.IsNullOrEmpty(token))
            {
                _httpContextAccessor.SetAuthToken(token);
                var userClaims = DecryptToken(token);
                principal = SetClaimsPrincipal(userClaims);
            }
            else
            {
                _httpContextAccessor.SetAuthToken(null);
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }
    }
}
