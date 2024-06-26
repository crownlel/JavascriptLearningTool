using JavascriptLearningTool.Models;
using JavascriptLearningTool.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JavascriptLearningTool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly JwtCredentials _jwtCredentials;
        private readonly UserService _userService;

        public AuthenticationController(JwtCredentials jwtCredentials, UserService userService)
        {
            _jwtCredentials = jwtCredentials;
            _userService = userService;
        }

        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> CreateToken(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest();
            }

            var authenticatedUser = await _userService.LoginAsync(user.Username, user.Password);
            if (authenticatedUser != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtCredentials.Key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, authenticatedUser.Username),
                    new Claim(ClaimTypes.Hash, authenticatedUser.Password)
                };
                var token = new JwtSecurityToken(_jwtCredentials.Issuer, _jwtCredentials.Audience, claims,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: credentials);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(tokenString);
            }
            return Unauthorized();
        }
    }
}
