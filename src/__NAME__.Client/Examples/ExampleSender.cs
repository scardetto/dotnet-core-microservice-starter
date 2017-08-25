using Rebus.Bus;
using __NAME__.Messages.Examples;

namespace __NAME__.Client.Examples
{
    public class ExampleSender
    {
        private readonly IBus _bus;

        public ExampleSender(IBus bus)
        {
            _bus = bus;
        }

        public void CloseExample(int id)
        {
            _bus.Send(new CloseExampleCommand {Id = id});
        }
    }
}
