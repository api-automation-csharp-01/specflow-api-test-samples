using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using RestSharp;
using SpecflowAPITests.Client;
using SpecflowAPITests.Helpers;
using SpecflowAPITests.Utils;
using System.Collections.Generic;
using System.IO;
using TechTalk.SpecFlow;

namespace specflow_api_test_samples.Steps
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
        public void GivenIUseTheServiceClient(ApisEnum service)
        {
            if (service.Equals(ApisEnum.Pivotal))
            {
                client = PivotalClient.GetInstance();
            }
            if (service.Equals(ApisEnum.Trello))
            {
                client = TrelloClient.GetInstance();
            }
        }

        [When(@"I send a ""(.*)"" POST request to ""(.*)"" with the following json body")]
        public void WhenISendAPOSTRequestToWithTheFollowingJsonBody(ApisEnum service, string endpoint, string body)
        {
            IRequest request;
            if (service.Equals(ApisEnum.Pivotal))
            {
                request = new PivotalRequest(endpoint);
                request.GetRequest().AddJsonBody(body);
                response = RequestManager.Post(client, request);
            }
            if (service.Equals(ApisEnum.Trello))
            {
                request = new TrelloRequest(endpoint);
                request.GetRequest().AddJsonBody(body);
                response = RequestManager.Post(client, request);
            }
        }

        [When(@"I send a ""(.*)"" PUT request to ""(.*)"" with the following json body")]
        public void WhenISendAPUTRequestToWithTheFollowingJsonBody(ApisEnum service, string endpoint, string body)
        {
            string endpointMapped = Mapper.MapValue(endpoint, _helper.getData());
            IRequest request;
            if (service.Equals(ApisEnum.Pivotal))
            {
                request = new PivotalRequest(endpointMapped);
                request.GetRequest().AddJsonBody(body);
                response = RequestManager.Put(client, request);
            }
            if (service.Equals(ApisEnum.Trello))
            {
                request = new TrelloRequest(endpointMapped);
                request.GetRequest().AddJsonBody(body);
                response = RequestManager.Put(client, request);
            }
        }

        [When(@"I store project id for workspace cleaning")]
        public void WhenIStoreProjectIdForWorkspaceCleaning()
        {
            var jsonObject = JObject.Parse(response.Content);
            _helper.StoreId(jsonObject.SelectToken("id").ToString());
        }

        [When(@"I store board id for workspace cleaning")]
        public void WhenIStoreBoardIdForWorkspaceCleaning()
        {
            var jsonObject = JObject.Parse(response.Content);
            _helper.StoreId(jsonObject.SelectToken("id").ToString());
        }

        [When(@"I store response ""(.*)"" value as ""(.*)""")]
        public void WhenIStoreResponseValueAs(string jsonpath, string key)
        {
            var jsonObject = JObject.Parse(response.Content);
            var value = jsonObject.SelectToken(jsonpath).ToString();
            _helper.StoreData(key, value);
        }

        [Then(@"I validate that the response status code is ""(\d+)""")]
        public void ThenIValidateThatTheResponseStatusCodeIs(int expectedStatusCode)
        {
            Assert.AreEqual(expectedStatusCode, (int)response.StatusCode, "Expected status does not match.");
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

        [Then(@"I validate that the response body contains the following values")]
        public void ThenIValidateThatTheResponseBodyContainsTheFollowingValues(Table table)
        {
            var jsonObject = JObject.Parse(response.Content);
            var dictionary = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                dictionary.Add(row[0], row[1]);
            }
            foreach (var entry in dictionary)
            {
                Assert.AreEqual(entry.Value, jsonObject.SelectToken(entry.Key).ToString());
            }
        }
    }
}
