using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace JavascriptLearningTool.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public string Password { get; set; }

        public ClaimsPrincipal ToClaimsPrincipal() => new(new ClaimsIdentity(
        [
            new (ClaimTypes.Name, Username),
            new (ClaimTypes.Hash, Password),
            //new (ClaimTypes.Role, "user")
        ], "apiauth_type"));

        public static User FromClaimsPrincipal(ClaimsPrincipal principal) => new()
        {
            Username = principal.FindFirstValue(ClaimTypes.Name),
            //Password = principal.FindFirstValue(ClaimTypes.Hash)
        };
    }
}
