using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.StructureMap;
using Nancy.Configuration;
using Nancy.Diagnostics;
using StructureMap;
using __NAME__.App.Infrastructure.UnitOfWork;

namespace __NAME__.App.Infrastructure.Nancy
{
    public class NancyBootstrapper : StructureMapNancyBootstrapper
    {
        private readonly IContainer _container;

        public NancyBootstrapper(IContainer container)
        {
            _container = container;
        }

        public override void Configure(INancyEnvironment environment)
        {
            // Set diagnostics password
            environment.Diagnostics(
                enabled: true, 
                password: "1234"
                );

            environment.Tracing(
                enabled: true,
                displayErrorTraces: true
                );
        }

        protected override IContainer GetApplicationContainer() => _container;

        protected override void RequestStartup(IContainer requestContainer, IPipelines pipelines, NancyContext context)
        {
            // Set up unit of work
            pipelines.BeforeRequest += NancyUnitOfWorkPipeline.BeforeRequest(requestContainer);
            pipelines.AfterRequest += NancyUnitOfWorkPipeline.AfterRequest();

            // Set up validation exception handling
            pipelines.OnError += HttpBadRequestPipeline.OnHttpBadRequest;
        }
    }
}
