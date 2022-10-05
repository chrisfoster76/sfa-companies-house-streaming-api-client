using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using StreamingClientTest.Config;

namespace StreamingClientTest.ApiClient
{
    public sealed class StreamingApiClient : IStreamingApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://stream.companieshouse.gov.uk";
        private readonly string _url;
        private readonly string _apiUser;

        public StreamingApiClient(string url)
        {
            var configService = new ConfigurationService();
            var config = configService.GetAppConfig();

            _apiUser = config.StreamingApiUser;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
            _url = url;
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

            var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

            Console.Write($"Status Code: ");
            if (response.IsSuccessStatusCode)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(response.StatusCode);
            }

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Response (headers) received");

            return response;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
