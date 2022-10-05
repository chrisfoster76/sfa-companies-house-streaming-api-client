using StreamingClientTest.Streams;

namespace StreamingClientTest.ApiClient
{
    public interface IStreamingApiClientFactory
    {
        IStreamingApiClient GetClient(ApiStream apiStream);
    }
}