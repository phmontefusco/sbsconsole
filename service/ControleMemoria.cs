using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace Sinqia.Framework.Services
{
    public class ControleMemoria : IControleMemoria
    {
        private readonly IDistributedCache _distributedCache;

        public ControleMemoria(IServiceProvider service)
        {
            try
            {
                _distributedCache = service.GetRequiredService<IDistributedCache>();
            }
            catch { }
        }

        public string GetValor(string cacheKey)
        {
            //var cacheKey = "HORA";
            var ret = "Fetched from cache : " + _distributedCache.GetString(cacheKey);
            return ret;
        }

        public string SetValor(string cacheKey, string valor)
        {
            //var cacheKey = "HORA";
            //var existingTime = valor;
            _distributedCache.SetString(cacheKey, valor);
            return "Added to cache : " + valor;
        }
    }


    public interface IControleMemoria
    {
        string GetValor(string chave);
        string SetValor(string chave, string valor);
    }
}