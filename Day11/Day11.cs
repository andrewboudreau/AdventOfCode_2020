using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode_2020.Week02
{
    public class Day11 : Day00
    {
        public Day11(IServiceProvider serviceProvider, ILogger<Day11> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = Day11ExampleInput.Split("\r\n");

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

        public const string Day11ExampleInput = @"";
    }
}
