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
            Id = "*.*";
            Nome = "base";
            Ativa = true;
            StringConexao = "";
            Provider = "";
            ServidorBD = "";
            ServidorComponente = "";
            Servidor = "";
            NomeBD = "";
            TimeOutBD = 5000;
            UsuarioBD = "";
            SenhaBD = "";
            HabilitarPool = false;
            MinPool = 0;
            MaxPool = 50;
            HabilitarRessubmissaoAutomaticaBD = false;
            NumeroRessubmissoesBD = 15;
            IntervaloEntreRessubmissoesBD = 5000;
            ClassePersistencia = "";
            DiretorioVirtualPortugues = "";
            DiretorioVirtualIngles = "";
            DiretorioVirtualEspanhol = "";
            Dominio = "";
            UtilizarStringConexao = false;
            UtilizarUpperCase = false;
            CaseSensitive = false;

            Auditoria = false;
        }
        // Cadastrais
        public string Id;
        public string Nome;
        public bool Ativa;

        // BD 
        public bool UtilizarStringConexao;

        // BD - UtilizarStringConexao: true
        public string StringConexao;

        // BD - UtilizarStringConexao: False
        public string ServidorBD;
        public string NomeBD;
        public long TimeOutBD;
        public string UsuarioBD;
        public string SenhaBD;
        public bool UtilizarUpperCase;
        public bool HabilitarPool;
        public int MinPool;
        public int MaxPool;

        // BD - Ressubmiss�o Autom�tica
        public bool HabilitarRessubmissaoAutomaticaBD;
        public int NumeroRessubmissoesBD;
        public int IntervaloEntreRessubmissoesBD;
        public string ClassePersistencia;

        public bool Auditoria;

        // Thiago Martins: remover/revisar na vers�o nova
        internal bool CaseSensitive;
        public string Provider;
        public string ServidorComponente;
        public string Servidor;
        public string DiretorioVirtualPortugues;
        public string DiretorioVirtualIngles;
        public string DiretorioVirtualEspanhol;
        public string Dominio;




    }
}
