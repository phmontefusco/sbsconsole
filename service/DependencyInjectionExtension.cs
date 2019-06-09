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

        public static void addMemory(this IServiceCollection serviceCollection, IConfigurationRoot configuration)
        {
            var usaMemoriaLocal = configuration.GetValue<bool>("ConfigSBS:MemoryConfig:providerInMemory");
            //var _conf = conf.utilizaRedis;
            if (usaMemoriaLocal)
            {
                serviceCollection.AddDistributedMemoryCache();
            }
            else
            {
                serviceCollection.AddDistributedRedisCache(options =>
                {
                    options.Configuration = configuration.GetSection("ConfigSBS:MemoryConfig:providerServerMemory").Value;
                    options.InstanceName = configuration.GetSection("ConfigSBS:MemoryConfig:providerInstanceMemory").Value;
                });
            }
        }


    }
}
