using Microsoft.AspNetCore.Builder;
using Nancy.Owin;
using __NAME__.App.Infrastructure.Bootstrapping;

namespace __NAME__.App.Infrastructure.Nancy
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(x => x.UseNancy(o => o.Bootstrapper = new NancyBootstrapper(Bootstrapper.Container)));
        }
    }
}
