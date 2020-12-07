using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week01
{
    public class Day07 : Day00
    {
        public Day07(IServiceProvider serviceProvider, ILogger<Day07> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = Day07ExampleInput.Split("\r\n");

            ValidateDirectInputCases(DirectInput);
            IgnoreDirectInput(false);
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            return "N/A";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            return base.Solve2(inputs);
        }

        private void ValidateDirectInputCases(IEnumerable<string> inputs)
        {
            var data = inputs.ToDay07Object().Count();
            AssertExpectedResult(5, 5);
            AssertExpectedResult(11, 11);
        }

        public const string Day07ExampleInput =
    @"";
    }
}