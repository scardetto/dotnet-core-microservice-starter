namespace __NAME__.App.Infrastructure.Config
{
    public class AppConfiguration
    {
        public string RabbitConnectionString { get; set; }
        public string DbConnectionString { get; set; }
        public AppNames Names { get; set; }

        public AppConfiguration(string appName = "__NAME__")
        {
            Names = new AppNames(appName);
        }
    }
}
