using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode_2020.Week02
{
    public class Day12 : Day00
    {
        public Day12(IServiceProvider serviceProvider, ILogger<Day12> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = Day12ExampleInput.Split("\r\n");

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

        public const string Day12ExampleInput = 
@"";
    }
}
