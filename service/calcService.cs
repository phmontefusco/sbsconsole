//using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sinqia.Framework.Model;
//using Serilog;
//using SerilogMetrics;
//using SerilogTimings;
//using System.Diagnostics;
using Sinqia.Framework.Services;

namespace sbsconsole.services
{
    public interface ICalcService
    {
        void soma(int a, int b);
    }

    public class calcService : ICalcService
    {
        //private readonly ILogger<calc> _logger;
        //private readonly ILogger _logger;
        private readonly ITraceInfra _tracerInfra;

        //public calc(ILogger<calc> logger)
        public calcService(IServiceProvider service)
        {

            //_logger = service.GetRequiredService<ILogger>().ForContext<calcService>();
            _tracerInfra = service.GetRequiredService<ITraceInfra>();

        }
        public void soma(int a, int b)
        {
            _tracerInfra.WriteStart("INICIO COM TRACER");
            //_logger.
            //var xx = Operation.Begin("teste time2");
            //var met = new StackFrame(1).GetMethod().Name;

            //_logger.Information("Inicio Rotina : {0}", met.ToString());
            _tracerInfra.WriteParameters(() => a, () => b);
            //_tracerInfra.Write("log debug {0} + {1}", a.ToString(), b.ToString());
            _tracerInfra.Write("Rotina de Calculo : {0}", a + b);
            var valor2 = soma2(a, b + 5);
            _tracerInfra.Write("Rotina de Calculo apos soma 2: {0}", valor2);
            //System.Threading.Thread.Sleep(2000);
            _tracerInfra.WriteEnd();

        }

        public int soma2(int a, int b)
        {
            var valor3 = a + b * 3;
            _tracerInfra.WriteStart("soma 2");
            _tracerInfra.WriteParameters(() => a, () => b);
            multi1(valor3, a, b);

            //System.Threading.Thread.Sleep(2000);
            _tracerInfra.WriteEnd();
            return valor3;

        }

        public int multi1(int mult, int a, int b)
        {
            var valor4 = mult * (a + b * 3);
            _tracerInfra.WriteStart("soma 2");
            _tracerInfra.WriteParameters(() => mult, () => a, () => b);

            //System.Threading.Thread.Sleep(2000);
            _tracerInfra.WriteEnd();
            return valor4;

        }

    }
}