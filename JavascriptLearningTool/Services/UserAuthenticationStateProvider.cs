using JavascriptLearningTool.Repositories;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace JavascriptLearningTool.Services
{
    public class UserAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly UserService _userService;
        private readonly UserRepository _userRepository;

        public UserAuthenticationStateProvider(UserService userService, UserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = _userService.User;
            return new(user);
        }

        public async Task LoginAsync(string username, string password)
        {
            var principal = new ClaimsPrincipal();
            var user = await _userRepository.GetUser(username, password);
            if (user != null)
            {
                principal = user.ToClaimsPrincipal();
                _userService.User = principal;
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
            }
        }

        public async Task LogoutAsync()
        {
            var principal = new ClaimsPrincipal();
            _userService.User = principal;
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }
    }
}
