using RestSharp;

namespace SpecflowAPITests.Client
{
    public interface IClient
    {
        RestClient GetClient();
    }
}
