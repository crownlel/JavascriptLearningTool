using JavascriptLearningTool.Repositories;
using System.Data;

namespace JavascriptLearningTool.Services
{
    public class DBConnectionFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DBConnectionFactory(IServiceProvider serviceProvider)
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
