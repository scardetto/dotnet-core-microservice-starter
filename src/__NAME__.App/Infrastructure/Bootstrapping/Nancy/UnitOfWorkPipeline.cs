using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using StructureMap;

namespace __NAME__.App.Infrastructure.Bootstrapping.Nancy
{
    public class UnitOfWorkPipeline
    {
        private const string UNIT_OF_WORK_SCOPE = "__NAME__.UnitOfWorkScope";
        private const string UNIT_OF_WORK_OPTIONS = "__NAME__.UnitOfWorkOptions";

        private static readonly IEnumerable<string> Exclusions = new[] {
            "/ping"
        };

        public static Func<NancyContext, Response> BeforeRequest(IContainer container) => ctx => {
            if (ctx.IsHttpOptions() || ShouldExclude(ctx.Request.Path)) return null;
                
            //var unitOfWork = container.GetInstance<INHibernateUnitOfWork>();
            //ctx.Items[UNIT_OF_WORK_SCOPE] = unitOfWork.CreateTransactionalScope(GetUnitOfWorkOptions(ctx));

            return ctx.Response;
        };
        
        public static Action<NancyContext> AfterRequest() => ctx => {
            //var scope = GetUnitOfWorkScope(ctx);
            //scope?.Complete();
        };

        private static bool ShouldExclude(string path) => Exclusions.Any(path.Equals);
    }
}
