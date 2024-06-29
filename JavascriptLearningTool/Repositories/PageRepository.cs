using Dapper;
using JavascriptLearningTool.Helpers;
using JavascriptLearningTool.Models;

namespace JavascriptLearningTool.Repositories
{
    public class PageRepository(DbConnectionFactory connectionFactory) : BaseRepository(connectionFactory)
    {
        public async Task<CoursePage?> GetCoursePageAsync(int courseId, int pageId)
        {
            using var connection = _connectionFactory.OpenConnection();
            return await connection.QuerySingleOrDefaultAsync<CoursePage>("SELECT * FROM CoursePages WHERE CourseId = @CourseId AND Id = @PageId", new { CourseId = courseId, PageId = pageId });
        }

    }
}
