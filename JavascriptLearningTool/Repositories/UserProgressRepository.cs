using Dapper;
using JavascriptLearningTool.Helpers;
using JavascriptLearningTool.Models;
using System.Data;

namespace JavascriptLearningTool.Repositories
{
    public class UserProgressRepository : BaseRepository
    {

        public UserProgressRepository(DbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<UserProgress?> GetUserProgressAsync(int userId, int courseId)
        {
            using var connection = _connectionFactory.OpenConnection();
            return await connection.QuerySingleOrDefaultAsync<UserProgress>(
                "SELECT * FROM UserProgresses WHERE UserId = @UserId AND CourseId = @CourseId",
                new { UserId = userId, CourseId = courseId });
        }

        public async Task<IEnumerable<UserProgress>> GetAllUserProgressesAsync()
        {
            using var connection = _connectionFactory.OpenConnection();
            return await connection.QueryAsync<UserProgress>("SELECT * FROM UserProgresses");
        }

        public async Task AddUserProgressAsync(UserProgress progress)
        {
            using var connection = _connectionFactory.OpenConnection();
            var sql = "INSERT INTO UserProgresses (UserId, CourseId, LastPage, LastUpdated) VALUES (@UserId, @CourseId, @LastPage, @LastUpdated)";
            await connection.ExecuteAsync(sql, new
            {
                progress.UserId,
                progress.CourseId,
                progress.LastPage,
                progress.LastUpdated
            });
        }

        public async Task UpdateUserProgressAsync(UserProgress progress)
        {
            using var connection = _connectionFactory.OpenConnection();
            var sql = "UPDATE UserProgresses SET LastPage = @LastPage, LastUpdated = @LastUpdated WHERE UserId = @UserId AND CourseId = @CourseId";
            await connection.ExecuteAsync(sql, new
            {
                progress.UserId,
                progress.CourseId,
                progress.LastPage,
                progress.LastUpdated
            });
        }

        public async Task DeleteUserProgressAsync(int userId, int courseId)
        {
            using var connection = _connectionFactory.OpenConnection();
            var sql = "DELETE FROM UserProgresses WHERE UserId = @UserId AND CourseId = @CourseId";
            await connection.ExecuteAsync(sql, new { UserId = userId, CourseId = courseId });
        }

        public async Task AddPageActivity(int userId, int courseId, int pageId, int secondsSpentOnPage)
        {
            var newPageActivity = new PageActivity
            {
                UserId = userId,
                CourseId = courseId,
                PageId = pageId,
                SecondsSpent = secondsSpentOnPage,
                Timestamp = DateTime.Now
            };
            var sql = "INSERT INTO PageActivities (UserId, CourseId, PageId, SecondsSpent, Timestamp) VALUES (@UserId, @CourseId, @PageId, @SecondsSpent, @Timestamp)";
            using var connection = _connectionFactory.OpenConnection();
            await connection.ExecuteAsync(sql, new
            {
                newPageActivity.UserId,
                newPageActivity.CourseId,
                newPageActivity.PageId,
                newPageActivity.SecondsSpent,
                newPageActivity.Timestamp
            });
        }

        public async Task SaveUserProgressAsync(int userId, int courseId, int pageId, int secondsSpentOnPage)
        {
            var userProgress = await GetUserProgressAsync(userId, courseId);

            // track time spent on page
            if (secondsSpentOnPage > 3)
            {
                await AddPageActivity(userId, courseId, userProgress!.LastPage, secondsSpentOnPage);
            }

            userProgress.LastPage = pageId;
            userProgress.LastUpdated = DateTime.Now;
            await UpdateUserProgressAsync(userProgress);

        }

        public async Task<IEnumerable<PageActivity>> GetAllUserPageStatsAsync(int userId)
        {
            var connection = _connectionFactory.OpenConnection();
            var sql = @"select * from PageActivities where UserId = @UserId";
            return await connection.QueryAsync<PageActivity>(sql, new { UserId = userId });
        }

    }

}
