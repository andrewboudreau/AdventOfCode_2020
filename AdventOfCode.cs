using System;
using System.Linq;
using System.Reflection;
using AdventOfCode_2020.Common;
using AdventOfCode_2020.Week01;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020
{
    public class AdventOfCode
    {
        public const LogLevel LoggingLevel = LogLevel.Debug;

        public static Type SolutionForDay = typeof(Day01);

        public static ILoggerFactory LogFactory;

        public static void Main(string[] args)
        {
            using var scope = ConfigureServices().CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var logger = serviceProvider.GetRequiredService<ILogger<AdventOfCode>>();
            var (front, back) = (Console.ForegroundColor, Console.BackgroundColor);

            var day = (Day00)serviceProvider.GetRequiredService(SolutionForDay);


            var part1 = day.Solve();
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Solution:");
            Console.ForegroundColor = front;
            Console.BackgroundColor = back;
            Console.WriteLine(" " + part1);
            Console.WriteLine();

            var part2 = day.Solve2();
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"Solution Part 2:");
            Console.ForegroundColor = front;
            Console.BackgroundColor = back;
            Console.WriteLine(" " + part2);
            Console.WriteLine();

            Console.ReadKey();
        }

        private static ServiceProvider ConfigureServices(ServiceCollection services)
        {
            var singleLineConsoleLoggerProvider = new SingleLineConsoleLogger();
            services.AddLogging(configure =>
            {
                configure.ClearProviders()
                    .AddProvider(singleLineConsoleLoggerProvider)
                    .SetMinimumLevel(LoggingLevel);
            });

            var days = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => type.IsSubclassOf(typeof(Day00)) && !type.IsAbstract);

            foreach (var day in days)
            {
                services.AddTransient(day);
            }

            var serviceProvider = services.BuildServiceProvider();
            LogFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = serviceProvider.GetRequiredService<ILogger<AdventOfCode>>();
            services.AddSingleton(typeof(ILogger), logger);

            return services.BuildServiceProvider();
        }

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            return ConfigureServices(services);
        }
    }
}