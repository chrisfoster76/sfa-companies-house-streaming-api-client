using StreamingClientTest.Streams;

namespace StreamingClientTest.ApiClient
{
    public class StreamingApiClientFactory : IStreamingApiClientFactory
    {
        public IStreamingApiClient GetClient(ApiStream apiStream)
        {
            return new StreamingApiClient(apiStream.GetUrl());
        }
    }
}
