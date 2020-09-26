﻿using Newtonsoft.Json;

namespace SpecflowAPITests.Config
{
    public class ApiConfig
    {
        [JsonProperty("token")] public string Token { get; set; }
        [JsonProperty("key")] public string Key { get; set; }
        [JsonProperty("baseURL")] public string BaseUrl { get; set; }
    }
}
