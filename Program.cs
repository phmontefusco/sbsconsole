using System;
using System.IO;
using System.Diagnostics;
// using Senior.Framework.Nucleo.Master.entInfraestrutura.Servico;
// using Senior.Framework.Nucleo.Master.entInfraestrutura.Modelo;
// using Senior.Framework.Nucleo.Master.entAcesso.Servico;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Configuration;

// using Microsoft.Extensions.Configuration.Xml;
//using Microsoft.Extensions.Logging;
using sbsconsole.services;
using Sinqia.Framework.Model;
using Serilog;
// using Serilog.Core;
// using Serilog.Events;
// using Serilog.Sinks.File;
// using Serilog.Sinks.SystemConsole;
// using Serilog.Settings.Configuration;

// using SimpleProxy.Caching;
// using SimpleProxy.Diagnostics;
// using SimpleProxy.Extensions;
// using SimpleProxy.Logging;
// using SimpleProxy.Strategies;

//using AOP.UsingDispatchProxy;
//using AOP.Example;

//using SerilogMetrics;

using Serilog.Injection;

//using SerilogTimings;

using Sinqia.Framework.Services;
using Microsoft.Extensions.Options;

namespace sbsconsole
{
    static class SBSBatch
    {

        //private static ConfigurationSBS _confSBS;
        private static IOptions<ConfigurationSBS> _confSBS;

        static void Main(string[] args)
        {

            // Valida os parametros de entrada
            //string tipo;
            //if (ValidarParametros(args, out tipo)) return;

            // Log.Logger = new LoggerConfiguration()
            // .MinimumLevel.Debug()
            // .WriteTo.RollingFile("log\\consoleapp.log", fileSizeLimitBytes: 740)
            // .CreateLogger();


            // create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var tracAux = serviceProvider.GetService<ITraceInfra>();

            var _xx = serviceProvider.GetService<IConfigurationRoot>();

            IOptions<ConfigurationSBS> iop = serviceProvider.GetService<IOptions<ConfigurationSBS>>();

            //_confSBS = new ConfigurationSBS();

            //         new ConfigureFromConfigurationOptions<ConfigurationSBS>(
            // _xx.GetSection("ConfigurationSBS"))
            //     .Configure(_confSBS);

            tracAux.WriteStart("Inicio Programa");

            //var log = serviceProvider.GetService<ILogger>();

            //Log.Information("Inicio SBS Console");


            // entry to run app
            //serviceProvider.GetService<AppController>().Run();

            var cf = serviceProvider.GetService<IConfiguracao>();

            // var x = cf.obterValorConexao("");
            var testconta = serviceProvider.GetService<ICalcService>();


            testconta.soma(2, 2);

            // var decorated = LoggingAdvice<ICalcService>.Create(
            //     new calcService(serviceProvider));


            //    var decorated = LoggingAdvice<IMyClass>.Create(
            //        new MyClass()); 

            // decorated.soma(5, 8);
            //    var length = decorated.MyMethod("Hello world!");

            //serviceProvider.GetService<MyClass>().MyMethod("teste novo log"); //in MyClass' constructor get 
            //the logger by adding a parameter (ILogger<MyClass>)
            //var logger = serviceProvider.GetService<ILogger<MyClass>>();

            //Log.Information("Final SBS Console");
            tracAux.WriteEnd();

        }


        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            IConfigurationRoot configuration;


            // serviceCollection.AddLogging(configure => configure.AddSerilog())
            //     .AddTransient<MyClass>();


            // serviceCollection.AddLogging(loggingBuilder =>
            // {
            //     loggingBuilder.AddConsole(x => x.IncludeScopes = true);
            //     loggingBuilder.AddDebug();
            // });

            // build configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("app-settings.json", optional: false, reloadOnChange: true)
                .Build();


            // var build2 = new ConfigurationBuilder()
            //     .SetBasePath(Directory.GetCurrentDirectory());
            // build2.AddJsonFile("app-settings.json", optional: false, reloadOnChange: true);
            // build2.AddJsonFile(configuration.GetSection("ConfigurationSBS:persistencia:conexao").Value);
            // build2.Build();

            serviceCollection.AddOptions();

            IConfigurationSection sec = configuration.GetSection("ConfigurationSBS");

            //serviceCollection.Configure<ConfigurationSBS>(configuration.GetSection("ConfigurationSBS"));
            serviceCollection.Configure<ConfigurationSBS>(sec);

            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);
            serviceCollection.AddSingleton<IConfiguration>(configuration);


            // configConsole(configuration);
            //ConfigureConsole(configuration);

            serviceCollection.AddSerilogServices("log\\consoleapp.log");

            // add services
            serviceCollection.AddTransient<ITestService, TestService>();

            // add services
            serviceCollection.AddTransient<ICalcService, calcService>();

            serviceCollection.AddTransient<ITraceInfra, TracerInfra>();

            serviceCollection.AddTransient<IConfiguracao, Configuracao>();

            // add app
            serviceCollection.AddTransient<AppController>();

        }

        private static void ConfiguraAmbiente(IConfiguration configuration)
        {
            var _confSBS = configuration.GetSection("ConfigurationSBS").Get<ConfigurationSBS>();
        }

        // private static void configConsole(IOptions<ConfigurationSBS> arquivoConf)
        // {
        //     arquivoConf
        //     var dd = configuration.GetSection("ConfigurationSBS");
        //     var conexaoJson = configuration.GetSection("ConfigurationSBS:persistencia:conexao").Value;

        //     configuration = new ConfigurationBuilder()
        //                     //.SetBasePath(Directory.GetCurrentDirectory())
        //                     .AddJsonFile(conexaoJson, optional: false, reloadOnChange: true)
        //                     .Build();

        // }
        private static void ConfigureConsole(IConfigurationRoot configuration)
        {
            // var dd = configuration.GetSection("ConfigurationSBS");

            //System.Console.WriteLine("Assembly : " + dd.proxySBS.proxynamespaceassembly.ToString());

            //System.Console.Title = configuration.GetSection("Configuration:ConsoleTitle").Value;
            System.Console.WriteLine("Assembly : " + configuration.GetSection("ConfigurationSBS:proxy:proxynamespaceassembly").Value);
            System.Console.WriteLine("Assemblythirdparty : " + configuration.GetSection("ConfigurationSBS:proxy:proxynamespaceassemblythirdparty").Value);
            System.Console.WriteLine("proxynamespacepersistencia : " + configuration.GetSection("ConfigurationSBS:proxy:proxynamespacepersistencia").Value);
            // var conexaoJson = configuration.GetSection("ConfigurationSBS:persistencia:conexao").Value;

            // configuration = new ConfigurationBuilder()
            //                 //.SetBasePath(Directory.GetCurrentDirectory())
            //                 .AddJsonFile(conexaoJson, optional: false, reloadOnChange: true)
            //                 .Build();

            //IConfigurationSection section = configuration.AddOptions(conexaoJson);
            // section.Bind()
        }


    }
}
