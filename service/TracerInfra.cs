using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
// using System.IO;
using System.Reflection;
// using System.Security;
// using System.Text;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;
//using SerilogMetrics;
//using SerilogTimings;

namespace Sinqia.Framework.Services
{
    /// <summary>
    /// A tracing class with enhanced funtionality.
    /// </summary>
    public class TracerInfra : ITraceInfra
    {

        private readonly ILogger _logger;
        private const string LambdaMethod = "lambda_method";
        private static char identationSymbol = ' ';
        public static bool TrackTime = true;

        // public static int IndentSize = 2;

        // public static bool ShowDateTime = true;

        // public static bool ShowCounter = true;

        // public static bool ShowThreadID = true;

        public static Stack _stack = new Stack();
        public static Stack _perfStack = new Stack();
        public static string _indent = "";
        // public const string _emptyPerfTime = "           ";
        // public static long _perfFrequency = 0;

        public TracerInfra(IServiceProvider service)
        {
            try
            {
                _logger = service.GetRequiredService<ILogger>();
            }
            catch { }
        }


        public void WriteStart(string text)
        {
            StackFrame frame = new StackFrame(1);
            MethodBase mi = frame.GetMethod();
            string assemblyName = mi.DeclaringType != null ? "AssemblyIsUndetectable" : assemblyName = mi.DeclaringType.Assembly.GetName().ToString().Split(',')[0];
            string className = mi.DeclaringType != null ? mi.DeclaringType.FullName : "ClassIsUndetectable";
            string methodName = mi.DeclaringType != null ? mi.Name : "MethodIsUndetectable";
            // string parametros = "";

            if (mi.DeclaringType == null && mi.Name == LambdaMethod)
            {
                frame = new StackFrame(3);
                mi = frame.GetMethod();
            }

            _indent = new string(identationSymbol, _stack.Count * 2);

            _stack.Push(className + "." + methodName);
            DateTime tempo1 = DateTime.Now;
            _perfStack.Push(tempo1);
            Write("Inicio Rotina " + className + "." + methodName);
            Write("Hora Inicial : {0:MM/dd/yyy HH:mm:ss.fff}}", tempo1);

        }

        public void WriteEnd()
        {
            StackFrame frame = new StackFrame(2);

            if (frame.GetFileName() == null)
            {
                frame = new StackFrame(1);
            }
            MethodBase mi = frame.GetMethod();
            string assemblyName = "AssemblyIsUndetectable";
            string className = mi.DeclaringType != null ? mi.DeclaringType.FullName : "ClassIsUndetectable";
            string methodName = mi.DeclaringType != null ? mi.Name : "MethodIsUndetectable";



            if (mi.DeclaringType == null && mi.Name == LambdaMethod)
            {
                frame = new StackFrame(1);
                mi = frame.GetMethod();
            }

            if (mi.DeclaringType != null)
            {
                assemblyName = mi.DeclaringType.Assembly.GetName().ToString().Split(',')[0];
                methodName = mi.Name;
            }

            /* ALT:16/04/2007-ATV:7030309-RM:014506-{{INICIO}} */
            //if (!CanTrace(assemblyName)) return;
            /* ALT:16/04/2007-ATV:7030309-RM:014506-{{FIM}} */

            try
            {

                DateTime tempoFinal = new DateTime();
                // if ((_indent.Length - IndentSize) > 0)
                //     _indent = new string(' ', _indent.Length - IndentSize);
                // else
                //     _indent = " ";


                var rotina = (string)_stack.Pop();
                var _perfTime = "";
                _indent = new string(identationSymbol, _stack.Count * 2);

                if (TrackTime)
                {
                    DateTime tempoInicial = (DateTime)_perfStack.Pop();
                    tempoFinal = DateTime.Now;

                    TimeSpan span = tempoFinal - tempoInicial;
                    float ms = (float)span.TotalMilliseconds;
                    _perfTime = string.Format("{0:0000.000}ms ", ms);
                }
                Write("Final Rotina " + rotina);
                Write("Hora Final : {0:MM/dd/yyy HH:mm:ss.fff}}", tempoFinal);
                Write("Tempo Rotina : {0}", _perfTime);

            }
            catch { }
        }

        public void Write(string text, params object[] arg)
        {
            try
            {
                _logger.Information(_indent + text, arg);
            }
            catch (Exception)
            {
                //LogInfra.gravarErro(e);
            }
        }


        public void WriteParameters(params Expression<Func<object>>[] providedParameters)
        {

            var paramList = new ParamLogUtility(providedParameters).GetLog();
            Write("Lista de Parametros " + paramList);

        }
    }


    internal class ParamLogUtility
    {
        private readonly String _methodName;
        private String _paramaterLog;

        private readonly Dictionary<String, Type> _methodParamaters;
        private readonly List<Tuple<String, Type, object>> _providedParametars;

        public ParamLogUtility(params Expression<Func<object>>[] providedParameters)
        {
            try
            {
                var currentMethod = new StackTrace().GetFrame(2).GetMethod();

                /*Set class and current method info*/
                _methodName = String.Format("Metodo = {0}",
                 currentMethod.Name);

                /*Get current methods parameters*/
                _methodParamaters = new Dictionary<string, Type>();
                (from aParamater in currentMethod.GetParameters()
                 select new { Name = aParamater.Name, DataType = aParamater.ParameterType })
                    .ToList()
                    .ForEach(obj => _methodParamaters.Add(obj.Name, obj.DataType));

                /*Get provided methods parameters*/
                _providedParametars = new List<Tuple<string, Type, object>>();
                foreach (var aExpression in providedParameters)
                {
                    Expression bodyType = aExpression.Body;
                    if (bodyType is MemberExpression)
                    {
                        AddProvidedParamaterDetail((MemberExpression)aExpression.Body);
                    }
                    else if (bodyType is UnaryExpression)
                    {
                        UnaryExpression unaryExpression = (UnaryExpression)aExpression.Body;
                        AddProvidedParamaterDetail((MemberExpression)unaryExpression.Operand);
                    }
                    else
                    {
                        throw new Exception("Expression type unknown.");
                    }
                }

                /*Process log for all method parameters*/
                ProcessLog();
            }
            catch (Exception exception)
            {
                throw new Exception("Error in paramater log processing.", exception);
            }
        }
        private void ProcessLog()
        {
            try
            {

                foreach (var aMethodParamater in _methodParamaters)
                {
                    var aParameter =
                        _providedParametars.Where(
                            obj => obj.Item1.Equals(aMethodParamater.Key) &&
                    obj.Item2 == aMethodParamater.Value).Single();
                    _paramaterLog += String.Format(@" ""{0}"":{1},",
                aParameter.Item1, JsonConvert.SerializeObject(aParameter.Item3));
                }
                _paramaterLog = (_paramaterLog != null) ? _paramaterLog.Trim(' ', ',') : string.Empty;
            }
            catch (Exception e)
            {
                throw new Exception("MathodParamater is not found in providedParameters." + e.Message);
            }
        }

        private void AddProvidedParamaterDetail(MemberExpression memberExpression)
        {
            ConstantExpression constantExpression = (ConstantExpression)memberExpression.Expression;
            var name = memberExpression.Member.Name;
            var value = ((FieldInfo)memberExpression.Member).GetValue(constantExpression.Value);
            var type = value.GetType();
            _providedParametars.Add(new Tuple<string, Type, object>(name, type, value));
        }

        public String GetLog()
        {
            return String.Format("{0}({1})", _methodName, _paramaterLog);
        }
    }

    public interface ITraceInfra
    {

        void WriteStart(string text);

        void WriteEnd();

        void Write(string text, params object[] arg);

        void WriteParameters(params Expression<Func<object>>[] providedParameters);

    }
}