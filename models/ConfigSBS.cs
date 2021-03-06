using System;
using System.Collections.Generic;
using Sinqia.Framework.Model;

namespace Sinqia.Framework.Model
{
    public class ConfigSBS
    {
        /// <summary>
        /// Sets header title in console window
        /// </summary>

        public string chave1 { get; set; }

        public ProxyConfig proxyConfig { get; set; }

        public PersistenciaConfig persistenciaConfig { get; set; }

        public SegurancaConfig segurancaConfig { get; set; }

        public ServiceConfig serviceConfig { get; set; }

        public MemoryConfig memoryConfig { get; set; }


        public class ProxyConfig
        {
            public string proxynamespaceassembly { get; set; }
            public string proxynamespaceassemblythirdparty { get; set; }
            public string proxynamespacepersistencia { get; set; }
        }

        public class PersistenciaConfig
        {
            public ConexaoConfig conexaoConfig { get; set; }

        }

        public class SegurancaConfig
        {
            public string autenticacao { get; set; }
        }

        public class ServiceConfig
        {
            public string servicoweb { get; set; }
        }

        public class MemoryConfig
        {

            public bool providerInMemory { get; set; }
            public string providerServerMemory { get; set; }
            public string providerInstanceMemory { get; set; }

        }

    }

}