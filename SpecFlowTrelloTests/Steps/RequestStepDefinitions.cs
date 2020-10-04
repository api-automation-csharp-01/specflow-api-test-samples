using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using RestSharp;
using SpecFlowTrelloTests.Client;
using SpecFlowTrelloTests.Helpers;
using SpecFlowTrelloTests.Utils;
using TechTalk.SpecFlow;

namespace SpecFlowTrelloTests.Steps
{
    [Binding]
    public sealed class RequestStepDefinitions
    {
        private IClient client;
        private IRestResponse response;
        private Helper _helper; 

        public RequestStepDefinitions(Helper helper)
        {
            _helper = helper;
        }

        [Given(@"I use the ""(.*)"" service client")]
        public void GivenIUseTheServiceClient(string service)
        {
            if (service.Equals("Trello"))
            {
                client = TrelloClient.GetInstance();
            }
        }

        [When(@"I send a POST request to ""(.*)"" with the following body")]
        public void WhenISendAPOSTRequestToWithTheFollowingBody(string endpoint, string body)
        {
            var request = new TrelloRequest(endpoint);
            request.GetRequest().AddJsonBody(body);
            response = RequestManager.Post(client, request);
        }

        [When(@"I send a PUT request to ""(.*)"" with the following body")]
        public void WhenISendAPUTRequestToWithTheFollowingBody(string endpoint, string body)
        {
            string endpointMapped = Mapper.MapValue(endpoint, _helper.getData());
            var request = new TrelloRequest(endpointMapped);
            request.GetRequest().AddJsonBody(body);
            response = RequestManager.Put(client, request);
        }

        [When(@"I store board id for workstapce cleaning")]
        public void WhenIStoreBoardIdForWorkstapceCleaning()
        {
            var jsonObject = JObject.Parse(response.Content);
            _helper.StoreId(jsonObject.SelectToken("id").ToString());
        }

        [When(@"I store response ""(.*)"" value as ""(.*)""")]
        public void WhenIStoreResponseValueAs(string jsonPath, string key)
        {
            var jsonObject = JObject.Parse(response.Content);
            var value = jsonObject.SelectToken(jsonPath).ToString();
            _helper.StoreData(key, value);
        }

        [Then(@"I validate that status code returned is ""(\d+)""")]
        public void ThenIValidateThatStatusCodeReturnedIs(int expectedStatusCode)
        {
            Assert.AreEqual(expectedStatusCode, (int)response.StatusCode, "Expected status code does not match");
        }

        [Then(@"I validate that the response body match ""(.*)"" JSON schema")]
        public void ThenIValidateThatTheResponseBodyMatchJSONSchema(string schemaPath)
        {
            var jsonObject = JObject.Parse(response.Content);
            var jsonSchemaString = File.ReadAllText(schemaPath);
            var jsonSchema = JSchema.Parse(jsonSchemaString);
            IList<string> schemaErrors = new List<string>();
            Assert.IsTrue(jsonObject.IsValid(jsonSchema, out schemaErrors), "Schema validation failed: " + string.Join("\n", schemaErrors));
            
        }

        [Then(@"I validate that response body contains the following values")]
        public void ThenIValidateThatResponseBodyContainsTheFollowingValues(Table table)
        {
            var jsonObject = JObject.Parse(response.Content);
            var dictionary = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                dictionary.Add(row[0], row[1]);
            }
            foreach(var entry in dictionary)
            {
                Assert.AreEqual(entry.Value, jsonObject.SelectToken(entry.Key).ToString());

            }
        }

    }
}
