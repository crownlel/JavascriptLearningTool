using Dapper;
using JavascriptLearningTool.Helpers;
using JavascriptLearningTool.Models;
using JavascriptLearningTool.Services;
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

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var connection = _connectionFactory.OpenConnection();
            var sql = "SELECT * FROM Users WHERE Id = @Id";
            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            var connection = _connectionFactory.OpenConnection();
            var sql = "SELECT * FROM Users WHERE Username = @Username";
            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Username = username });
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var connection = _connectionFactory.OpenConnection();
            var sql = "SELECT * FROM Users";
            return await connection.QueryAsync<User>(sql);
        }

        public async Task AddUserAsync(User user)
        {
            var connection = _connectionFactory.OpenConnection();
            var sql = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
            await connection.ExecuteAsync(sql, user);
        }

        public async Task UpdateUserAsync(User user)
        {
            var connection = _connectionFactory.OpenConnection();
            var sql = "UPDATE Users SET Username = @Username, Password = @Password WHERE Id = @Id";
            await connection.ExecuteAsync(sql, user);
        }

        public async Task DeleteUserAsync(int id)
        {
            var connection = _connectionFactory.OpenConnection();
            var sql = "DELETE FROM Users WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
