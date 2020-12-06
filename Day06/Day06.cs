using System;
using System.Collections.Generic;
using System.Linq;

using AdventOfCode_2020.Common;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week01
{
    public class Day06 : Day00
    {
        public Day06(IServiceProvider serviceProvider, ILogger<Day06> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = @"".Split("\r\n");

            //ValidateDirectInputCases(DirectInput);
            //IgnoreDirectInput();
        }

        protected override string Solve(IEnumerable<string> inputs)
        {

            return $"";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            return base.Solve2(inputs);
        }

        private void ValidateDirectInputCases(IEnumerable<string> inputs)
        {
            var data = inputs.ToDay06();
        }
    }
}