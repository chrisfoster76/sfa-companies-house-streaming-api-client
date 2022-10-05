using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using StreamingClientTest.Config;
using StreamingClientTest.Models;

namespace StreamingClientTest.Storage
{
    public sealed class StorageService : IStorageService
    {
        private readonly string _tableName;
        private readonly SqlConnection _sqlConnection;

        public StorageService(string tableName)
        {
            var configService = new ConfigurationService();
            var config = configService.GetAppConfig();

            _sqlConnection = new SqlConnection(config.DatabaseConnectionString);
            _tableName = tableName;
        }

        public async Task StoreEvent(StreamingApiEvent eventData)
        {
            var persistenceModel = new Data.EventData
            {
                Data = eventData.Data.ToString(),
                Timepoint = eventData.EventData.Timepoint,
                PublishedAt = eventData.EventData.PublishedAt,
                ResourceUri = eventData.ResourceUri,
                Type = eventData.EventData.Type,
                ResourceKind = eventData.ResourceKind,
            };

            var sql =
                $"insert into [{_tableName}] (Timepoint, PublishedAt, Type, ResourceUri, ResourceKind, Data) values (@Timepoint, @PublishedAt, @Type, @ResourceUri, @ResourceKind, @Data)";

            try
            {
                await _sqlConnection.ExecuteAsync(sql, persistenceModel);
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
        }

        public async Task<long?> GetLastTimepoint()
        {
            var sql = $"select max(timepoint) from [{_tableName}]";
            var timepoint = await _sqlConnection.ExecuteScalarAsync<long?>(sql);
            return timepoint;
        }

        public void Dispose()
        {
            _sqlConnection?.Dispose();
        }
    }
}
