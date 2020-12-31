using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using AdventOfCode_2020.Common;
using System.Runtime.CompilerServices;

namespace AdventOfCode_2020.Week02
{
    public class Day13 : Day00
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Check(long test, int value) => test % value == 0;

        public Day13(IServiceProvider serviceProvider, ILogger<Day13> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = Day13ExampleInput.Split("\r\n");
            ValidateDirectInput();
            IgnoreDirectInput();
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var depart = int.Parse(inputs.First());
            var cycles = inputs.Skip(1).First().Split(',').Where(x => x != "x").ToInts().ToList();

            var answers = new List<(int Cycle, int Departs)>();
            foreach (var cycle in cycles)
            {
                var t = depart;
                while (t % cycle != 0)
                {
                    t++;
                }

                answers.Add((cycle, t));
            }

            var soonest = answers.Min(x => x.Departs);
            var busId = answers.First(x => x.Departs == soonest).Cycle;
            var waitTime = soonest - depart;
            var solution = busId * waitTime;

            AssertExpectedResult(115, solution);
            AssertExpectedResult(solution, waitTime * busId);

            return $"Answer: {busId}*{waitTime}={solution}. From bus {busId} at {soonest} is the soonest you can leave. You wait for {waitTime}.";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var cycles = inputs.Skip(1).First().Split(',').ToList();
            var equations = new List<(int Offset, int Bus)>(10);

            foreach (var (Value, Index) in cycles.Select((value, index) => (value, index)))
            {
                if (Value == "x")
                {
                    continue;
                }

                equations.Add((Index, int.Parse(Value)));
            }

            long step = 1;
            long time = 100_000_000_000_000;

            foreach (var (Offset, Bus) in equations)
            {
                while ((time + Offset) % Bus != 0)
                {
                    time += step;
                }

                step *= Bus;
            }

            AssertExpectedResult(756261495958122, time);
            return $"{time} works";
        }

        void ValidateDirectInput()
        {
            int test = 100_000;
            while (true)
            {
                test += 1;
                if (
                    Check(test, 7) &&
                    Check(test + 1, 13) &&
                    Check(test + 4, 59) &&
                    Check(test + 6, 31) &&
                    Check(test + 7, 19))
                {
                    AssertExpectedResult(1068781, test);
                    return;
                }
            }
        }

        bool CheckPart2(long test)
        {
            return
                Check(test, 41) &&
                Check(test + 35, 37) &&
                Check(test + 41, 659) &&
                Check(test + 49, 23) &&
                Check(test + 54, 13) &&
                Check(test + 60, 19) &&
                Check(test + 70, 29) &&
                Check(test + 72, 937) &&
                Check(test + 896, 17);
        }

        public const string Day13ExampleInput =
@"939
7,13,x,x,59,x,31,19";
    }
}
