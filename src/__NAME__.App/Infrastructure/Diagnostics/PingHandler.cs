using System.Threading.Tasks;
using log4net;
using Rebus.Handlers;
using __NAME__.Messages.Diagnostics;

namespace __NAME__.App.Infrastructure.Diagnostics
{
    public class PingHandler : IHandleMessages<PingCommand>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PingHandler));

        public Task Handle(PingCommand message)
        {
            return Task.Run(() => {
                Log.Info($"Ping received from {message.Sender} with Id {message.Id} created on {message.DateCreated}");
            });
        }
    }
}
