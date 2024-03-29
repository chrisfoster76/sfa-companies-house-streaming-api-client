﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using StreamingClientTest.ApiClient;
using StreamingClientTest.Models;
using StreamingClientTest.Storage;
using StreamingClientTest.Streams;

namespace StreamingClientTest
{
    static class Program
    {
        private static readonly int MaxStreamReadTimeSeconds = 300;

        public static async Task Main(string[] args)
        {
            Console.CursorVisible = false;
            ConsoleUtilities.ShowTitle("Companies House Api Stream Test Client");
            Console.WriteLine();

            //Each execution "round-robins" the streams we're interested in
            await FetchData(ApiStream.CompanyProfile);
            await FetchData(ApiStream.Officers);
            await FetchData(ApiStream.PersonsWithSignificantControl);

            Console.WriteLine("Complete");
            Console.ReadKey();
        }

        private static async Task FetchData(ApiStream apiStream)
        {
            Console.WriteLine($"Fetching data from {apiStream}");

            var providerService = new RoatpService();
            var providers = providerService.GetProviders();

            IStorageServiceFactory storageServiceFactory = new StorageServiceFactory();
            IStreamingApiClientFactory clientFactory = new StreamingApiClientFactory();

            using var storageService = storageServiceFactory.GetStorageService(apiStream);
            using var client = clientFactory.GetClient(apiStream);
            
            var timepoint = await storageService.GetLastTimepoint();

            if (timepoint.HasValue)
            {
                Console.WriteLine($"Last timepoint: {timepoint.Value}");
            }
            else
            {
                Console.WriteLine($"No last timepoint");
            }

            if (timepoint.HasValue)
            {
                timepoint = timepoint.Value + 1;
            }

            var response = await client.GetAsync(timepoint);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Unable to connect...");
                return;
            }

            var stream = await response.Content.ReadAsStreamAsync();

            using var sr = new StreamReader(stream);

            var maxTimeReached = false;
            var noMoreData = false;
            var stopwatch = Stopwatch.StartNew();

            while (!maxTimeReached && !noMoreData)
            {
                var line = await sr.ReadLineAsync();

                Console.CursorLeft = 0;

                if (line == null)
                {
                    Console.Write(" * no more data *     ");
                    noMoreData = true;
                    continue;
                }
                
                if (string.IsNullOrWhiteSpace(line))
                {
                    Console.Write(" * heartbeat *          ");
                    continue;
                }

                try
                {
                    var pscEventData = JsonSerializer.Deserialize<StreamingApiEvent>(line);
                    Console.Write($"Timepoint: {pscEventData.EventData.Timepoint}");

                    var isRoatpProvider = providers.Any(x=> x.CompanyNumber == pscEventData.ProviderCompanyNumber);

                    await storageService.StoreEvent(pscEventData, isRoatpProvider);
                }
                catch (JsonException ex)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        Console.WriteLine($"Json error {ex.Message}");
                    }
                }

                if (stopwatch.Elapsed.TotalSeconds >= MaxStreamReadTimeSeconds)
                {
                    maxTimeReached = true;
                    Console.WriteLine();
                    Console.WriteLine($"Max time of {MaxStreamReadTimeSeconds}s reached; terminating");
                }
            }

            sr.Close();
            Console.WriteLine();
        }
    }
}
