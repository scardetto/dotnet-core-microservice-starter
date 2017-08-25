using System.Data;
using NUnit.Framework;

namespace __NAME__.Tests.Infrastructure
{
    [TestFixture]
    public abstract class PersistenceTesterBase
    {
        protected IDbConnection Connection { get; set; }

        [SetUp]
        public void SetUp()
        {
            Connection = new TestConnectionProvider().GetConnection();
        }

        [TearDown]
        public void TearDown()
        {
            Connection.Dispose();
        }
    }
}
