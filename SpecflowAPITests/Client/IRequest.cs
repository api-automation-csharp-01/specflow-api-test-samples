using RestSharp;

namespace SpecflowAPITests.Client
{
    public interface IRequest
    {
        RestRequest GetRequest();
    }
}
