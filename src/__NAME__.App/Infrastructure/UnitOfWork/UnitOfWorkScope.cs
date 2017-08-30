using System;
using System.Data;
using System.Transactions;
using Rebus.TransactionScopes;
using StructureMap;

namespace __NAME__.App.Infrastructure.UnitOfWork
{
    public class UnitOfWorkScope : IDisposable
    {
        private readonly IDbConnection _connection;
        private readonly TransactionScope _innerScope;

        public UnitOfWorkScope(IContainer container)
        {
            _innerScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            // Enlist the DbConnection
            _connection = container.GetInstance<IDbConnection>();
            _connection.Open();

            // Enlist Rebus
            //_innerScope.EnlistRebus();
        }

        public void Complete()
        {
            _innerScope.Complete();
        }

        public void Dispose()
        {
            _connection.Close();
            _innerScope.Dispose();
        }
    }
}
