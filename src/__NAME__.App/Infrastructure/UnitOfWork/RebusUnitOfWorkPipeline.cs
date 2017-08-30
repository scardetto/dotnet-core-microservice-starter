using System;
using Rebus.Pipeline;
using Rebus.Transport;
using StructureMap;

namespace __NAME__.App.Infrastructure.UnitOfWork
{
    public static class RebusUnitOfWorkPipeline
    {
        private const string REBUS_NESTED_CONTAINER = "nested-structuremap-container";
        private const string UNIT_OF_WORK_SCOPE = "Crm.UnitOfWorkScope";

        public static UnitOfWorkScope Create(IMessageContext ctx)
        {
            // This is a hack to get the StructureMap nested container.
            // https://github.com/rebus-org/Rebus.StructureMap/blob/master/Rebus.StructureMap/StructureMapContainerAdapter.cs
            var container = ctx.TransactionContext.GetOrNull<IContainer>(REBUS_NESTED_CONTAINER);

            if (container == null) {
                throw new ApplicationException("Could not find nested container in message context");
            }

            var scope = container.GetInstance<UnitOfWorkScope>();
            ctx.TransactionContext.Items[UNIT_OF_WORK_SCOPE] = scope;
            return scope;
        }

        public static void Commit(IMessageContext ctx, UnitOfWorkScope scope) => scope.Complete();

        public static void Rollback(IMessageContext ctx, UnitOfWorkScope scope)
        {
            // Not implemented. The scope will rollback automatically if not completed
        }

        public static void Dispose(IMessageContext ctx, UnitOfWorkScope scope) => scope.Dispose();
    }
}
