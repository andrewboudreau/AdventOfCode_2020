using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using AdventOfCode_2020.Common;
using AdventOfCode_2020.Week01;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020
{
    using static AdventOfCode_2020.Common.ConsoleColorStack;
    using static AdventOfCode_2020.Common.FiggleFontExtensions;
    using static AdventOfCode_2020.Common.DisposableConsoleColor;

    public class AdventOfCode
    {
        public const LogLevel LoggingLevel = LogLevel.Debug;

        public static Type SolutionForDay { get; set; } = typeof(Day07);

        public static ILoggerFactory LogFactory { get; private set; }

        public static void Main(string[] args)
        {
            if (args.Any())
            {
                Console.WriteLine(string.Join(" ", args));
            }

            using var scope = ConfigureServices().CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var logger = serviceProvider.GetRequiredService<ILogger<AdventOfCode>>();
            var day = (Day00)serviceProvider.GetRequiredService(SolutionForDay);

            Console.WriteLine(Figgle.FiggleFonts.Doom.Render(day.Title));
            //Console.WriteLine(RandomFiggleFont().Render(day.Title));

            var part1 = day.Solve();
            Push(ConsoleColor.White, ConsoleColor.DarkGray);
            Console.Write($"Solution:");
            Pop();

            Console.WriteLine(" " + part1);
            Console.WriteLine();

            var part2 = day.Solve2();
            Push(ConsoleColor.Cyan, ConsoleColor.DarkGray);
            Console.Write($"Solution Part 2:");
            Pop();

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