using Dapper;
using JavascriptLearningTool.Helpers;
using JavascriptLearningTool.Models;
using System.Data;
using System.Reflection;

namespace JavascriptLearningTool.Repositories
{
    public class UserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<User?> GetUser(string username, string password)
        {
            try
            {
                _dbConnection.Open();
                var hashedPassword = HashedPasswordManager.HashPassword(password);
                var user = await _dbConnection.QueryFirstOrDefaultAsync<User>("SELECT * FROM users WHERE username = @Username",
                    new { Username = username });

                if (user != null && HashedPasswordManager.VerifyHashedPassword(user.Password, password))
                {
                    return user;
                }

                return null;
            }
            finally
            {
                _dbConnection.Close();
            }
        }
    }
}
