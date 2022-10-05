using StreamingClientTest.Streams;

namespace StreamingClientTest.Storage
{
    public class StorageServiceFactory : IStorageServiceFactory
    {
        public IStorageService GetStorageService(ApiStream stream)
        {
            return new StorageService(stream.GetTable());
        }
    }
}