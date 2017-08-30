using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Yaml;
using Rebus.Auditing.Messages;
using Rebus.Config;
using Rebus.Retry.Simple;
using Rebus.StructureMap;
using StructureMap;
using __NAME__.App.Infrastructure.Config;
using __NAME__.App.Infrastructure.Nancy;
using __NAME__.App.Infrastructure.UnitOfWork;

namespace __NAME__.App.Infrastructure.Bootstrapping
{
    public static class Bootstrapper
    {
        public static string Environment { get; private set; }
        public static AppConfiguration Config { get; private set; }
        public static IContainer Container { get; private set; }

        public static void Bootstrap()
        {
            Environment = InitEnvironment();

            InitLogging();

            Config = InitConfiguration();

            Container = InitContainer();

            InitRebus();

            InitNancy();

            RunAtStartup();

            Console.ReadLine();
        }

        private static string InitEnvironment()
        {
            var env = System.Environment.GetEnvironmentVariable("APP_ENVIRONMENT")?.ToLower();

            if (string.IsNullOrEmpty(env)) {
                env = "dev";
            }

            return env;
        }

        private static AppConfiguration InitConfiguration()
        {
           var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddYamlFile("_conf/appSettings.yaml", optional: false)
                .AddYamlFile($"_conf/appSettings.{Environment}.yaml", optional: true)
                .AddEnvironmentVariables()
                ;

            var appConfig = new AppConfiguration();
            builder.Build().Bind(appConfig);

            return appConfig;
        }

        private static IContainer InitContainer()
        {
            // Setup container
            var container = new Container();

            container.Configure(c => {
                c.Scan(s => {
                    s.AssemblyContainingType<AppConfiguration>();
                    s.LookForRegistries();
                });

                c.For<AppConfiguration>().Use(Config);
            });

            return container;
        }

        private static void InitRebus()
        {
            // Start server bus
            Configure.With(new StructureMapContainerAdapter(Container))
                .Options(c => c.LogPipeline(verbose: true))
                .Transport(c => c.UseRabbitMq(Config.RabbitConnectionString, Config.Names.InputQueue))
                .Logging(c => c.Log4Net())
                .Options(c => c.EnableMessageAuditing(Config.Names.AuditQueue))
                .Options(c => c.SimpleRetryStrategy(
                    maxDeliveryAttempts: 1,
                    errorQueueAddress: Config.Names.ErrorQueue))
                .Options(c => c.EnableUnitOfWork(
                    RebusUnitOfWorkPipeline.Create,
                    RebusUnitOfWorkPipeline.Commit,
                    RebusUnitOfWorkPipeline.Rollback,
                    RebusUnitOfWorkPipeline.Dispose))
                .Start()
                ;
        }

        private static void InitNancy()
        {
            // Init Nancy
            new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build()
                .Run();
        }

        private static void InitLogging()
        {
            var config = new XmlDocument();
            using (var file = File.OpenRead($"_conf/Log4Net.{Environment}.config")) {
                config.Load(file);
            }

            var repo = log4net.LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repo, config["log4net"]);
        }

        private static void RunAtStartup()
        {
            Container.GetAllInstances<IRunAtStartup>()
                .ToList()
                .ForEach(startup => startup.Run());
        }
    }
}
