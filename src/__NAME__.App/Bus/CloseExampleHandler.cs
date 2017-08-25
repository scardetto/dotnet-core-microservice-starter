using System.Data;
using System.Threading.Tasks;
using Dapper;
using Rebus.Handlers;
using __NAME__.App.Domain;
using __NAME__.Messages.Examples;

namespace __NAME__.App.Bus
{
    public class CloseExampleHandler : IHandleMessages<CloseExampleCommand>
    {
        private readonly IDbConnection _connection;

        public CloseExampleHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task Handle(CloseExampleCommand message)
        {
            var entity = _connection.Get<ExampleEntity>(message.Id);
            entity.Close();
            await _connection.UpdateAsync(entity);
        }
    }
}
