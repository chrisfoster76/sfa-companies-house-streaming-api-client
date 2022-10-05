using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace StreamingClientTest.ApiClient
{
    public interface IStreamingApiClient : IDisposable
    {
        Task<HttpResponseMessage> GetAsync(long? timepoint);
    }
}