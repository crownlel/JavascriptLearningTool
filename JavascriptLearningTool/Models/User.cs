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
    }
}
