//using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sinqia.Framework.Model;
using Serilog;

namespace sbsconsole.services
{
    public interface ITestService
    {
        void Run();
    }

    public class TestService : ITestService
    {
        private readonly ILogger _logger;
        //private readonly ConfigSBS _config;

        public TestService(ILogger logger)//,
                                          //IOptions<ConfigSBS> config)
        {
            _logger = logger;
            //_config = config.Value;
        }
        public void Run()
        {
            var myLog = Log.ForContext<TestService>();
            myLog.Information("teste");
            // _logger.Warning("Implementação Basica");
            // _logger.Warning($"Wow! We are now in the test service of: {_config.ConsoleTitle}");
        }
    }
}