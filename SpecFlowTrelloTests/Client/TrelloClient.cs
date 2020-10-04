using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SpecFlowTrelloTests.Config;
using RestSharp;

namespace SpecFlowTrelloTests.Client
{
    public sealed class TrelloClient : IClient
    {
        private static TrelloClient instance;
        private RestClient client;
        private TrelloClient()
        {
            client = new RestClient(EnvironmentConfig.GetInstance().GetBaseUrl(service: "Trello"));
        }
        public static TrelloClient GetInstance()
        {
            if (instance == null)
            {
                instance = new TrelloClient();
            }
            return instance;
        }
        public RestClient GetClient()
        {
            return client;
        }

    }
}
