using log4net;
using Newtonsoft.Json;
using Rebus.Handlers;
using StructureMap;
using __NAME__.App.Infrastructure.Bootstrapping;
using __NAME__.App.Infrastructure.Diagnostics;
using __NAME__.App.Infrastructure.Nancy;

namespace __NAME__.App.Infrastructure.Registries
{
    public class AppRegistry : Registry
    {
        public AppRegistry()
        {
            Scan(s => {
                s.AssemblyContainingType<AppRegistry>(); 
                s.WithDefaultConventions();

                s.AddAllTypesOf<IRunAtStartup>();
                s.AddAllTypesOf<IReportStatus>();

                s.ConnectImplementationsToTypesClosing(typeof(IHandleMessages<>));
            });

            For<JsonSerializer>().Use<CustomJsonSerializer>();
            For<ILog>().Use(c => LogManager.GetLogger(typeof(Bootstrapper)));
        }
    }
}
