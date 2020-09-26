using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using RestSharp;
using SpecflowAPITests.Client;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TechTalk.SpecFlow;

namespace specflow_api_test_samples.Steps
{
    [Binding]
    public sealed class RequestStepDefinitions
    {
        IClient client;
        IRestResponse response;
        public RequestStepDefinitions()
        {
        }

        [Given(@"I use the ""(.*)"" service client")]
        public void GivenIUseTheServiceClient(string service)
        {
            if (service.Equals("pivotal"))
            {
                client = PivotalClient.GetInstance();
            }
            if (service.Equals("trello"))
            {
                client = TrelloClient.GetInstance();
            }
        }

        [When(@"I send a POST request to ""(.*)"" with the following json body")]
        public void WhenISendAPOSTRequestToWithTheFollowingJsonBody(string endpoint, string body)
        {
            var request = new PivotalRequest(endpoint);
            request.GetRequest().AddJsonBody(body);
            response = RequestManager.Post(client, request);
        }

        [Then(@"I validate that the response status code is ""(\d+)""")]
        public void ThenIValidateThatTheResponseStatusCodeIs(int expectedStatusCode)
        {
            Assert.AreEqual(expectedStatusCode, (int) response.StatusCode, "Expected status does not match.");
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
            foreach(var row in table.Rows)
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
