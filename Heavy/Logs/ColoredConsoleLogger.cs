using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Heavy
{
    public class ColoredConsoleLogger : ILogger
    {
        private  string _name;
        private  ColoredConsoleLoggerConfiguration _config;
        public ColoredConsoleLogger(string name, ColoredConsoleLoggerConfiguration config)
        {
            _name = name;
            _config = config;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == _config.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            var color = Console.BackgroundColor;
            Console.ForegroundColor = _config.Color;
            Console.WriteLine($"{logLevel.ToString()} - {_name} - {formatter(state, exception)}");


            Console.WriteLine($"Start************************************************************");


            Debug.WriteLine($"CustomEFLogger {_name} {logLevel} {eventId} {state} start");

            Debug.WriteLine($"异常信息：{exception?.Message}");

            Console.WriteLine($"信息：{formatter.Invoke(state, exception)}");

            Console.WriteLine($"CustomEFLogger {_name} {logLevel} {eventId} {state} end");

            Console.WriteLine($"************************************************************End");

           
            Console.ForegroundColor = color;
        }
    }
}
