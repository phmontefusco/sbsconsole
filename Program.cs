using System;
using System.IO;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using sbsconsole.services;
using Sinqia.Framework.Model;
using Serilog;

using Serilog.Injection;

//using SerilogTimings;

using Sinqia.Framework.Services;
using Microsoft.Extensions.Options;

namespace sbsconsole
{
    static class SBSBatch
    {

        static void Main(string[] args)
        {

            // create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var tracAux = serviceProvider.GetService<ITraceInfra>();

            var mem = serviceProvider.GetService<IControleMemoria>();

            mem.SetValor("HORA:1", "11");
            mem.SetValor("HORA:2", "12");

            var vamosver = mem.GetValor("HORA:1");
            var vamosver2 = mem.GetValor("HORA:2");

            tracAux.WriteStart("Inicio Programa");
            tracAux.WriteStart(vamosver);
            tracAux.WriteStart(vamosver2);

            var cf = serviceProvider.GetService<IConfiguracao>();

            var x = cf.obterValorConexao();
            var testconta = serviceProvider.GetService<ICalcService>();


            testconta.soma(2, 2);

            tracAux.WriteEnd();

        }

        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            IConfigurationRoot configuration;

            // // build configuration
            configuration = serviceCollection.addConfigSBS("app-settings.json");

            serviceCollection.AddOptions();

            serviceCollection.Configure<ConfigSBS>(configuration.GetSection("ConfigSBS"));

            serviceCollection.AddSerilogServices("log\\consoleapp.log");


            // add services
            serviceCollection.AddTransient<ITestService, TestService>();

            serviceCollection.AddTransient<ICalcService, calcService>();

            serviceCollection.AddTransient<ITraceInfra, TracerInfra>();

            serviceCollection.AddTransient<IConfiguracao, Configuracao>();

            serviceCollection.addMemory(false, "localhost:6379", "redisPH");

            serviceCollection.AddTransient<IControleMemoria, ControleMemoria>();

            // add app
            serviceCollection.AddTransient<AppController>();

        }


        private static void ConfigureConsole(IConfigurationRoot configuration)
        {
            // var dd = configuration.GetSection("ConfigSBS");

            //System.Console.WriteLine("Assembly : " + dd.proxySBS.proxynamespaceassembly.ToString());

            //System.Console.Title = configuration.GetSection("Configuration:ConsoleTitle").Value;
            System.Console.WriteLine("Assembly : " + configuration.GetSection("ConfigSBS:proxy:proxynamespaceassembly").Value);
            System.Console.WriteLine("Assemblythirdparty : " + configuration.GetSection("ConfigSBS:proxy:proxynamespaceassemblythirdparty").Value);
            System.Console.WriteLine("proxynamespacepersistencia : " + configuration.GetSection("ConfigSBS:proxy:proxynamespacepersistencia").Value);
            // var conexaoJson = configuration.GetSection("ConfigSBS:persistencia:conexao").Value;

        }


    }
}
