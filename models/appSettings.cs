using System.Collections.Generic;
using Sinqia.Framework.Model;

namespace Sinqia.Framework.Model
{
    public class ConfigurationSBS
    {
        /// <summary>
        /// Sets header title in console window
        /// </summary>

        public ProxyConfig proxySBS { get; set; }

        public PersistenciaConfig persistenciaSBS { get; set; }

        public SegurancaConfig segurancaSBS { get; set; }

        public ServiceConfig serviceSBS { get; set; }


        public class ProxyConfig
        {
            public string proxynamespaceassembly { get; set; }
            public string proxynamespaceassemblythirdparty { get; set; }
            public string proxynamespacepersistencia { get; set; }
        }

        public class PersistenciaConfig
        {
            //public string conexao { get; set; }
            public ConexaoConfig conexaoSBS { get; set; }

        }

        public class SegurancaConfig
        {
            public string autenticacao { get; set; }
        }

        public class ServiceConfig
        {
            public string servicoweb { get; set; }
        }
    }
}