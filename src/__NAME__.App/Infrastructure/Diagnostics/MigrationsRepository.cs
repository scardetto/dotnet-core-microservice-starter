using System.Data;
using System.Linq;
using Dapper;

namespace __NAME__.App.Infrastructure.Diagnostics
{
    public class MigrationsRepository
    {
        private readonly IDbConnection _connection;

        private const string ALL_COLUMNS =
            "select Version = @Version, AppliedOn = @AppliedOn, Description = @Description";

        public MigrationsRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public MigrationRecord LastMigration
        {
            get
            {
                return _connection.GetList<MigrationRecord>()
                    .OrderByDescending(x => x.AppliedOn)
                    .Take(count: 1)
                    .SingleOrDefault();
            }
        }
    }
}
