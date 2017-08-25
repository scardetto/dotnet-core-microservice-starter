using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Refit;
using __NAME__.Client;
using __NAME__.Client.Examples;
using __NAME__.Messages.Examples;

namespace __NAME__.Tests.Acceptance.Api.Examples
{
    [TestFixture]
    public class ExampleTester
    {
        private readonly IExampleClient _client;

        public ExampleTester()
        {
            var config = TestConfig.Instance;
            _client = ApiClientFactory.GetClient<IExampleClient>(config.ApiUrl);
        }

        [Test]
        public async Task should_list_examples()
        {
            var models = await _client.List();
            models.Should().NotBeEmpty();
        }

        [Test]
        public async Task should_create_example()
        {
            var model = new NewExampleModel {Name = "test"};
            var createdModel = await _client.Create(model);

            createdModel.Id.Should().BePositive();
        }

        [Test]
        public void should_validate_new_example()
        {
            var model = new NewExampleModel { Name = null };

            Func<Task> task = async () => { await _client.Create(model); };
            task.ShouldThrow<ApiException>().Where(ex => ContainsNameEmptyValidationError(ex));

        }

        private static bool ContainsNameEmptyValidationError(ApiException ex)
        {
            ex.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            // TODO: Need to add a strongly typed validation error payload so that individual errors can be tested.
            //var validationErrors = ex.GetContentAs<IDictionary<string, List<IDictionary<string, string[]>>>>();
            //validationErrors.Should().HaveCount(1);
            //validationErrors.First().Key.Should().Be("errors");
            //validationErrors.First().Value.Should().HaveCount(1);
            //validationErrors.First().Value.First().Should().ContainKey("name");
            //validationErrors.First().Value.First().First().Value.Should().Contain("'Name' should not be empty.");
            
            return true;
        }

        [Test]
        public async Task should_close_created_example()
        {
            var model = new NewExampleModel { Name = "test" };
            var createdModel = await _client.Create(model);
            await _client.Close(new CloseExampleModel { Id = createdModel.Id });
            Thread.Sleep(5000);

            var newModel = await _client.Get(createdModel.Id);
            newModel.Status.Should().Be(20000);
        }
    }
}
