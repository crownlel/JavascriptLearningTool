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
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (HashedPasswordManager.VerifyHashedPassword(user?.Password, password))
            {
                user.Password = HashedPasswordManager.HashPassword(password);
                return user;
            }

            return null;
        }
    }
}
