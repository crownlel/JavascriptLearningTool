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

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            var connection = _connectionFactory.OpenConnection();
            return await connection.QuerySingleOrDefaultAsync<Course>("SELECT * FROM Courses WHERE Id = @Id", new { Id = id });
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            var connection = _connectionFactory.OpenConnection();
            return await connection.QueryAsync<Course>("SELECT * FROM Courses");
        }

        public async Task<IEnumerable<Course>> GetAllUserCoursesAsync(int userId)
        {
            var connection = _connectionFactory.OpenConnection();
            var sql = @"SELECT c.Id, c.Name, c.Description, 
                        ISNULL(up.LastPage, 0)  as CurrentPage, c.Pages FROM Courses c
                        left join UserProgresses up ON c.Id = up.CourseId and up.UserId = @UserId";
            return await connection.QueryAsync<Course>(sql, new {UserId = userId});
        }

        public async Task AddCourseAsync(Course course)
        {
            var connection = _connectionFactory.OpenConnection();
            var sql = "INSERT INTO Courses (Name, Pages) VALUES (@Name, @Pages)";
            await connection.ExecuteAsync(sql, course);
        }

        public async Task UpdateCourseAsync(Course course)
        {
            var connection = _connectionFactory.OpenConnection();
            var sql = "UPDATE Courses SET Name = @Name, Pages = @Pages WHERE Id = @Id";
            await connection.ExecuteAsync(sql, course);
        }

        public async Task DeleteCourseAsync(int id)
        {
            var connection = _connectionFactory.OpenConnection();
            var sql = "DELETE FROM Courses WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
