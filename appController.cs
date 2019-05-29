using Microsoft.Extensions.Options;
using sbsconsole.services;
using Sinqia.Framework.Model;
// using Microsoft.Extensions.Logging.Debug;
// using Microsoft.Extensions.Logging.Console;
using Serilog;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace sbsconsole
{
    public class AppController
    {
        private readonly ITestService _testService;
        //private readonly ILogger<AppController> _logger;
        private readonly ILogger _logger;
        private readonly IOptions<ConfigSBS> _config;
        private readonly ICalcService _calc;

        public AppController(IServiceProvider service)
        {
            _testService = service.GetRequiredService<ITestService>(); //testService;
            _logger = service.GetRequiredService<ILogger>(); //logger;
            _config = service.GetRequiredService<IOptions<ConfigSBS>>(); // config.Value;
            _calc = service.GetRequiredService<ICalcService>();
        }

        public void Run()
        {
            //_logger.LogInformation($"This is a console application for {_config.ConsoleTitle}");
            //_testService.Run();
            Log.Information("Implementação auxiliar");
            _calc.soma(2, 2);

            //_calc.soma(4, 8);
        }
    }
}