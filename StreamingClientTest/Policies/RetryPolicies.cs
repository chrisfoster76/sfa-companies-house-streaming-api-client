using System;
using System.Net;
using System.Net.Http;
using Polly;
using Polly.Retry;

namespace StreamingClientTest.Policies
{
    public static class RetryPolicies
    {
        public static AsyncRetryPolicy<HttpResponseMessage> BackOffPolicy => Policy
            .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(new[]
            {
                TimeSpan.FromMinutes(10),
                TimeSpan.FromMinutes(15),
                TimeSpan.FromMinutes(20)
            }, (result, timeSpan, retryCount, context) =>
            {
                ConsoleUtilities.ShowHttpStatusCode(result.Result.StatusCode);
                Console.WriteLine($"Request failed. Retry count = {retryCount}. Waiting {timeSpan} before next retry. ");
            });

        public static AsyncRetryPolicy<HttpResponseMessage> RetryPolicy => Policy
            .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
        .WaitAndRetryAsync(new[]
        {
            TimeSpan.FromSeconds(10),
            TimeSpan.FromSeconds(20),
            TimeSpan.FromSeconds(30)
        }, (result, timeSpan, retryCount, context) =>
        {
            ConsoleUtilities.ShowHttpStatusCode(result.Result.StatusCode);
            Console.WriteLine($"Request failed. Retry count = {retryCount}. Waiting {timeSpan} before next retry. ");
        });
    }
}
