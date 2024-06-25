using Dapper;
using JavascriptLearningTool.Helpers;
using JavascriptLearningTool.Models;
using JavascriptLearningTool.Services;
using System.Data;
using System.Reflection;

namespace JavascriptLearningTool.Repositories
{
    public class UserRepository : BaseRepository
    {

        public UserRepository(DBConnectionFactory conFactory) : base(conFactory)
        {
        }

        public async Task<User?> GetUser(string username, string password)
        {
            using var con = _connectionFactory.OpenConnection();

            var user = await con.QueryFirstOrDefaultAsync<User>("SELECT * FROM users WHERE username = @Username",
                new { Username = username });

            if (HashedPasswordManager.VerifyHashedPassword(user?.Password, password))
            {
                return user;
            }

            return null;
        }
    }
}
