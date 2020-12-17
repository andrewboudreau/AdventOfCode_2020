using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;
using AdventOfCode_2020.Common;

namespace AdventOfCode_2020.Week02
{
    public class Day10 : Day00
    {
        public Day10(IServiceProvider serviceProvider, ILogger<Day10> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = Day10ExampleInput.Split("\r\n");
            IgnoreDirectInput();
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var adapters = inputs.ToInts().OrderBy(x => x).ToList();

            var counts = new int[4];
            counts[3] += 1; // internal device adapter is always +3 joltage difference.
            var joltage = 0;
            foreach (var adapter in adapters)
            {
                var difference = adapter - joltage;
                counts[difference] += 1;
                joltage += difference;
            }

            var solution = counts[1] * counts[3];
            AssertExpectedResult(1656, solution);

            return $"{solution} found by multiplying the count of 1 and 3 difference joltage. Total combinations is. Difference 1:{counts[1]} 2:{counts[2]} 3:{counts[3]}";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var adapters = inputs.ToInts().Append(0).OrderBy(x => x).ToList();

            long total = 1;
            int branches = 0;
            var joltage = 0;

            for (var index = 1; index < adapters.Count; index++)
            {
                var choices = adapters.Skip(index).Count(x => x - joltage <= 3);
                joltage += adapters[index] - joltage;
                branches++;

                if (choices == 1)
                {
                    total *= branches switch
                    {
                        1 => 1,
                        2 => 2,
                        3 => 4,
                        4 => 7,
                        _ => throw new InvalidOperationException($"{branches} is an invalid length")
                    };

                    logger.LogTrace($"Running Total : {total}");
                    branches = 0;
                }
            }

            AssertExpectedResult(56693912375296, total);
            return $"{total} possible adapter stacking options";
        }

        public const string Day10ExampleInput = @"16
10
15
5
1
11
7
19
6
12
4";

        public const string Day10ExampleInput2 = @"28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3";
    }
}
