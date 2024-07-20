using Dapper;
using JavascriptLearningTool.Helpers;
using JavascriptLearningTool.Models;

namespace JavascriptLearningTool.Repositories
{
    public class CourseRepository : BaseRepository
    {
        public CourseRepository(DbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<IEnumerable<Course>> GetAllUserCoursesAsync(int userId)
        {
            using var connection = _connectionFactory.OpenConnection();
            var sql = @"SELECT c.Id, c.Name, c.Description, 
                        ISNULL(up.LastPage, 0)  as CurrentPage, c.Pages FROM Courses c
                        left join UserProgresses up ON c.Id = up.CourseId and up.UserId = @UserId";
            return await connection.QueryAsync<Course>(sql, new {UserId = userId});
        }

        public async Task<Course?> GetUserCourseAsync(int courseId, int userId)
        {
            using var connection = _connectionFactory.OpenConnection();
            var sql = @"SELECT c.Id, c.Name, c.Description, 
                        ISNULL(up.LastPage, 0)  as CurrentPage, c.Pages FROM Courses c
                        left join UserProgresses up ON c.Id = up.CourseId and up.UserId = @UserId
                        where c.Id = @CourseId";
            return await connection.QueryFirstOrDefaultAsync<Course>(sql, new { UserId = userId, CourseId = courseId });
        }
    }
}
