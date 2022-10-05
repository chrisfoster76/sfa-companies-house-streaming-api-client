using StreamingClientTest.Streams;

namespace StreamingClientTest.Storage
{
    public interface IStorageServiceFactory
    {
        IStorageService GetStorageService(ApiStream stream);
    }
}