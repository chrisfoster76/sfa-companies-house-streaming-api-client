using System.IO;
using Microsoft.Extensions.Configuration;

namespace StreamingClientTest.Config
{
    public interface IConfigurationService
    {
        AppConfig GetAppConfig();
    }

    public class ConfigurationService : IConfigurationService
    {
        public AppConfig GetAppConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", optional: false);

            IConfiguration config = builder.Build();

            var appConfig = config.GetSection("AppConfig").Get<AppConfig>();

            return appConfig;
        }
    }
}
