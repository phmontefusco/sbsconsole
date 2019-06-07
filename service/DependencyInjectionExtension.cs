using System.IO;
using Microsoft.Extensions.Configuration;
using Sinqia.Framework.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Summary description for Config.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        public static IConfigurationRoot addConfigSBS(this IServiceCollection services, string arqSettings)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(arqSettings, optional: false, reloadOnChange: true)
                .Build();
            return configuration;
        }

        public static void addMemory(this IServiceCollection serviceCollection, bool usaRedis,
                    string providerServerMemory, string providerInstanceMemory)
        //public static void addMemory(this IServiceCollection serviceCollection, IConfiguracao conf)
        {
            if (usaRedis)
            {
                serviceCollection.AddDistributedRedisCache(options =>
                {
                    // options.Configuration = "localhost:6379";
                    // options.InstanceName = "redisPH";
                    options.Configuration = providerServerMemory;
                    options.InstanceName = providerInstanceMemory;
                });
            }
            else
            {
                serviceCollection.AddDistributedMemoryCache();
            }
        }


    }
}
