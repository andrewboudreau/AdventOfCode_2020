using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week01
{
    public class Day05 : Day00
    {
        public Day05(IServiceProvider serviceProvider, ILogger<Day05> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = @"".Split("\r\n");

            //IgnoreDirectInput();
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            return "N/A";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            return base.Solve2(inputs);
        }
    }
}