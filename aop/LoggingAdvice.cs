using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Globalization;
using Serilog;
using Microsoft.Extensions.DependencyInjection;

namespace AOP.UsingDispatchProxy
{
    public class LoggingAdvice<T> : DispatchProxy
    {
        private T _decorated;
        private Action<string> _logInfo;
        private Action<string> _logError;
        private Func<object, string> _serializeFunction;
        private TaskScheduler _loggingScheduler;

        // Create new stopwatch.
        private Stopwatch stopwatch = new Stopwatch();
        private DateTime localDate;

        private String cultureName = "pt-BR";
        const string formatoData = "MM/dd/yyyy hh:mm:ss.ffff tt";

        private readonly ILogger _logger;

        public LoggingAdvice()
        {
            var culture = new CultureInfo(cultureName);
        }

        private void LogAdvice(string msg, object arg = null)
        {
            //Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg, arg);
            Log.Information(msg, arg);

            //Console.ResetColor();///
        }
        // private void inicioLog()
        // {
        //     localDate = DateTime.Now;
        //     Log("Start event: {0}", localDate.ToString(formatoData));
        //     stopwatch.Start();

        // }

        // private void fimLog()
        // {
        //     stopwatch.Stop();
        //     localDate = DateTime.Now;
        //     Log("Stop event: {0} ", localDate.ToString(formatoData));
        //     Log("Time elapsed: {0:hh\\:mm\\:ss\\:ffff}", stopwatch.Elapsed);
        // }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            if (targetMethod != null)
            {
                try
                {
                    try
                    {

                        LogBefore(targetMethod, args);
                    }
                    catch (Exception ex)
                    {
                        //Do not stop method execution if exception
                        LogException(ex);
                    }

                    //inicioLog();
                    var result = targetMethod.Invoke(_decorated, args);
                    var resultTask = result as Task;

                    if (resultTask != null)
                    {
                        resultTask.ContinueWith(task =>
                            {
                                if (task.Exception != null)
                                {
                                    LogException(task.Exception.InnerException ?? task.Exception, targetMethod);
                                }
                                else
                                {
                                    object taskResult = null;
                                    if (task.GetType().GetTypeInfo().IsGenericType &&
                                        task.GetType().GetGenericTypeDefinition() == typeof(Task<>))
                                    {
                                        var property = task.GetType().GetTypeInfo().GetProperties()
                                            .FirstOrDefault(p => p.Name == "Result");
                                        if (property != null)
                                        {
                                            taskResult = property.GetValue(task);
                                        }
                                    }

                                    LogAfter(targetMethod, args, taskResult);
                                    //fimLog();
                                }
                            },
                            _loggingScheduler);
                    }
                    else
                    {
                        try
                        {
                            LogAfter(targetMethod, args, result);
                            //fimLog();
                        }
                        catch (Exception ex)
                        {
                            //Do not stop method execution if exception
                            LogException(ex);
                        }
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    if (ex is TargetInvocationException)
                    {
                        LogException(ex.InnerException ?? ex, targetMethod);
                        throw ex.InnerException ?? ex;
                    }
                }
            }

            throw new ArgumentException(nameof(targetMethod));
        }


        public static T Create(T decorated,
            // Action<string> logInfo, Action<string> logError,
            // Func<object, string> serializeFunction, 
            TaskScheduler loggingScheduler = null)
        {
            object proxy = Create<T, LoggingAdvice<T>>();
            // ((LoggingAdvice<T>)proxy).SetParameters(decorated, logInfo, logError, serializeFunction, loggingScheduler);
            ((LoggingAdvice<T>)proxy).SetParameters(decorated, loggingScheduler);


            return (T)proxy;
        }

        private void SetParameters(T decorated,
            //Action<string> logInfo, Action<string> logError,
            //Func<object, string> serializeFunction, 
            TaskScheduler loggingScheduler)
        {
            if (decorated == null)
            {
                throw new ArgumentNullException(nameof(decorated));
            }

            _decorated = decorated;
            //_logInfo = logInfo;
            //_logError = logError;
            //_serializeFunction = serializeFunction;
            _loggingScheduler = loggingScheduler;// ?? TaskScheduler.FromCurrentSynchronizationContext();
        }

        private string GetStringValue(object obj)
        {
            if (obj == null)
            {
                return "null";
            }

            if (obj.GetType().GetTypeInfo().IsPrimitive || obj.GetType().GetTypeInfo().IsEnum || obj is string)
            {
                return obj.ToString();
            }

            try
            {
                return _serializeFunction?.Invoke(obj) ?? obj.ToString();
            }
            catch
            {
                return obj.ToString();
            }
        }

        private void LogException(Exception exception, MethodInfo methodInfo = null)
        {
            try
            {
                // var errorMessage = new StringBuilder();
                // errorMessage.AppendLine($"Class {_decorated.GetType().FullName}");
                // errorMessage.AppendLine($"Method {methodInfo?.Name} threw exception");
                // errorMessage.AppendLine(exception.GetDescription());

                LogAdvice("Class : " + _decorated.GetType().FullName);
                LogAdvice("Method : " + methodInfo.Name);
                LogAdvice("Erro : " + exception.GetDescription());
                //_logError?.Invoke(errorMessage.ToString());
            }
            catch (Exception)
            {
                // ignored
                //Method should return original exception
            }
        }

        private void LogAfter(MethodInfo methodInfo, object[] args, object result)
        {
            //var afterMessage = new StringBuilder();
            //afterMessage.AppendLine($"Class {_decorated.GetType().FullName}");
            //afterMessage.AppendLine($"Method {methodInfo.Name} executed");
            //afterMessage.AppendLine("Output:");
            //afterMessage.AppendLine(GetStringValue(result));
            stopwatch.Stop();
            localDate = DateTime.Now;
            LogAdvice("Stop event: {0} ", localDate.ToString(formatoData));
            LogAdvice("Time elapsed: {0:hh\\:mm\\:ss\\:ffff}", stopwatch.Elapsed);

            // var parameters = methodInfo.GetParameters();
            // if (parameters.Any())
            // {
            //     afterMessage.AppendLine("Parameters:");
            //     for (var i = 0; i < parameters.Length; i++)
            //     {
            //         var parameter = parameters[i];
            //         var arg = args[i];
            //         afterMessage.AppendLine($"{parameter.Name}:{GetStringValue(arg)}");
            //     }
            // }

            //_logInfo?.Invoke(afterMessage.ToString());
        }

        private void LogBefore(MethodInfo methodInfo, object[] args)
        {
            //var beforeMessage = new StringBuilder();
            var nomeClasse = _decorated.GetType().FullName;
            var nomeMetodo = methodInfo.Name;
            localDate = DateTime.Now;
            LogAdvice("Start event: {0}", localDate.ToString(formatoData));
            LogAdvice("Class : " + _decorated.GetType().FullName);
            LogAdvice("Method : " + methodInfo.Name);
            stopwatch.Start();

            //beforeMessage.AppendLine($"Class {_decorated.GetType().FullName}");
            //beforeMessage.AppendLine($"Method {methodInfo.Name} executing");
            // var parameters = methodInfo.GetParameters();
            // if (parameters.Any())
            // {
            //     beforeMessage.AppendLine("Parameters:");

            //     for (var i = 0; i < parameters.Length; i++)
            //     {
            //         var parameter = parameters[i];
            //         var arg = args[i];
            //         beforeMessage.AppendLine($"{parameter.Name}:{GetStringValue(arg)}");
            //     }
            // }

            //_logInfo?.Invoke(beforeMessage.ToString());
        }
    }
}