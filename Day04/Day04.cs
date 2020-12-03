using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AdventOfCode_2020.Common.DataStructures;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week01
{
    public class Day04 : Day00
    {
        public Day04(IServiceProvider serviceProvider, ILogger<Day04> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = @"".Split("\r\n");

            //IgnoreDirectInput();
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            return $"N/A";
        }
    }
}