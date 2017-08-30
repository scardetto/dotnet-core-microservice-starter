using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace __NAME__.App.Infrastructure.Nancy
{
    public class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            Converters.Add(new StringEnumConverter());
            ContractResolver = new CamelCasePropertyNamesContractResolver();
            DateTimeZoneHandling = DateTimeZoneHandling.Local;
            NullValueHandling = NullValueHandling.Ignore;
        }
    }
}
