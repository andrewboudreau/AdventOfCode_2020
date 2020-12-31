using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode_2020.Week03
{
    public class Day16 : Day00
    {
        public Day16(IServiceProvider serviceProvider, ILogger<Day16> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = Day16ExampleInput.Split("\r\n");

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

        public const string Day16ExampleInput =
@"class: 1-3 or 5-7
row: 6-11 or 33-44
seat: 13-40 or 45-50

your ticket:
7,1,14

nearby tickets:
7,3,47
40,4,50
55,2,20
38,6,12";
    }
}
