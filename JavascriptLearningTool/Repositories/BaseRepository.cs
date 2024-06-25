using JavascriptLearningTool.Services;

namespace JavascriptLearningTool.Repositories
{
    public abstract class BaseRepository
    {
        private protected readonly DBConnectionFactory _connectionFactory;

        public BaseRepository(DBConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
    }
}
