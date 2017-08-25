using Dapper;
using FluentAssertions;
using NUnit.Framework;
using __NAME__.App.Domain;
using __NAME__.Tests.Infrastructure;

namespace __NAME__.Tests.Persistence.Examples
{
    public class ExamplePersistenceTester : PersistenceTesterBase
    {
        [Test]
        public void should_save_and_load_example()
        {
            var entity = new ExampleEntity("test");
            var newEntity = VerifyPersistence(entity);

            newEntity.ShouldBeEquivalentTo(entity, DefaultCompareConfig.Compare);
        }

        private object VerifyPersistence(ExampleEntity entity)
        {
            entity.Id = Connection.Insert(entity).Value;
            return Connection.Get<ExampleEntity>(entity.Id);
        }
    }
}
