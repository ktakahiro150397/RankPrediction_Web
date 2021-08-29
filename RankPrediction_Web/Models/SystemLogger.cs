using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RankPrediction_Web.Models
{
    public class SystemLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new SystemLogger(categoryName);
        }

        public void Dispose()
        {
        }
    }

    public class SystemLogger : ILogger
    {

        private readonly string CategoryName;

        public SystemLogger(string categoryName)
        {
            CategoryName = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {

            var message = formatter(state, exception);
            var level = (int)logLevel;

            if (exception != null)
            {
                //例外発生時にログを記録する
                var fileName = $"{DateTime.Now.ToString("yyyyMMdd_HHmmss")}_exception.log";
                var fileFullPath = Path.Combine(Path.GetFullPath("./_Log"), fileName);
                using (var sr = new StreamWriter(fileFullPath, true))
                {
                    sr.WriteLine(exception.ToString());
                }
            }
        }
    }
}
