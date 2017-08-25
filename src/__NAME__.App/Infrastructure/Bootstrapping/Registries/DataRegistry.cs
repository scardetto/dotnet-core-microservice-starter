using System.Data;
using System.Data.SqlClient;
using StructureMap;

namespace __NAME__.App.Infrastructure.Bootstrapping.Registries
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            For<IDbConnection>().Use(c => CreateConnection(c));
        }

        public static IDbConnection CreateConnection(IContext c)
        {
            var config = c.GetInstance<AppConfiguration>();
            var connection = new SqlConnection(config.DbConnectionString);
            connection.Open();
            return connection;
        }
    }
}