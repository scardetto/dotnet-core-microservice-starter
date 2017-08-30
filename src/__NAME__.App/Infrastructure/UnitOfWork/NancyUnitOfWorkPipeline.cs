using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using StructureMap;
using __NAME__.App.Infrastructure.Nancy;

namespace __NAME__.App.Infrastructure.UnitOfWork
{
    public class NancyUnitOfWorkPipeline
    {
        private const string UNIT_OF_WORK_SCOPE = "__NAME__.UnitOfWorkScope";

        private static readonly IEnumerable<string> EXCLUSIONS = new[] {
            "/ping"
        };

        public static Func<NancyContext, Response> BeforeRequest(IContainer container) => ctx => {
            if (ShouldExclude(ctx)) return null;

            ctx.Items[UNIT_OF_WORK_SCOPE] = container.GetInstance<UnitOfWorkScope>();

            return ctx.Response;
        };
        
        public static Action<NancyContext> AfterRequest() => ctx => {
            var scope = GetUnitOfWorkScope(ctx);
            scope?.Complete();
        };

        private static UnitOfWorkScope GetUnitOfWorkScope(NancyContext ctx)
        {
            return (UnitOfWorkScope)ctx.Items[UNIT_OF_WORK_SCOPE];
        }

        private static bool ShouldExclude(NancyContext ctx) => 
            ctx.IsHttpOptions() || EXCLUSIONS.Any(ctx.Request.Path.Equals);
    }
}
