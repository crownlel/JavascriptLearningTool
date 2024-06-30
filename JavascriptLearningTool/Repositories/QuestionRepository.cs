using Dapper;
using JavascriptLearningTool.Components.Pages;
using JavascriptLearningTool.Helpers;
using JavascriptLearningTool.Models;

namespace JavascriptLearningTool.Repositories
{
    public class QuestionRepository(DbConnectionFactory connectionFactory) : BaseRepository(connectionFactory)
    {
        public async Task<IEnumerable<Question>> GetTestQuestionsAsync(int courseId, int totalQuestions)
        {
            using var connection = _connectionFactory.OpenConnection();

            return await connection.QueryAsync<Question>($"select top {totalQuestions} * from Questions where CourseId = @CourseId order by NEWID()",
                new { CourseId = courseId });
        }

        public async Task<IEnumerable<Question>> GetQuestionsComprehensiveAsync(int totalQuestions)
        {
            using var connection = _connectionFactory.OpenConnection();

            return await connection.QueryAsync<Question>($"select top {totalQuestions} * from Questions order by NEWID()");
        }
    }
}
