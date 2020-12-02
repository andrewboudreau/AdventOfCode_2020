﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020
{
    public abstract class Day00
    {
        private const string DefaultFileName = "input.dat";
        public const string FullBlock = "█";

        protected readonly ILogger logger;


        public Day00(IServiceProvider serviceProvider, ILogger<Day00> logger)
        {
            ServiceProvider = serviceProvider;
            this.logger = logger;

            IEnumerable<string> defaultLineParser()
            {
                foreach (var input in DirectInput)
                {
                    yield return input;
                }
            }

            DirectInputSingleLineParse = defaultLineParser;
        }

        public int Day => int.Parse(GetType().Name.ToLowerInvariant().Replace("day", ""));

        public virtual string InputFile => $@"Day{Day:00}\\{DefaultFileName}";

        public IServiceProvider ServiceProvider { get; }

        public IEnumerable<string> DirectInput { get; protected set; }

        public Func<IEnumerable<string>> DirectInputSingleLineParse { get; set; }

        public virtual IEnumerable<string> ReadInput()
        {
            if (DirectInput != null)
            {
                foreach (var line in DirectInputSingleLineParse())
                {
                    yield return line;
                }
            }
            else
            {
                var inputFile = Directory.EnumerateFiles(Directory.GetCurrentDirectory(),InputFile, new EnumerationOptions() { RecurseSubdirectories = true }).FirstOrDefault();
                logger.LogInformation($"Using input file {inputFile}");
                using var file = new StreamReader(inputFile);

                string line;
                while ((line = file.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        public void IgnoreDirectInput()
        {
            DirectInput = null;
        }

        public virtual string Solve()
        {
            return Solve(ReadInput());
        }

        public virtual void AssertExpectedResult(int expected, int actual)
        {
            AssertExpectedResult(expected.ToString("N0"), actual.ToString("N0"));
        }

        public virtual void AssertExpectedResult(string expected, string actual)
        {
            if (expected != actual)
            {
                throw new InvalidOperationException($"Expected known correct answer '{expected}' but returned '{actual}'");
            }
        }

        protected abstract string Solve(IEnumerable<string> inputs);

        public virtual string Solve2()
        {
            return Solve2(ReadInput());
        }

        protected virtual string Solve2(IEnumerable<string> inputs)
        {
            return "N/A";
        }

        private static string fileGuid = Guid.NewGuid().ToString("N").Substring(0, 3);

        static protected string SaveToFile(string msg)
        {
            var file = $"out-{fileGuid}.txt";
            File.AppendAllText(file, msg);
            return file;
        }

        static protected string[] ReadFile(string file = null)
        {
            var path = file ?? new DirectoryInfo("./").GetFiles().OrderByDescending(f => f.LastWriteTime).First(x => x.Name.StartsWith("out")).Name;
            return File.ReadAllLines(path);
        }
    }
}