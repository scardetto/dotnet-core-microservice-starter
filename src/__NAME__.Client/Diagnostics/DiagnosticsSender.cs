using Rebus.Bus;
using __NAME__.Messages.Diagnostics;

namespace __NAME__.Client.Diagnostics
{
    public class DiagnosticsSender
    {
        private readonly IBus _bus;

        public DiagnosticsSender(IBus bus)
        {
            _bus = bus;
        }

        public void Ping(string sender)
        {
            _bus.Send(new PingCommand { Sender = sender });
        }
    }
}