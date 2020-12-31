using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week03
{
    public class Day15 : Day00
    {
        public Day15(IServiceProvider serviceProvider, ILogger<Day15> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = "18,8,0,5,4,1,20".Split(',');
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            return "no solution";
        }
    }
}
