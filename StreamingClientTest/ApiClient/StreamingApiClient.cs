using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Polly;
using StreamingClientTest.Config;
using StreamingClientTest.Policies;

namespace StreamingClientTest.ApiClient
{
    public sealed class StreamingApiClient : IStreamingApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _url;
        private readonly string _apiUser;

        public StreamingApiClient(string baseUrl, string url)
        {
            var configService = new ConfigurationService();
            var config = configService.GetAppConfig();

            _apiUser = config.StreamingApiUser;

            _baseUrl = baseUrl;
            _url = url;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<HttpResponseMessage> GetAsync(long? timepoint)
        {
            var authToken = Encoding.ASCII.GetBytes($"{_apiUser}:"); //username with no password
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

            var url = $"{_baseUrl}/{_url}";
           
            if (timepoint.HasValue)
            {
                url = $"{url}?timepoint={timepoint.Value}";
            }

            Console.WriteLine($"Getting {url}");

            var response = await Policy
                .WrapAsync(RetryPolicies.RetryPolicy, RetryPolicies.BackOffPolicy)
                .ExecuteAsync(() => _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead));

            ConsoleUtilities.ShowHttpStatusCode(response.StatusCode);

            return response;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
