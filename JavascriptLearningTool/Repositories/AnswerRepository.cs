using Dapper;
using JavascriptLearningTool.Helpers;
using JavascriptLearningTool.Models;

namespace JavascriptLearningTool.Repositories
{
    public class AnswerRepository(DbConnectionFactory connectionFactory) : BaseRepository(connectionFactory)
    {

        public async Task SaveUserAnswersAsync(int userId, IEnumerable<Answer> answers)
        {
            using var connection = _connectionFactory.OpenConnection();
            foreach (var answer in answers)
            {
                answer.UserId = userId;
            }
            var sql = "INSERT INTO Answers (UserId, QuestionId, SelectedOption, SubmittedAt) VALUES (@UserId, @QuestionId, @SelectedOption, @SubmittedAt)";
            await connection.ExecuteAsync(sql, answers);
        }
    }
}
