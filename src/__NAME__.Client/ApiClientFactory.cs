using System;
using System.Configuration;
using Refit;

namespace __NAME__.Client
{
    /// <summary>
    /// This class creates implementations for your Api interfaces.
    /// </summary>
    public class ApiClientFactory
    {
        private const string ERROR_STRING = "Configuration section is missing. {0} to your app settings and provide a valid URL";
        private static readonly string ApiConfigKey = "__NAME__.api.path".ToLower();

        /// <summary>
        /// Creates a client API implementation with the provided URL.
        /// </summary>
        public static T GetClient<T>(string apiUrl)
        {
            AssertValidConfiguration(() => apiUrl != null);

            return RestService.For<T>(apiUrl);
        }

        private static void AssertValidConfiguration(Func<bool> valueIsTrue)
        {
            if (!valueIsTrue())
            {
                throw new Exception(string.Format(ERROR_STRING, ApiConfigKey));
            }
        }
    }
}
