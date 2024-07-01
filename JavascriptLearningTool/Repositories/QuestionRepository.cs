using Dapper;
using JavascriptLearningTool.Components.Pages;
using JavascriptLearningTool.Helpers;
using JavascriptLearningTool.Models;
using System.Text;

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

        public async Task<IEnumerable<QuestionProgress>> GetQuestionProgressAsync(int userId)
        {
            var sql = @"select 
                        q.Id, 
                        q.CourseId,
                        COUNT(a.Id) as 'TotalAnswers', 
                        COUNT(case when a.SelectedOption = q.CorrectOption then 1 else null end) as 'CorrectAnswers'  
                        from Questions q
                        left join Answers a on a.QuestionId = q.Id and (a.UserId = @UserId or a.UserId is null)
                        group by q.CourseId, q.Id;";
            using var connection = _connectionFactory.OpenConnection();
            return await connection.QueryAsync<QuestionProgress>(sql, new { UserId = userId });
        }

        public async Task<IEnumerable<Question>> GetQuestionsWeightedAsync(int userId, int totalQuestions, int? courseId = null)
        {
            var stringBuilder = new StringBuilder(
                        @"select 
                        q.Id, 
                        q.CourseId,
                        COUNT(a.Id) as 'TotalAnswers', 
                        COUNT(case when a.SelectedOption = q.CorrectOption then 1 else null end) as 'CorrectAnswers'  
                        from Questions q
                        left join Answers a on a.QuestionId = q.Id and (a.UserId = @UserId or a.UserId is null)");
            if (courseId.HasValue)
            {
                stringBuilder.Append(" where q.CourseId = @CourseId");
            }
            stringBuilder.Append(" group by q.CourseId, q.Id;");

            using var connection = _connectionFactory.OpenConnection();

            var questionProgress = await connection.QueryAsync<QuestionProgress>(stringBuilder.ToString(), new { UserId = userId, CourseId = courseId });
            var notCompletedQuestions = questionProgress.Where(qp => !qp.IsCompleted).Select(q => q.Id).ToList();

            // Generate a weighted random question id list
            var questionIdsPool = questionProgress.Select(q => q.Id).ToList();
            questionIdsPool.AddRange(notCompletedQuestions);

            var random = new Random();
            var weightedQuestionIds = new List<int>();
            for (int i = 0; i < totalQuestions; i++)
            {
                // Assign random number to each question id in order to pick a random
                var randoms = questionIdsPool.Select(i => new { RandomId = random.Next(), QuestionId = i });
                var id = randoms.OrderBy(x => x.RandomId).First().QuestionId;
                questionIdsPool.RemoveAll(q => q == id);
                weightedQuestionIds.Add(id);
            }

            return await connection.QueryAsync<Question>($"select * from Questions where Id in @Ids", new { Ids = weightedQuestionIds });
        }
    }
}
