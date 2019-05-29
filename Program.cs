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

        //private static ConfigSBSSettings _confSBSXX;
        //private static IOptions<ConfigSBS> _confSBS;

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


            tracAux.WriteStart("Inicio Programa");


            var cf = serviceProvider.GetService<IConfiguracao>();

            var x = cf.obterValorConexao();
            var testconta = serviceProvider.GetService<ICalcService>();


            testconta.soma(2, 2);

            tracAux.WriteEnd();

        }


        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            IConfigurationRoot configuration;


            // build configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("app-settings.json", optional: false, reloadOnChange: true)
                .Build();



            serviceCollection.AddOptions();

            IConfigurationSection sec = configuration.GetSection("ConfigSBS");
            serviceCollection.Configure<ConfigSBS>(configuration.GetSection("ConfigSBS"));

            var ch = configuration.GetSection("ConfigSBS").Get<ConfigSBS>();

            serviceCollection.Configure<ConfigSBS>(sec);


            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);
            serviceCollection.AddSingleton<IConfiguration>(configuration);


            serviceCollection.AddSerilogServices("log\\consoleapp.log");

            // add services
            serviceCollection.AddTransient<ITestService, TestService>();

            // add services
            serviceCollection.AddTransient<ICalcService, calcService>();

            serviceCollection.AddTransient<ITraceInfra, TracerInfra>();

            serviceCollection.AddTransient<IConfiguracao, Configuracao>();

            // add app
            serviceCollection.AddTransient<AppController>();

            //serviceCollection.addMvc();

        }

        // private static void addConfigSBS(this IServiceCollection services, IConfiguration configuration)
        // {
        //     var section = configuration.GetSection("ConfigSBS");

        //     // we first need to create an instance
        //     var settings = new ConfigSBSSettings();

        //     // then we set the properties 
        //     new ConfigureFromConfigurationOptions<ConfigSBSSettings>(section)
        //         .Configure(settings);
        //     // then we register the instance into the services collection
        //     //services.AddSingleton(new ConfigSBS(settings));

        //     // var _confSBS = configuration.GetSection("ConfigSBSSettings").Get<ConfigSBS>();
        // }

        // private static void configConsole(IOptions<ConfigSBS> arquivoConf)
        // {
        //     arquivoConf
        //     var dd = configuration.GetSection("ConfigSBS");
        //     var conexaoJson = configuration.GetSection("ConfigSBS:persistencia:conexao").Value;

        //     configuration = new ConfigurationBuilder()
        //                     //.SetBasePath(Directory.GetCurrentDirectory())
        //                     .AddJsonFile(conexaoJson, optional: false, reloadOnChange: true)
        //                     .Build();

        // }
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
