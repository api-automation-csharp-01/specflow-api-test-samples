using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SpecFlowTrelloTests.Config
{
    public class ApiServices
    {
        [JsonProperty("Trello")]
        public ApiConfig Trello { get; set; }
    }
}
