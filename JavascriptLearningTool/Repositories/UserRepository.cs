using Dapper;
using JavascriptLearningTool.Models;
using System.Data;

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
            _dbConnection.Open();
            var result = await _dbConnection.QueryAsync("SELECT * FROM users");
            if (username == "admin" && password == "password")
            {
                return new User
                {
                    Username = username,
                    Password = password
                };
            }
            return null;
        }
    }
}
