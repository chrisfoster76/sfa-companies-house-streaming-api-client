using System;
using System.Threading.Tasks;
using StreamingClientTest.Models;

namespace StreamingClientTest.Storage
{
    public interface IStorageService : IDisposable
    {
        Task StoreEvent(StreamingApiEvent eventData, bool isRoatpProvider);
        Task<long?> GetLastTimepoint();
    }
}