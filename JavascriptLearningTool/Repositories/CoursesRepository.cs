using Dapper;
using JavascriptLearningTool.Models;
using JavascriptLearningTool.Services;

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
