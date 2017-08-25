using System.Data;
using System.Data.SqlClient;

namespace __NAME__.Tests.Infrastructure
{
    public class TestConnectionProvider
    {
        public IDbConnection GetConnection()
        {
            var connectionString = Config.Instance.ConnectionString; 
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
