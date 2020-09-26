using Newtonsoft.Json;

namespace SpecflowAPITests.Config
{
    public class ApiServices
    {
        [JsonProperty("pivotal")] public ApiConfig Pivotal { get; set; }
        [JsonProperty("trello")] public ApiConfig Trello { get; set; }
    }
}
