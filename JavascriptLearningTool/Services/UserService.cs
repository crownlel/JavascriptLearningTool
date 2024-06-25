using JavascriptLearningTool.Helpers;
using JavascriptLearningTool.Models;
using JavascriptLearningTool.Repositories;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Connections;
using System.Security.Claims;

namespace JavascriptLearningTool.Services
{
    public class UserService
    {
        private static ClaimsPrincipal _currentUser = new(new ClaimsIdentity());

        public static bool IsLoggedIn => _currentUser.Identity?.IsAuthenticated ?? false;

        public static ClaimsPrincipal User
        {
            get => _currentUser;
            set
            {
                if (_currentUser != value)
                {
                    _currentUser = value;
                }
            }
        }

        private readonly UserRepository _userRepository;
        private readonly UserAuthenticationStateProvider _authenticationStateProvider;

        public UserService(UserRepository userRepository , UserAuthenticationStateProvider authenticationStateProvider)
        {
            _userRepository = userRepository;
            _authenticationStateProvider = authenticationStateProvider;
        }


        public async Task<User?> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (HashedPasswordManager.VerifyHashedPassword(user?.Password, password))
            {
                _authenticationStateProvider.NotifyChanged(user);
                return user;
            }

            return null;
        }

        public void Logout()
        {
            _authenticationStateProvider.NotifyChanged(null);
        }
    }
}
