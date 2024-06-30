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

            return await connection.QueryAsync<Test>($"select Id as 'CourseId', Name as 'Title', {Constants.CourseTestDuration} as 'Duration' from courses");
        }

        public async Task<Test?> GetTestAsync(int courseId)
        {
            using var connection = _connectionFactory.OpenConnection();

            return await connection.QueryFirstOrDefaultAsync<Test>($"select Id as 'CourseId', Name as 'Title', {Constants.CourseTestDuration} as 'Duration' from courses where Id = @CourseId",
                new { CourseId = courseId });
        }
    }
}
