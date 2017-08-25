using Rebus.Config;
using Rebus.StructureMap;
using StructureMap;
using __NAME__.Client.Diagnostics;
using __NAME__.Client.Examples;

namespace __NAME__.Client
{
    public static class ClientBootstrapper
    {
        public static void Bootstrap(IContainer container, string apiUrl, string connectionString)
        {
            BootstrapApi(container, apiUrl);
            BootstrapBus(container, connectionString);
        }

        public static void BootstrapApi(IContainer container, string apiUrl)
        {
            container.Configure(c => {
                c.ForSingletonOf<IDiagnosticsClient>().Use(ApiClientFactory.GetClient<IDiagnosticsClient>(apiUrl));
                c.ForSingletonOf<IExampleClient>().Use(ApiClientFactory.GetClient<IExampleClient>(apiUrl));
            });
        }

        public static void BootstrapBus(IContainer container, string connectionString)
        {
            // Start server bus
            Configure.With(new StructureMapContainerAdapter(container))
                .Transport(t => t.UseRabbitMqAsOneWayClient(connectionString))
                .Start()
                ;
        }
    }
}
