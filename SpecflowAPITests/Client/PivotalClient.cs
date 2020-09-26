using RestSharp;
using SpecflowAPITests.Config;

namespace SpecflowAPITests.Client
{
    public sealed class PivotalClient : IClient
    {
        private static PivotalClient instance;
        private RestClient client;

        private PivotalClient()
        {
            client = new RestClient(EnvironmentConfig.GetInstance().GetBaseUrl(ApisEnum.Pivotal));
        }

        public static PivotalClient GetInstance()
        {
            if (instance == null) instance = new PivotalClient();

            return instance;
        }

        public RestClient GetClient()
        {
            return client;
        }
    }
}
