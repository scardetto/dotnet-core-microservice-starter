namespace __NAME__.App.Infrastructure.Config
{
    public class AppNames
    {
        public string Prefix { get; }

        public AppNames(string prefix)
        {
            Prefix = prefix;
        }

        private string QueuePrefix => Prefix.ToLower();
        private string TablePrefix => Prefix.ToLower();

        public string InputQueue => $"{QueuePrefix}.rebus.input";
        public string AuditQueue => $"{QueuePrefix}.rebus.audit";
        public string ErrorQueue => $"{QueuePrefix}.rebus.error";

        public string SagaTable => $"{TablePrefix}_SAGA";
        public string SagaIndexTable => $"{TablePrefix}_SAGA_INDEX";
        public string TimeoutTable => $"{TablePrefix}_TIMEOUT";
    }
}