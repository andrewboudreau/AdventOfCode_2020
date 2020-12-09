using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

using AdventOfCode_2020.Common;

namespace AdventOfCode_2020.Week01
{
    public class Day09 : Day00
    {
        public Day09(IServiceProvider serviceProvider, ILogger<Day09> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = Day09ExampleInput.Split("\r\n");

            ValidateDirectInputCases(DirectInput);
            IgnoreDirectInput(true);
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var data = inputs.ToTransmission();
            return "no solution";
        }

        private void ValidateDirectInputCases(IEnumerable<string> inputs)
        {
        }

        public const string Day09ExampleInput =
    @"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576";
    }
}
