using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode_2020.Week02
{
    public class Day13 : Day00
    {
        public Day13(IServiceProvider serviceProvider, ILogger<Day13> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = Day13ExampleInput.Split("\r\n");

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

        public const string Day13ExampleInput = 
@"";
    }
}
