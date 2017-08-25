using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using __NAME__.Client;
using __NAME__.Client.Diagnostics;

namespace __NAME__.Tests.Acceptance.Api.Diagnostics
{
    [TestFixture]
    public class DiagnosticsTester
    {
        private readonly IDiagnosticsClient _client;

        public DiagnosticsTester()
        {
            var config = TestConfig.Instance;
            _client = ApiClientFactory.GetClient<IDiagnosticsClient>(config.ApiUrl);
        }

        [Test]
        public async Task should_report_all_status_as_ok()
        {
            var models = await _client.ListStatus();

            models.Select(m => m.Status).All(s => s == "OK")
                .Should().BeTrue("The api should report all statuses as 'OK'");
        }
    }
}
