using Dapper;
using JavascriptLearningTool.Helpers;
using JavascriptLearningTool.Models;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace JavascriptLearningTool.Repositories
{
    public class UserRepository : BaseRepository
    {

        public UserRepository(DbConnectionFactory conFactory) : base(conFactory)
        {
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            using var connection = _connectionFactory.OpenConnection();
            var sql = "SELECT * FROM Users WHERE Username = @Username";
            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Username = username });
        }
    }
}
