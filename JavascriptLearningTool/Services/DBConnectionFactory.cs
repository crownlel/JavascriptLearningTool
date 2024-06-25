using System.Data;

namespace JavascriptLearningTool.Services
{
    public class DbConnectionFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DbConnectionFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDbConnection OpenConnection()
        {
            var connection = _serviceProvider.GetRequiredService<IDbConnection>();
            connection.Open();
            return connection;
        }
    }
}
