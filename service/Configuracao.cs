using System;
using System.Data;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
//using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
// using Senior.Framework.Nucleo.Master.Modelo;
using Sinqia.Framework.Model;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Sinqia.Framework.Services
{
    /// <summary>
    /// Summary description for Config.
    /// </summary>
    public class Configuracao : IConfiguracao
    {
        // /* ALT:08/08/2007-ATV:0074843-OC:000925-{{INICIO}} */
        // private static bool IsPathToAppConfigDelegated = false;
        // private static string PathToAppConfig = "";

        // private static Hashtable mHTArquivoLog = new Hashtable();
        // private static Hashtable mHTConexao = new Hashtable();
        // private static Hashtable mHTErro = new Hashtable();
        // private static Hashtable mHTMessageBus = new Hashtable();
        // private static Hashtable mHTProxyNamespaceAssemblyModelo = new Hashtable();
        // private static Hashtable mHTProxyNamespaceAssemblyServico = new Hashtable();
        // private static Hashtable mHTProxyNamespacePersistencia = new Hashtable();
        // private static Hashtable mHTTemporizador = new Hashtable();
        // private static Hashtable mHTTrace = new Hashtable();
        // /* ALT:08/08/2007-ATV:0074843-OC:000925-{{FIM}} */

        // /* ALT:06/02/2008-ATV:0076919-OC:002204-{{INICIO}} */
        // private static Hashtable mHTAutenticacao = new Hashtable();
        // /* ALT:06/02/2008-ATV:0076919-OC:002204-{{FIM}} */

        // /* ALT:02/07/2009-ATV:0081812-OC:009508-{{INICIO}} */
        // private static Hashtable mHTServicoWeb = new Hashtable();
        // /* ALT:02/07/2009-ATV:0081812-OC:009508-{{FIM}} */

        private readonly ITraceInfra _tracerInfra;
        private readonly IOptions<ConfigSBS> _config;

        public Configuracao(IServiceProvider service)
        {
            _tracerInfra = service.GetRequiredService<ITraceInfra>();
            _config = service.GetService<IOptions<ConfigSBS>>();
        }

        public bool utilizaRedis()
        {
            try
            {
                var usaRedis = _config.Value.memoryConfig.providerRedisMemory;
                return usaRedis;
            }
            catch
            {
                return false;
            }
        }
        public string obterValorConexao()
        {
            try
            {
                var nome = _config.Value.persistenciaConfig.conexaoConfig.nomeBD;
                return nome;
            }
            catch
            {

                return null;
            }
        }

        // public void carregaConexao(string pConexao)
        // {
        //     var builinterno = new ConfigurationProvider

        // }

        // private string obterValor(string pStrSecao, string pStrChave)
        // {
        //     NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection(pStrSecao);
        //     if (config != null)
        //     {
        //         string value = config[pStrChave];
        //         if (value == null) return null;
        //         string[] ret = new string[] { value };
        //         return ret[0];
        //     }
        //     else
        //     {
        //         return null;
        //     }
        // }
        // public string obterStringConexao()
        // {
        //     _tracerInfra.WriteStart("");
        //     _tracerInfra.WriteEnd();
        //     return obterStringConexao("*.*");
        // }
        // public string obterStringConexao(string pStrContexto)
        // {
        //     Conexao lObjConexao = new Conexao();
        //     lObjConexao = obterConexao(pStrContexto);
        //     return lObjConexao.StringConexao;
        // }

        // public static ArrayList obterTodosConexao(string pStrContexto)
        // {

        //     ArrayList lObjArr = new ArrayList();
        //     //System.Xml.XmlDataDocument lObjXMLDtDoc = new System.Xml.XmlDataDocument();
        //     //XmlDocument lObjXMLDtDoc = new XmlDocument();

        //     string arquivoXML = obterCaminhoArquivoXML("AgentesGroup/local/master/persistencia", "conexao");
        //     string arquivoXMLGlobal = obterCaminhoArquivoXML("AgentesGroup/global/master/persistencia", "conexao");
        //     if (arquivoXML == null)
        //     {
        //         arquivoXML = arquivoXMLGlobal;
        //     }

        //     if (!IO.existeArquivo(arquivoXML))
        //     {
        //         return null;
        //     }

        //     ArrayList lObjConexoes = XML.obterTodosConexao(arquivoXML, pStrContexto);
        //     if (lObjConexoes.Count == 0)
        //     {
        //         lObjConexoes = XML.obterTodosConexao(arquivoXML, "*.*");
        //     }
        //     IEnumerator enm = lObjConexoes.GetEnumerator();
        //     while (enm.MoveNext())
        //     {
        //         Conexao lObjConexao = (Conexao)enm.Current;
        //         if (lObjConexao.Ativa)
        //         {
        //             lObjArr.Add(lObjConexao);
        //         }
        //     }


        //     return lObjArr;
        // }
        // public static ArrayList obterTodosTrace(string pStrContexto, string pStrUserLoginDelegatedToAgents)
        // {

        //     ArrayList lObjArr = new ArrayList();
        //     //System.Xml.XmlDataDocument lObjXMLDtDoc = new System.Xml.XmlDataDocument();
        //     //XmlDocument lObjXMLDtDoc = new XmlDocument();
        //     string arquivoXML = obterCaminhoArquivoXML("AgentesGroup/local/master/log", "trace");
        //     string arquivoXMLGlobal = obterCaminhoArquivoXML("AgentesGroup/global/master/log", "trace");
        //     if (arquivoXML == null)
        //     {
        //         arquivoXML = arquivoXMLGlobal;
        //     }
        //     if (!IO.existeArquivo(arquivoXML))
        //     {
        //         return null;
        //     }
        //     IEnumerator enm = XML.obterTodosTrace(arquivoXML, pStrContexto, pStrUserLoginDelegatedToAgents).GetEnumerator();
        //     while (enm.MoveNext())
        //     {
        //         Trilha lObjTrace = (Trilha)enm.Current;
        //         lObjArr.Add(lObjTrace);
        //     }

        //     return lObjArr;
        // }


        // public static ArrayList obterTodosTemporizador(string pStrContexto)
        // {
        //     ArrayList lObjArr = new ArrayList();
        //     //System.Xml.XmlDataDocument lObjXMLDtDoc = new System.Xml.XmlDataDocument();
        //     //XmlDocument lObjXMLDtDoc = new XmlDocument();
        //     string arquivoXML = obterCaminhoArquivoXML("AgentesGroup/local/master/temporizador", "temporizador");
        //     string arquivoXMLGlobal = obterCaminhoArquivoXML("AgentesGroup/global/master/temporizador", "temporizador");
        //     if (arquivoXML == null)
        //     {
        //         arquivoXML = arquivoXMLGlobal;
        //     }
        //     if (!IO.existeArquivo(arquivoXML))
        //     {
        //         return null;
        //     }
        //     IEnumerator enm = XML.obterTodosTemporizador(arquivoXML, pStrContexto).GetEnumerator();
        //     while (enm.MoveNext())
        //     {
        //         Temporizador lObjTemporizador = (Temporizador)enm.Current;
        //         lObjArr.Add(lObjTemporizador);
        //     }
        //     return lObjArr;
        // }
        // /// <summary>
        // /// 
        // /// </summary>
        // /// <param name="pStrContexto"></param>
        // /// <returns></returns>
        // public static Temporizador obterTemporizador()
        // {
        //     Temporizador lObjTemporizador = new Temporizador();
        //     ArrayList lObjArr = new ArrayList();
        //     //lObjTemporizador= (Temporizador)obterTodosTemporizador("*.*")[0];	
        //     lObjTemporizador = obterTemporizador("*.*");
        //     return lObjTemporizador;
        // }
        // /// <summary>
        // /// 
        // /// </summary>
        // /// <param name="pStrContexto"></param>
        // /// <returns></returns>

        // public static Temporizador obterTemporizador(string pStrContexto)
        // {
        //     Temporizador lObjTemporizador = new Temporizador();
        //     //****************************************************************************************
        //     string lstrChave = "obterTemporizador" + pStrContexto;

        //     if (mHTTemporizador.Contains(lstrChave))
        //     {
        //         if (mHTTemporizador[lstrChave] == null)
        //         {
        //             return null;
        //         }
        //         else
        //         {
        //             lObjTemporizador = (Temporizador)mHTTemporizador[lstrChave];
        //             return lObjTemporizador;
        //         }
        //     }
        //     //****************************************************************************************

        //     ArrayList lArrObjTemporizador = obterTodosTemporizador(pStrContexto);

        //     if (lArrObjTemporizador == null || lArrObjTemporizador.Count == 0)
        //     {
        //         lObjTemporizador = null;
        //     }
        //     else
        //     {
        //         lObjTemporizador = (Temporizador)lArrObjTemporizador[0];
        //     }

        //     mHTTemporizador.Add(lstrChave, lObjTemporizador);

        //     return lObjTemporizador;
        // }


        // /// <summary>
        // /// 
        // /// </summary>
        // /// <param name="pStrContexto"></param>
        // /// <param name="pStrNome"></param>
        // /// <returns></returns>
        // public static Temporizador obterTemporizador(string pStrContexto, string pStrNome)
        // {
        //     Temporizador lObjTemporizador = new Temporizador();
        //     ArrayList lObjArr = new ArrayList();
        //     lObjArr = obterTodosTemporizador(pStrContexto);
        //     IEnumerator lenm = lObjArr.GetEnumerator();
        //     while (lenm.MoveNext())
        //     {
        //         lObjTemporizador = (Temporizador)lenm.Current;
        //         if (lObjTemporizador.Id.Equals(pStrContexto) && lObjTemporizador.Nome.Equals(pStrNome))
        //         {
        //             break;
        //         }
        //     }
        //     return lObjTemporizador;
        // }

        // public static ArrayList obterTodosErro(string pStrContexto)
        // {
        //     ArrayList lObjArr = new ArrayList();
        //     //System.Xml.XmlDataDocument lObjXMLDtDoc = new System.Xml.XmlDataDocument();
        //     //XmlDocument lObjXMLDtDoc = new XmlDocument();
        //     string arquivoXML = obterCaminhoArquivoXML("AgentesGroup/local/master/log", "erro");
        //     string arquivoXMLGlobal = obterCaminhoArquivoXML("AgentesGroup/global/master/log", "erro");
        //     if (arquivoXML == null)
        //     {
        //         arquivoXML = arquivoXMLGlobal;
        //     }

        //     IEnumerator enm = XML.obterTodosErro(arquivoXML, pStrContexto).GetEnumerator();
        //     while (enm.MoveNext())
        //     {
        //         Erro lObjErro = (Erro)enm.Current;
        //         lObjArr.Add(lObjErro);
        //     }
        //     return lObjArr;
        // }
        // public static ArquivoLog obterArquivoLog(string pStrContexto)
        // {
        //     ArquivoLog lObjArquivoLog = new ArquivoLog();
        //     //****************************************************************************************
        //     string lstrChave = "obterArquivoLog" + pStrContexto;

        //     if (mHTArquivoLog.Contains(lstrChave))
        //     {
        //         if (mHTArquivoLog[lstrChave] == null)
        //         {
        //             return null;
        //         }
        //         else
        //         {
        //             lObjArquivoLog = (ArquivoLog)mHTArquivoLog[lstrChave];
        //             return lObjArquivoLog;
        //         }
        //     }
        //     //****************************************************************************************

        //     ArrayList lArrObjArquivoLog = obterTodosArquivoLog(pStrContexto);

        //     if (lArrObjArquivoLog == null || lArrObjArquivoLog.Count == 0)
        //     {
        //         lObjArquivoLog = null;
        //     }
        //     else
        //     {
        //         lObjArquivoLog = (ArquivoLog)lArrObjArquivoLog[0];
        //     }

        //     mHTArquivoLog.Add(lstrChave, lObjArquivoLog);

        //     return lObjArquivoLog;
        // }


        // public static ArrayList obterTodosArquivoLog(string pStrContexto)
        // {
        //     ArrayList lObjArr = new ArrayList();
        //     //System.Xml.XmlDataDocument lObjXMLDtDoc = new System.Xml.XmlDataDocument();
        //     //XmlDocument lObjXMLDtDoc = new XmlDocument();

        //     string arquivoXML = obterCaminhoArquivoXML("AgentesGroup/local/master/log", "arquivolog");
        //     string arquivoXMLGlobal = obterCaminhoArquivoXML("AgentesGroup/global/master/log", "arquivolog");
        //     if (arquivoXML == null)
        //     {
        //         arquivoXML = arquivoXMLGlobal;
        //     }
        //     if (!IO.existeArquivo(arquivoXML))
        //     {
        //         return null;
        //     }
        //     IEnumerator enm = XML.obterTodosArquivoLog(arquivoXML, pStrContexto).GetEnumerator();
        //     while (enm.MoveNext())
        //     {
        //         ArquivoLog lObjArquivoLog = (ArquivoLog)enm.Current;
        //         lObjArr.Add(lObjArquivoLog);
        //     }
        //     return lObjArr;
        // }

        // public static ArrayList obterTodosMessageBus(string pStrContexto)
        // {
        //     ArrayList lObjArr = new ArrayList();
        //     //System.Xml.XmlDataDocument lObjXMLDtDoc = new System.Xml.XmlDataDocument();
        //     //XmlDocument lObjXMLDtDoc = new XmlDocument();
        //     string arquivoXML = obterCaminhoArquivoXML("AgentesGroup/local/master/ems", "messagebus");
        //     string arquivoXMLGlobal = obterCaminhoArquivoXML("AgentesGroup/global/master/ems", "messagebus");
        //     if (arquivoXML == null)
        //     {
        //         arquivoXML = arquivoXMLGlobal;
        //     }
        //     IEnumerator enm = XML.obterTodosMessageBus(arquivoXML, pStrContexto).GetEnumerator();
        //     while (enm.MoveNext())
        //     {
        //         MessageBus lObjMessageBus = (MessageBus)enm.Current;
        //         lObjArr.Add(lObjMessageBus);
        //     }
        //     return lObjArr;
        // }

        // /* ALT:06/02/2008-ATV:0076919-OC:002204-{{INICIO}} */
        // public static ArrayList obterTodosAutenticacao(string pStrContexto)
        // {
        //     ArrayList lObjArr = new ArrayList();
        //     //System.Xml.XmlDataDocument lObjXMLDtDoc = new System.Xml.XmlDataDocument();
        //     //XmlDocument lObjXMLDtDoc = new XmlDocument();
        //     string arquivoXML = obterCaminhoArquivoXML("AgentesGroup/local/master/seguranca", "autenticacao");
        //     string arquivoXMLGlobal = obterCaminhoArquivoXML("AgentesGroup/global/master/seguranca", "autenticacao");
        //     if (arquivoXML == null)
        //     {
        //         arquivoXML = arquivoXMLGlobal;
        //     }
        //     IEnumerator enm = XML.obterTodosAutenticacao(arquivoXML, pStrContexto).GetEnumerator();
        //     while (enm.MoveNext())
        //     {
        //         Autenticacoes lObjAutenticacoes = (Autenticacoes)enm.Current;
        //         lObjArr.Add(lObjAutenticacoes);
        //     }
        //     return lObjArr;
        // }

        // public static Autenticacao obterAutenticacao(string pStrContexto, string pStrID)
        // {
        //     Autenticacao lObjAutenticacao = new Autenticacao();

        //     string lstrChave = "obterAutenticacao" + pStrContexto + pStrID;

        //     if (mHTAutenticacao.Contains(lstrChave))
        //     {
        //         if (mHTAutenticacao[lstrChave] == null)
        //         {
        //             return null;
        //         }
        //         else
        //         {
        //             lObjAutenticacao = (Autenticacao)mHTAutenticacao[lstrChave];
        //             return lObjAutenticacao;
        //         }
        //     }

        //     ArrayList lArrObjAutenticacoes = obterTodosAutenticacao(pStrContexto);

        //     if (lArrObjAutenticacoes == null || lArrObjAutenticacoes.Count == 0)
        //     {
        //         lObjAutenticacao = null;
        //     }
        //     else
        //     {
        //         foreach (Autenticacoes lObjAutenticacoes in lArrObjAutenticacoes)
        //         {
        //             foreach (Autenticacao lObjAutenticacaoItem in lObjAutenticacoes.items)
        //             {
        //                 if (lObjAutenticacaoItem.Id.ToUpper() == pStrID.ToUpper())
        //                 {
        //                     lObjAutenticacao = lObjAutenticacaoItem;
        //                     break;
        //                 }
        //             }
        //         }
        //     }


        //     //Boa tarde Daniel, 
        //     //Acrecentamos um lock ao objeto HashTable (mHTProxyNamespaceAssemblyModelo) para que o proprio Framework gerencie os acesso concorrentes. 
        //     //S�o apenas duas linhas de codigo dentro do bloco travado, portanto n�o vemos problema nisso, e por outro lado podemos verificar ao longo do tempo se isso resolve os Autenticacaos intermitentes que ocorrem nesse trecho.
        //     //Voc� v� algum problema nessa altera��o?

        //     lock (mHTAutenticacao)
        //     {
        //         if (!mHTAutenticacao.Contains(lstrChave))
        //         {
        //             mHTAutenticacao.Add(lstrChave, lObjAutenticacao);
        //         }
        //     }




        //     return lObjAutenticacao;
        // }
        // /* ALT:06/02/2008-ATV:0076919-OC:002204-{{FIM}} */

        // public static ProxyNamespaceAssembly obterProxyNamespaceAssemblyModelo(string pStrContexto)
        // {
        //     ProxyNamespaceAssembly lObjProxyNamespaceAssemblyModelo = new ProxyNamespaceAssembly();
        //     //****************************************************************************************
        //     string lstrChave = "obterProxyNamespaceAssemblyModelo" + pStrContexto;

        //     if (mHTProxyNamespaceAssemblyModelo.Contains(lstrChave))
        //     {
        //         if (mHTProxyNamespaceAssemblyModelo[lstrChave] == null)
        //         {
        //             return null;
        //         }
        //         else
        //         {
        //             lObjProxyNamespaceAssemblyModelo = (ProxyNamespaceAssembly)mHTProxyNamespaceAssemblyModelo[lstrChave];
        //             return lObjProxyNamespaceAssemblyModelo;
        //         }
        //     }
        //     //****************************************************************************************

        //     ArrayList lArrObjProxyNAM = obterTodosProxyNamespaceAssemblyModelo(pStrContexto);

        //     if (lArrObjProxyNAM == null || lArrObjProxyNAM.Count == 0)
        //     {
        //         lObjProxyNamespaceAssemblyModelo = null;
        //     }
        //     else
        //     {
        //         lObjProxyNamespaceAssemblyModelo = (ProxyNamespaceAssembly)lArrObjProxyNAM[0];
        //     }


        //     //Boa tarde Daniel, 
        //     //
        //     // 
        //     //
        //     //Acrecentamos um lock ao objeto HashTable (mHTProxyNamespaceAssemblyModelo) para que o proprio Framework gerencie os acesso concorrentes. 
        //     //
        //     //S�o apenas duas linhas de codigo dentro do bloco travado, portanto n�o vemos problema nisso, e por outro lado podemos verificar ao longo do tempo se isso resolve os erros intermitentes que ocorrem nesse trecho.
        //     //
        //     //Voc� v� algum problema nessa altera��o?

        //     lock (mHTProxyNamespaceAssemblyModelo)
        //     {

        //         if (!mHTProxyNamespaceAssemblyModelo.Contains(lstrChave))
        //         {

        //             mHTProxyNamespaceAssemblyModelo.Add(lstrChave, lObjProxyNamespaceAssemblyModelo);

        //         }

        //     }


        //     return lObjProxyNamespaceAssemblyModelo;
        // }

        // public static ProxyNamespaceAssembly obterProxyNamespaceAssemblyServico(string pStrContexto)
        // {
        //     ProxyNamespaceAssembly lObjProxyNamespaceAssemblyServico = new ProxyNamespaceAssembly();
        //     //****************************************************************************************
        //     string lstrChave = "obterProxyNamespaceAssemblyServico" + pStrContexto;

        //     if (mHTProxyNamespaceAssemblyServico.Contains(lstrChave))
        //     {
        //         if (mHTProxyNamespaceAssemblyServico[lstrChave] == null)
        //         {
        //             return null;
        //         }
        //         else
        //         {
        //             lObjProxyNamespaceAssemblyServico = (ProxyNamespaceAssembly)mHTProxyNamespaceAssemblyServico[lstrChave];
        //             return lObjProxyNamespaceAssemblyServico;
        //         }
        //     }
        //     //****************************************************************************************

        //     ArrayList lArrObjProxyNAS = obterTodosProxyNamespaceAssemblyServico(pStrContexto);

        //     if (lArrObjProxyNAS == null || lArrObjProxyNAS.Count == 0)
        //     {
        //         lObjProxyNamespaceAssemblyServico = null;
        //     }
        //     else
        //     {
        //         lObjProxyNamespaceAssemblyServico = (ProxyNamespaceAssembly)lArrObjProxyNAS[0];
        //     }



        //     //Boa tarde Daniel, 
        //     //
        //     // 
        //     //
        //     //Acrecentamos um lock ao objeto HashTable (mHTProxyNamespaceAssemblyModelo) para que o proprio Framework gerencie os acesso concorrentes. 
        //     //
        //     //S�o apenas duas linhas de codigo dentro do bloco travado, portanto n�o vemos problema nisso, e por outro lado podemos verificar ao longo do tempo se isso resolve os erros intermitentes que ocorrem nesse trecho.
        //     //
        //     //Voc� v� algum problema nessa altera��o?

        //     lock (mHTProxyNamespaceAssemblyServico)
        //     {

        //         if (!mHTProxyNamespaceAssemblyServico.Contains(lstrChave))
        //         {

        //             mHTProxyNamespaceAssemblyServico.Add(lstrChave, lObjProxyNamespaceAssemblyServico);

        //         }

        //     }



        //     return lObjProxyNamespaceAssemblyServico;
        // }

        // public static ProxyNamespacePersistencia obterProxyNamespacePersistencia(string pStrContexto)
        // {
        //     ProxyNamespacePersistencia lObjProxyNamespacePersistencia = new ProxyNamespacePersistencia();
        //     //****************************************************************************************
        //     string lstrChave = "obterProxyNamespacePersistencia" + pStrContexto;

        //     if (mHTProxyNamespacePersistencia.Contains(lstrChave))
        //     {
        //         if (mHTProxyNamespacePersistencia[lstrChave] == null)
        //         {
        //             return null;
        //         }
        //         else
        //         {
        //             lObjProxyNamespacePersistencia = (ProxyNamespacePersistencia)mHTProxyNamespacePersistencia[lstrChave];
        //             return lObjProxyNamespacePersistencia;
        //         }
        //     }
        //     //****************************************************************************************

        //     ArrayList lArrObjPNP = obterTodosProxyNamespacePersistencia(pStrContexto);

        //     if (lArrObjPNP == null || lArrObjPNP.Count == 0)
        //     {
        //         lObjProxyNamespacePersistencia = null;
        //     }
        //     else
        //     {
        //         lObjProxyNamespacePersistencia = (ProxyNamespacePersistencia)lArrObjPNP[0];
        //     }


        //     //Boa tarde Daniel, 
        //     //
        //     // 
        //     //
        //     //Acrecentamos um lock ao objeto HashTable (mHTProxyNamespaceAssemblyModelo) para que o proprio Framework gerencie os acesso concorrentes. 
        //     //
        //     //S�o apenas duas linhas de codigo dentro do bloco travado, portanto n�o vemos problema nisso, e por outro lado podemos verificar ao longo do tempo se isso resolve os erros intermitentes que ocorrem nesse trecho.
        //     //
        //     //Voc� v� algum problema nessa altera��o?

        //     lock (mHTProxyNamespacePersistencia)
        //     {

        //         if (!mHTProxyNamespacePersistencia.Contains(lstrChave))
        //         {

        //             mHTProxyNamespacePersistencia.Add(lstrChave, lObjProxyNamespacePersistencia);

        //         }

        //     }

        //     return lObjProxyNamespacePersistencia;
        // }

        // public static Trilha obterTrace(string pStrContexto, string pStrUserLoginDelegatedToAgents)
        // {
        //     Trilha lObjTrace = new Trilha();
        //     //****************************************************************************************

        //     string lstrChave = "obterTrace" + pStrContexto;

        //     if (mHTTrace.Contains(lstrChave))
        //     {
        //         if (mHTTrace[lstrChave] == null)
        //         {
        //             return null;
        //         }
        //         else
        //         {
        //             lObjTrace = (Trilha)mHTTrace[lstrChave];
        //             return lObjTrace;
        //         }
        //     }
        //     //****************************************************************************************

        //     ArrayList lArrObjTrace = obterTodosTrace(pStrContexto, pStrUserLoginDelegatedToAgents);

        //     if (lArrObjTrace == null || lArrObjTrace.Count == 0)
        //     {
        //         lObjTrace = null;
        //     }
        //     else
        //     {
        //         lObjTrace = (Trilha)lArrObjTrace[0];
        //     }

        //     //Boa tarde Daniel, 
        //     //Acrecentamos um lock ao objeto HashTable (mHTProxyNamespaceAssemblyModelo) para que o proprio Framework gerencie os acesso concorrentes. 
        //     //S�o apenas duas linhas de codigo dentro do bloco travado, portanto n�o vemos problema nisso, e por outro lado podemos verificar ao longo do tempo se isso resolve os Traces intermitentes que ocorrem nesse trecho.
        //     //Voc� v� algum problema nessa altera��o?

        //     lock (mHTTrace)
        //     {
        //         if (!mHTTrace.Contains(lstrChave))
        //         {
        //             mHTTrace.Add(lstrChave, lObjTrace);
        //         }
        //     }

        //     return lObjTrace;
        // }
        // public static Erro obterErro(string pStrContexto)
        // {
        //     Erro lObjErro = new Erro();
        //     //****************************************************************************************
        //     string lstrChave = "obterErro" + pStrContexto;

        //     if (mHTErro.Contains(lstrChave))
        //     {
        //         if (mHTErro[lstrChave] == null)
        //         {
        //             return null;
        //         }
        //         else
        //         {
        //             lObjErro = (Erro)mHTErro[lstrChave];
        //             return lObjErro;
        //         }
        //     }
        //     //****************************************************************************************

        //     ArrayList lArrObjErro = obterTodosErro(pStrContexto);

        //     if (lArrObjErro == null || lArrObjErro.Count == 0)
        //     {
        //         lObjErro = null;
        //     }
        //     else
        //     {
        //         lObjErro = (Erro)lArrObjErro[0];
        //     }


        //     //Boa tarde Daniel, 
        //     //Acrecentamos um lock ao objeto HashTable (mHTProxyNamespaceAssemblyModelo) para que o proprio Framework gerencie os acesso concorrentes. 
        //     //S�o apenas duas linhas de codigo dentro do bloco travado, portanto n�o vemos problema nisso, e por outro lado podemos verificar ao longo do tempo se isso resolve os erros intermitentes que ocorrem nesse trecho.
        //     //Voc� v� algum problema nessa altera��o?

        //     lock (mHTErro)
        //     {
        //         if (!mHTErro.Contains(lstrChave))
        //         {
        //             mHTErro.Add(lstrChave, lObjErro);
        //         }
        //     }


        //     return lObjErro;
        // }

        // public static Conexao obterConexao(string pStrContexto)
        // {
        //     Conexao lObjConexao = new Conexao();
        //     //****************************************************************************************
        //     string lstrChave = "obterConexao" + pStrContexto;

        //     if (mHTConexao.Contains(lstrChave))
        //     {
        //         if (mHTConexao[lstrChave] == null)
        //         {
        //             return null;
        //         }
        //         else
        //         {
        //             lObjConexao = (Conexao)mHTConexao[lstrChave];
        //             return lObjConexao;
        //         }
        //     }
        //     //****************************************************************************************

        //     ArrayList lArrObjCon = obterTodosConexao(pStrContexto);

        //     if (lArrObjCon == null || lArrObjCon.Count == 0)
        //     {
        //         lObjConexao = null;
        //     }
        //     else
        //     {
        //         lObjConexao = (Conexao)lArrObjCon[0];
        //     }

        //     //Boa tarde Daniel, 
        //     //Acrecentamos um lock ao objeto HashTable (mHTProxyNamespaceAssemblyModelo) para que o proprio Framework gerencie os acesso concorrentes. 
        //     //S�o apenas duas linhas de codigo dentro do bloco travado, portanto n�o vemos problema nisso, e por outro lado podemos verificar ao longo do tempo se isso resolve os erros intermitentes que ocorrem nesse trecho.
        //     //Voc� v� algum problema nessa altera��o?

        //     lock (mHTConexao)
        //     {
        //         if (!mHTConexao.Contains(lstrChave))
        //         {
        //             mHTConexao.Add(lstrChave, lObjConexao);
        //         }
        //     }

        //     return lObjConexao;

        // }

        // /// <summary>
        // /// L� os assemblys para chamada de getServico no framework da senior.
        // /// </summary>
        // /// <param name="pStrContexto"></param>
        // /// <returns></returns>
        // public static ArrayList obterTodosProxyNamespaceAssemblyModelo(string pStrContexto)
        // {
        //     ArrayList lObjArr = new ArrayList();
        //     //System.Xml.XmlDataDocument lObjXMLDtDoc = new System.Xml.XmlDataDocument();
        //     //XmlDocument lObjXMLDtDoc = new XmlDocument();
        //     string arquivoXML = obterCaminhoArquivoXML("AgentesGroup/local/master/proxy", "proxynamespaceassembly");
        //     string arquivoXMLGlobal = obterCaminhoArquivoXML("AgentesGroup/global/master/proxy", "proxynamespaceassembly");
        //     if (arquivoXML == null)
        //     {
        //         arquivoXML = arquivoXMLGlobal;
        //     }
        //     /* ALT:17/04/2009-ATV:0081309-OC:008060-{{INICIO}} */
        //     lObjArr = obterTodosProxyNamespaceAssemblyModeloThirdParty(pStrContexto);
        //     if (lObjArr.Count == 0)
        //     {
        //         IEnumerator enm = XML.obterTodosProxyNamespaceAssemblyModelo(arquivoXML, pStrContexto).GetEnumerator();
        //         while (enm.MoveNext())
        //         {
        //             ProxyNamespaceAssembly lObjProxyNamespaceAssembly = (ProxyNamespaceAssembly)enm.Current;
        //             lObjArr.Add(lObjProxyNamespaceAssembly);
        //         }
        //     }
        //     return lObjArr;
        //     /* ALT:17/04/2009-ATV:0081309-OC:008060-{{FIM}} */
        // }

        // /// <summary>
        // /// L� os assemblys para chamada de getServico no framework da senior.
        // /// </summary>
        // /// <param name="pStrContexto"></param>
        // /// <returns></returns>
        // public static ArrayList obterTodosProxyNamespaceAssemblyServico(string pStrContexto)
        // {
        //     ArrayList lObjArr = new ArrayList();
        //     //System.Xml.XmlDataDocument lObjXMLDtDoc = new System.Xml.XmlDataDocument();
        //     //XmlDocument lObjXMLDtDoc = new XmlDocument();
        //     string arquivoXML = obterCaminhoArquivoXML("AgentesGroup/local/master/proxy", "proxynamespaceassembly");
        //     string arquivoXMLGlobal = obterCaminhoArquivoXML("AgentesGroup/global/master/proxy", "proxynamespaceassembly");
        //     if (arquivoXML == null)
        //     {
        //         arquivoXML = arquivoXMLGlobal;
        //     }
        //     /* ALT:17/04/2009-ATV:0081309-OC:008060-{{INICIO}} */
        //     lObjArr = obterTodosProxyNamespaceAssemblyServicoThirdParty(pStrContexto);
        //     if (lObjArr.Count == 0)
        //     {
        //         IEnumerator enm = XML.obterTodosProxyNamespaceAssemblyServico(arquivoXML, pStrContexto).GetEnumerator();
        //         while (enm.MoveNext())
        //         {
        //             ProxyNamespaceAssembly lObjProxyNamespaceAssembly = (ProxyNamespaceAssembly)enm.Current;
        //             lObjArr.Add(lObjProxyNamespaceAssembly);
        //         }
        //     }

        //     return lObjArr;
        //     /* ALT:17/04/2009-ATV:0081309-OC:008060-{{FIM}} */
        // }
        // /// <summary>
        // /// DATA:17/11/2008-ATV:0080151-OC:005692 
        // /// </summary>
        // /// <param name="pStrContexto"></param>
        // /// <returns></returns>
        // public static ArrayList obterTodosProxyNamespaceAssemblyModeloThirdParty(string pStrContexto)
        // {
        //     ArrayList lObjArr = new ArrayList();

        //     //Este try catch faz com que a falta da entrada proxynamespaceassemblythirdparty, n�o seja impeditiva para 
        //     //O funcionamento normal da aplica��o.
        //     /* ALT:30/03/2010-ATV:0000100-OC:015411-{{INICIO}} */
        //     try
        //     {
        //         //System.Xml.XmlDataDocument lObjXMLDtDoc = new System.Xml.XmlDataDocument();
        //         //XmlDocument lObjXMLDtDoc = new XmlDocument();
        //         string arquivoXML = obterCaminhoArquivoXML("AgentesGroup/local/master/proxy", "proxynamespaceassemblythirdparty");
        //         string arquivoXMLGlobal = obterCaminhoArquivoXML("AgentesGroup/global/master/proxy", "proxynamespaceassemblythirdparty");
        //         if (arquivoXML == null)
        //         {
        //             arquivoXML = arquivoXMLGlobal;
        //         }

        //         if (arquivoXML != null)
        //         {
        //             IEnumerator enm = XML.obterTodosProxyNamespaceAssemblyModelo(arquivoXML, pStrContexto).GetEnumerator();
        //             while (enm.MoveNext())
        //             {
        //                 ProxyNamespaceAssembly lObjProxyNamespaceAssembly = (ProxyNamespaceAssembly)enm.Current;
        //                 lObjArr.Add(lObjProxyNamespaceAssembly);
        //             }
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         LogInfra.gravarErro(e, "Warning: O config da sua aplica��o est� sem a entrada proxynamespaceassemblythirdparty. Por favor regularize o config.");
        //         lObjArr = new ArrayList();
        //         return lObjArr;
        //     }
        //     /* ALT:30/03/2010-ATV:0000100-OC:015411-{{FIM}} */

        //     return lObjArr;
        // }
        // /// <summary>
        // /// DATA:17/11/2008-ATV:0080151-OC:005692 
        // /// </summary>
        // /// <param name="pStrContexto"></param>
        // /// <returns></returns>
        // public static ArrayList obterTodosProxyNamespaceAssemblyServicoThirdParty(string pStrContexto)
        // {
        //     ArrayList lObjArr = new ArrayList();
        //     //Este try catch faz com que a falta da entrada proxynamespaceassemblythirdparty, n�o seja impeditiva para 
        //     //O funcionamento normal da aplica��o.
        //     /* ALT:30/03/2010-ATV:0000100-OC:015411-{{INICIO}} */
        //     try
        //     {
        //         //System.Xml.XmlDataDocument lObjXMLDtDoc = new System.Xml.XmlDataDocument();
        //         //XmlDocument lObjXMLDtDoc = new XmlDocument();
        //         string arquivoXML = obterCaminhoArquivoXML("AgentesGroup/local/master/proxy", "proxynamespaceassemblythirdparty");
        //         string arquivoXMLGlobal = obterCaminhoArquivoXML("AgentesGroup/global/master/proxy", "proxynamespaceassemblythirdparty");
        //         if (arquivoXML == null)
        //         {
        //             arquivoXML = arquivoXMLGlobal;
        //         }

        //         if (arquivoXML != null)
        //         {
        //             IEnumerator enm = XML.obterTodosProxyNamespaceAssemblyServico(arquivoXML, pStrContexto).GetEnumerator();
        //             while (enm.MoveNext())
        //             {
        //                 ProxyNamespaceAssembly lObjProxyNamespaceAssembly = (ProxyNamespaceAssembly)enm.Current;
        //                 lObjArr.Add(lObjProxyNamespaceAssembly);
        //             }
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         LogInfra.gravarErro(e, "Warning: O config da sua aplica��o est� sem a entrada proxynamespaceassemblythirdparty. Por favor regularize o config.");
        //         lObjArr = new ArrayList();
        //         return lObjArr;
        //     }
        //     /* ALT:30/03/2010-ATV:0000100-OC:015411-{{FIM}} */

        //     return lObjArr;
        // }

        // /// <summary>
        // /// DATA:21/05/2007-ATV:0073857-OC:000338 
        // /// </summary>
        // /// <param name="pStrContexto"></param>
        // /// <returns></returns>
        // public static ArrayList obterTodosProxyNamespacePersistencia(string pStrContexto)
        // {

        //     ArrayList lObjArr = new ArrayList();
        //     //System.Xml.XmlDataDocument lObjXMLDtDoc = new System.Xml.XmlDataDocument();
        //     //XmlDocument lObjXMLDtDoc = new XmlDocument();
        //     string arquivoXML = obterCaminhoArquivoXML("AgentesGroup/local/master/proxy", "proxynamespacepersistencia");
        //     string arquivoXMLGlobal = obterCaminhoArquivoXML("AgentesGroup/global/master/proxy", "proxynamespacepersistencia");
        //     if (arquivoXML == null)
        //     {
        //         arquivoXML = arquivoXMLGlobal;
        //     }
        //     IEnumerator enm = XML.obterTodosProxyNamespacePersistencia(arquivoXML, pStrContexto).GetEnumerator();
        //     while (enm.MoveNext())
        //     {
        //         ProxyNamespacePersistencia lObjProxyNamespacePersistencia = (ProxyNamespacePersistencia)enm.Current;
        //         lObjArr.Add(lObjProxyNamespacePersistencia);
        //     }
        //     return lObjArr;
        // }



        // public static void delegarArquivoConfig(string pStrCaminho)
        // {
        //     IsPathToAppConfigDelegated = true;
        //     PathToAppConfig = pStrCaminho;
        // }
        // public static void cancelarDelegarArquivoConfig(string pStrCaminho)
        // {
        //     IsPathToAppConfigDelegated = false;
        //     PathToAppConfig = "";
        // }

        // /// <summary>
        // /// DATA:07/08/2007-ATV:0074843-OC:000925 
        // /// </summary>
        // /// <param name="pStrSecao"></param>
        // /// <param name="pStrChave"></param>
        // /// <returns></returns>
        // public static string obterCaminhoArquivoXML(string pStrSecao, string pStrChave)
        // {
        //     string arquivoXML = "";
        //     if (!IsPathToAppConfigDelegated)
        //     {
        //         arquivoXML = obterValor(pStrSecao, pStrChave);
        //     }
        //     else
        //     {
        //         if (PathToAppConfig.Trim().Length > 0)
        //         {
        //             ConfigXmlDocument lObjCfg = new ConfigXmlDocument();
        //             lObjCfg.Load(PathToAppConfig);

        //             //Achar Elemento. Considera-se este o �ltimo elemento..........
        //             string[] lArrStrTags = pStrSecao.Split('/');
        //             string lStrTag = lArrStrTags[lArrStrTags.Length - 1];

        //             XmlNodeList elemList = lObjCfg.GetElementsByTagName(lStrTag);

        //             //Artif�cio para conseguir extrair a "key" relacionada a pStrChave......
        //             for (int i = 0; i < elemList.Count; i++)
        //             {
        //                 if (elemList[i].HasChildNodes)
        //                 {
        //                     XmlNodeList lObjChildNodes = elemList[i].ChildNodes;
        //                     IEnumerator lObjEnum = lObjChildNodes.GetEnumerator();
        //                     while (lObjEnum.MoveNext())
        //                     {
        //                         string lStrOuterXML = ((XmlNode)lObjEnum.Current).OuterXml;
        //                         arquivoXML = lStrOuterXML.Split('=')[2];
        //                         arquivoXML = arquivoXML.Replace("/>", "");
        //                         arquivoXML = arquivoXML.Replace("\\\\", @"\");
        //                         arquivoXML = arquivoXML.Replace(Convert.ToChar(34), Convert.ToChar(32));
        //                         arquivoXML = arquivoXML.Trim();

        //                         if (lStrOuterXML.IndexOf(pStrChave) >= 0)
        //                         {
        //                             // Ao achar a chave, para o processo................
        //                             break;
        //                         }
        //                     }
        //                 }
        //             }

        //         }
        //     }
        //     return arquivoXML;
        // }
        // /// <summary>
        // /// DATA:10/11/2008-ATV:0080151-OC:005599 
        // /// 
        // /// </summary>
        // /// <param name="pLngSistema"></param>
        // /// <param name="pLngEvento"></param>
        // /// <returns></returns>
        // public static ProxyProcessamento obterProxyProcessamento(long pLngSistema, long pLngEvento)
        // {
        //     ProxyProcessamento lObjPP = new ProxyProcessamento();
        //     lObjPP.Assembly = "NOTFOUND";
        //     try
        //     {
        //         //System.Xml.XmlDataDocument lObjXMLDtDoc = new System.Xml.XmlDataDocument();
        //         //XmlDocument lObjXMLDtDoc = new XmlDocument();
        //         string arquivoXML = obterCaminhoArquivoXML("AgentesGroup/local/master/proxy", "proxyprocessamento");
        //         string arquivoXMLGlobal = obterCaminhoArquivoXML("AgentesGroup/global/master/proxy", "proxyprocessamento");
        //         if (arquivoXML == null)
        //         {
        //             arquivoXML = arquivoXMLGlobal;
        //         }
        //         if (IO.existeArquivo(arquivoXML))
        //         {
        //             DataSet lObjDS = createDataSetProxyProcessamento();
        //             lObjDS.ReadXml(arquivoXML);
        //             string lStrSQL = " Sistema = " + pLngSistema.ToString() + " AND " + " Evento = " + pLngEvento.ToString();
        //             DataRow[] lObjArrDR = lObjDS.Tables["ProxyProcessmento"].Select(lStrSQL);
        //             IEnumerator lObjIDR = lObjArrDR.GetEnumerator();
        //             while (lObjIDR.MoveNext())
        //             {
        //                 DataRow lObjDR = (DataRow)lObjIDR.Current;
        //                 lObjPP.Sistema = (long)lObjDR["Sistema"];
        //                 lObjPP.Evento = (long)lObjDR["Evento"];
        //                 lObjPP.Classe = (string)lObjDR["Classe"];
        //                 //Este "complic�metro" existe devido a natureza complexa da defini��o de um assembly...
        //                 string[] lObjArr = lObjDR["Assembly"].ToString().Split(',');
        //                 lObjPP.Assembly = lObjArr[0].ToString().Trim();
        //                 lObjPP.Versao = lObjArr[1].ToString().Trim();
        //                 lObjPP.Token = lObjArr[2].ToString().Trim();
        //             }
        //         }
        //         else
        //         {
        //             LogInfra.gravarErroSemException("N�o encontrei o arquivo XML do ProxyProcessamento", "", "");
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         LogInfra.gravarErro(e);
        //         throw e;
        //     }
        //     //Caso n�o ache o assembly o arquivo padr�o
        //     //Procure no arquivo thirdparty
        //     if (lObjPP.Assembly.Equals("NOTFOUND"))
        //     {
        //         return obterProxyProcessamentoThirdParty(pLngSistema, pLngEvento);
        //     }
        //     else
        //     {
        //         return lObjPP;
        //     }
        // }

        // /// <summary>
        // /// DATA:10/11/2008-ATV:0080151-OC:005599 
        // /// 
        // /// </summary>
        // /// <param name="pLngSistema"></param>
        // /// <param name="pLngEvento"></param>
        // /// <returns></returns>
        // public static ProxyProcessamento obterProxyProcessamentoThirdParty(long pLngSistema, long pLngEvento)
        // {
        //     ProxyProcessamento lObjPP = new ProxyProcessamento();
        //     try
        //     {
        //         //System.Xml.XmlDataDocument lObjXMLDtDoc = new System.Xml.XmlDataDocument();
        //         //XmlDocument lObjXMLDtDoc = new XmlDocument();
        //         string arquivoXML = obterCaminhoArquivoXML("AgentesGroup/local/master/proxy", "proxyprocessamentothirdparty");
        //         string arquivoXMLGlobal = obterCaminhoArquivoXML("AgentesGroup/global/master/proxy", "proxyprocessamentothirdparty");
        //         if (arquivoXML == null)
        //         {
        //             arquivoXML = arquivoXMLGlobal;
        //         }
        //         if (IO.existeArquivo(arquivoXML))
        //         {
        //             DataSet lObjDS = createDataSetProxyProcessamento();
        //             lObjDS.ReadXml(arquivoXML);
        //             string lStrSQL = " Sistema = " + pLngSistema.ToString() + " AND " + " Evento = " + pLngEvento.ToString();
        //             DataRow[] lObjArrDR = lObjDS.Tables["ProxyProcessmento"].Select(lStrSQL);
        //             IEnumerator lObjIDR = lObjArrDR.GetEnumerator();
        //             while (lObjIDR.MoveNext())
        //             {
        //                 DataRow lObjDR = (DataRow)lObjIDR.Current;
        //                 lObjPP.Sistema = (long)lObjDR["Sistema"];
        //                 lObjPP.Evento = (long)lObjDR["Evento"];
        //                 lObjPP.Classe = (string)lObjDR["Classe"];
        //                 //Este "complic�metro" existe devido a natureza complexa da defini��o de um assembly...
        //                 string[] lObjArr = lObjDR["Assembly"].ToString().Split(',');
        //                 lObjPP.Assembly = lObjArr[0].ToString().Trim();
        //                 lObjPP.Versao = lObjArr[1].ToString().Trim();
        //                 lObjPP.Token = lObjArr[2].ToString().Trim();
        //             }
        //         }
        //         else
        //         {
        //             LogInfra.gravarErroSemException("N�o encontrei o arquivo XML do ProxyProcessamento", "", "");
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         LogInfra.gravarErro(e);
        //         throw e;
        //     }
        //     return lObjPP;
        // }

        // /// <summary>
        // /// DATA:10/11/2008-ATV:0080151-OC:005599 
        // /// Ao inv�s de utilizar XSD, optei por experimentar uma nova t�cnica.
        // /// Crio um dataset para explicitar a defini��o e as constraints.
        // /// </summary>
        // /// <returns></returns>
        // private static DataSet createDataSetProxyProcessamento()
        // {
        //     try
        //     {
        //         /* ******************************************************************* */
        //         DataSet lObjDS = new DataSet();
        //         DataTable lObjDTProxyProcessamento = new DataTable();
        //         DataColumn[] lObjKeyFieldsOfEntity = new DataColumn[2];
        //         /* ******************************************************************* */
        //         lObjKeyFieldsOfEntity[0] = lObjDTProxyProcessamento.Columns.Add("Sistema", typeof(long));
        //         lObjKeyFieldsOfEntity[1] = lObjDTProxyProcessamento.Columns.Add("Evento", typeof(long));
        //         lObjDTProxyProcessamento.Columns.Add("Classe", typeof(string));
        //         lObjDTProxyProcessamento.Columns.Add("Assembly", typeof(string));
        //         /* ******************************************************************* */
        //         lObjDTProxyProcessamento.PrimaryKey = lObjKeyFieldsOfEntity;
        //         lObjDS.Tables.Add(lObjDTProxyProcessamento);
        //         lObjDS.Tables[lObjDS.Tables.Count - 1].TableName = "ProxyProcessamento";
        //         /* ******************************************************************* */

        //         return lObjDS;
        //     }
        //     catch (Exception e)
        //     {
        //         LogInfra.gravarErro(e);
        //         throw e;
        //     }
        // }

        // public static void clearConfiguration()
        // {
        //     IsPathToAppConfigDelegated = false;
        //     PathToAppConfig = "";
        //     mHTArquivoLog = new Hashtable();
        //     mHTConexao = new Hashtable();
        //     mHTErro = new Hashtable();
        //     mHTMessageBus = new Hashtable();
        //     mHTProxyNamespaceAssemblyModelo = new Hashtable();
        //     mHTProxyNamespaceAssemblyServico = new Hashtable();
        //     mHTProxyNamespacePersistencia = new Hashtable();
        //     mHTTemporizador = new Hashtable();
        //     mHTTrace = new Hashtable();
        //     mHTAutenticacao = new Hashtable();
        //     mHTServicoWeb = new Hashtable();
        // }

        // public static string obterConfiguracaoLog()
        // {
        //     string caminho = obterCaminhoArquivoXML("AgentesGroup/local/master/log", "configuracao");
        //     string str = obterCaminhoArquivoXML("AgentesGroup/global/master/log", "configuracao");

        //     if (caminho == null)
        //         caminho = str;
        //     if (!IO.existeArquivo(caminho))
        //         return null;

        //     return caminho;
        // }


        // #region "Implementa��o feita para permitir o uso de um webservice do Unibanco."
        // //Bom dia, Daniel.
        // //
        // //A atividade � a 81812.
        // //Essa � uma do Unibanco, que ir� utilizar a implementa��o, se quiser que eu abra uma separada, me avise.
        // //
        // //Muito obrigada,
        // //
        // //Silvia Ferreira
        // //Desenvolvimento - Software Banc�rio 
        // //Senior Solution S.A.

        // /// <summary>
        // /// DATA:02/07/2009-ATV:0081812-OC:009508 
        // /// </summary>
        // /// <param name="pStrContexto"></param>
        // /// <returns></returns>
        // public static ServicoWeb obterServicoWeb(string pStrContexto)
        // {
        //     ServicoWeb lObjServicoWeb = new ServicoWeb();
        //     //****************************************************************************************
        //     string lstrChave = "obterServicoWeb" + pStrContexto;

        //     if (mHTServicoWeb.Contains(lstrChave))
        //     {
        //         if (mHTServicoWeb[lstrChave] == null)
        //         {
        //             return null;
        //         }
        //         else
        //         {
        //             lObjServicoWeb = (ServicoWeb)mHTServicoWeb[lstrChave];
        //             return lObjServicoWeb;
        //         }
        //     }
        //     //****************************************************************************************

        //     ArrayList lArrObjServicoWeb = obterTodosServicoWeb(pStrContexto);

        //     if (lArrObjServicoWeb == null || lArrObjServicoWeb.Count == 0)
        //     {
        //         lObjServicoWeb = null;
        //     }
        //     else
        //     {
        //         lObjServicoWeb = (ServicoWeb)lArrObjServicoWeb[0];
        //     }


        //     //Boa tarde Daniel, 
        //     //Acrecentamos um lock ao objeto HashTable (mHTProxyNamespaceAssemblyModelo) para que o proprio Framework gerencie os acesso concorrentes. 
        //     //S�o apenas duas linhas de codigo dentro do bloco travado, portanto n�o vemos problema nisso, e por outro lado podemos verificar ao longo do tempo se isso resolve os ServicoWebs intermitentes que ocorrem nesse trecho.
        //     //Voc� v� algum problema nessa altera��o?

        //     lock (mHTServicoWeb)
        //     {
        //         if (!mHTServicoWeb.Contains(lstrChave))
        //         {
        //             mHTServicoWeb.Add(lstrChave, lObjServicoWeb);
        //         }
        //     }


        //     return lObjServicoWeb;
        // }

        // /// <summary>
        // /// DATA:02/07/2009-ATV:0081812-OC:009508 
        // /// </summary>
        // /// <param name="pStrContexto"></param>
        // /// <returns></returns>
        // public static ArrayList obterTodosServicoWeb(string pStrContexto)
        // {
        //     ArrayList lObjArr = new ArrayList();
        //     //System.Xml.XmlDataDocument lObjXMLDtDoc = new System.Xml.XmlDataDocument();
        //     string arquivoXML = obterCaminhoArquivoXML("AgentesGroup/local/master/servico", "servicoweb");
        //     string arquivoXMLGlobal = obterCaminhoArquivoXML("AgentesGroup/global/master/servico", "servicoweb");
        //     if (arquivoXML == null)
        //     {
        //         arquivoXML = arquivoXMLGlobal;
        //     }

        //     IEnumerator enm = XML.obterTodosServicoWeb(arquivoXML, pStrContexto).GetEnumerator();
        //     while (enm.MoveNext())
        //     {
        //         ServicoWeb lObjServicoWeb = (ServicoWeb)enm.Current;
        //         lObjArr.Add(lObjServicoWeb);
        //     }
        //     return lObjArr;
        // }

        // #endregion


    }


    // public static class ConfExtensions
    // {
    //     public static IServiceCollection AddConf(this IServiceCollection services, IConfigurationRoot configuration)
    //     {
    //         var section =
    //             configuration.GetSection("ConfigSBS");
    //         // we first need to create an instance
    //         var settings = new BankSettings();
    //         // then we set the properties 
    //         new ConfigureFromConfigurationOptions<BankSettings>(section)
    //             .Configure(settings);
    //         // then we register the instance into the services collection
    //         services.AddSingleton(new Models.Bank(settings));

    //         return services;
    //     }
    // }
    public interface IConfiguracao
    {

        bool utilizaRedis();

        string obterValorConexao();
    }
}
