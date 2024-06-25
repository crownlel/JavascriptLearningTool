using JavascriptLearningTool.Models;
using JavascriptLearningTool.Repositories;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace JavascriptLearningTool.Services
{
    public class UserAuthenticationStateProvider : AuthenticationStateProvider
    {

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = UserService.User;
            return new(user);
        }

        public void NotifyChanged(User? user)
        {
            UserService.User = user != null ? user.ToClaimsPrincipal() : new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
