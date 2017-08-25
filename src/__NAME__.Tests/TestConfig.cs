using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace __NAME__.Tests
{
    public class TestConfig
    {
        public string ConnectionString { get; set; }
    }

    public static class Config
    {
        public static TestConfig Instance { get; }

        static Config()
        {
            var env = Environment.GetEnvironmentVariable("APP_ENVIRONMENT")?.ToLower();

            if (string.IsNullOrEmpty(env)) {
                env = "dev";
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("_conf/appSettings.json", optional: false)
                .AddJsonFile($"_conf/appSettings.{env}.json", optional: true)
                .AddEnvironmentVariables()
                ;

            var appConfig = new TestConfig();
            builder.Build().Bind(appConfig);

            Instance = appConfig;
        }
    }
}
