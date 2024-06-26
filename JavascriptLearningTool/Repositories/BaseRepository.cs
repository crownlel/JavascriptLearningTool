using JavascriptLearningTool.Helpers;

namespace JavascriptLearningTool.Repositories
{
    public abstract class BaseRepository
    {
        private protected readonly DbConnectionFactory _connectionFactory;

        public BaseRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
    }
}
