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
            IgnoreDirectInput(false);
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
            //AssertExpectedResult(1656, solution);

            return $"{solution} found by multiplying the count of 1 and 3 difference joltage. Difference 1:{counts[1]} 2:{counts[1]} 3:{counts[1]}";
        }
        protected override string Solve2(IEnumerable<string> inputs)
        {
            var adapters = inputs.ToInts().OrderBy(x => x).ToList();
            long total = 0;
            var loop = 0;
            var branches = 0;
            Console.WriteLine();
            int depthFirst(int joltage, IEnumerable<int> remaining)
            {

                if (remaining.IsEmpty())
                {
                    return 1;
                }

                Console.WriteLine($"Loop:{loop} remaining:{string.Join(',', remaining.Select(x => x.ToString()))} ");


                var options = remaining
                    .TakeWhile(x => x - joltage < 4)
                    .Select((Value, Index) => (Value, Index + 1))
                    .ToList();

                if (options.Count > 1)
                    branches++;

                if (!options.Any()) throw new Exception("not right");

                if(options.Count > 1)
                {
                    foreach (var (Value, Offset) in options)
                    {
                        total += depthFirst(joltage + Value, remaining.Skip(Offset).ToList());
                    }
                }
                

                return 0;
            }

            var solution = depthFirst(0, adapters);
            return $"{total} {solution} possible adapter stacking.";
            // 214 is too low.
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
