using System;

namespace Sinqia.Framework.Model
{
    /// <summary>
    /// Summary description for Conexao.
    /// </summary>
    public class ConexaoConfig
    {
        public ConexaoConfig()
        {
            id = "*.*";
            nome = "base";
            ativa = true;
            stringConexao = "";
            provider = "";
            servidorBD = "";
            servidorComponente = "";
            servidor = "";
            nomeBD = "";
            timeOutBD = 5000;
            usuarioBD = "";
            senhaBD = "";
            habilitarPool = false;
            minPool = 0;
            maxPool = 50;
            habilitarRessubmissaoAutomaticaBD = false;
            numeroRessubmissoesBD = 15;
            intervaloEntreRessubmissoesBD = 5000;
            classePersistencia = "";
            diretorioVirtualPortugues = "";
            diretorioVirtualIngles = "";
            diretorioVirtualEspanhol = "";
            dominio = "";
            utilizarStringConexao = false;
            utilizarUpperCase = false;
            caseSensitive = false;

            auditoria = false;
        }
        // Cadastrais
        public string id { get; set; }
        public string nome { get; set; }
        public bool ativa { get; set; }

        // BD 
        public bool utilizarStringConexao { get; set; }
        public string stringConexao { get; set; }
        public string servidorBD { get; set; }
        public string nomeBD { get; set; }
        public long timeOutBD { get; set; }
        public string usuarioBD { get; set; }
        public string senhaBD { get; set; }
        public bool utilizarUpperCase { get; set; }
        public bool habilitarPool { get; set; }
        public int minPool { get; set; }
        public int maxPool { get; set; }

        // BD - Ressubmiss�o Autom�tica
        public bool habilitarRessubmissaoAutomaticaBD;
        public int numeroRessubmissoesBD;
        public int intervaloEntreRessubmissoesBD;
        public string classePersistencia;

        public bool auditoria;

        internal bool caseSensitive;
        public string provider;
        public string servidorComponente;
        public string servidor;
        public string diretorioVirtualPortugues;
        public string diretorioVirtualIngles;
        public string diretorioVirtualEspanhol;
        public string dominio;




    }
}
