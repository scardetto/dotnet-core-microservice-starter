using System.Data;
using System.Data.SqlClient;
using StructureMap;
using __NAME__.App.Infrastructure.Config;
using __NAME__.App.Infrastructure.UnitOfWork;

namespace __NAME__.App.Infrastructure.Registries
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            For<IDbConnection>().Use(c => CreateConnection(c));
            For<UnitOfWorkScope>().Use<UnitOfWorkScope>();
        }

        public static IDbConnection CreateConnection(IContext c)
        {
            var config = c.GetInstance<AppConfiguration>();
            return new SqlConnection(config.DbConnectionString);
        }
    }
}