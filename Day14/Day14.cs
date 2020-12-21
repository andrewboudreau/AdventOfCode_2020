using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode_2020.Week02
{
    public class Day14 : Day00
    {
        public Day14(IServiceProvider serviceProvider, ILogger<Day14> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = Day14ExampleInput.Split("\r\n");

            ValidateDirectInputCases(DirectInput);
            IgnoreDirectInput(false);
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            return "no solution";
        }


        private void ValidateDirectInputCases(IEnumerable<string> inputs)
        {
        }

        public const string Day14ExampleInput =
@"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0";
    }
}
