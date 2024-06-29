using Dapper;
using JavascriptLearningTool.Helpers;
using JavascriptLearningTool.Models;

namespace JavascriptLearningTool.Repositories
{
    public class TestRepository(DbConnectionFactory connectionFactory) : BaseRepository(connectionFactory)
    {
        public async Task<IEnumerable<Test>> GetTestsAsync()
        {
            using var connection = _connectionFactory.OpenConnection();

            return await connection.QueryAsync<Test>("select Id as 'CourseId', Name as 'Title', 5 as 'Duration' from courses");
        }
    }
}
