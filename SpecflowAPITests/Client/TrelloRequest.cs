using RestSharp;
using SpecflowAPITests.Config;

namespace SpecflowAPITests.Client
{
    public class TrelloRequest : IRequest
    {
        private RestRequest request;

        public TrelloRequest(string resource)
        {
            request = new RestRequest();
            request.AddParameter("key", EnvironmentConfig.GetInstance().GetKey(ApisEnum.Trello), ParameterType.QueryString);
            request.AddParameter("token", EnvironmentConfig.GetInstance().GetToken(ApisEnum.Trello), ParameterType.QueryString);    
            request.Resource = resource;
        }

        public RestRequest GetRequest()
        {
            return request;
        }
    }
}
