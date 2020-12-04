using System;

using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Common
{
    public sealed class SingleLineConsoleLogger : ILoggerProvider
    {
        public void Dispose() { }

        public ILogger CreateLogger(string categoryName)
        {
            return new CustomConsoleLogger(categoryName);
        }

        public class CustomConsoleLogger : ILogger
        {
#pragma warning disable IDE0052 // Remove unread private members
            private readonly string categoryName;
#pragma warning restore IDE0052 // Remove unread private members

            public CustomConsoleLogger(string categoryName)
            {
                this.categoryName = categoryName;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                if (!IsEnabled(logLevel))
                {
                    return;
                }

                var (fore, back) = (Console.ForegroundColor, Console.BackgroundColor);
                if (logLevel == LogLevel.Critical)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.WriteLine($"{logLevel.ToString().Substring(0, 4)}: {formatter(state, exception)}");

                Console.ForegroundColor = fore;
                Console.BackgroundColor = back;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }
        }
    }
}
