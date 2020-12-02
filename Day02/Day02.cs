using System;
using System.Collections.Generic;
using AdventOfCode_2020.Common;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week01
{
    class Day02 : Day00
    {
        public Day02(IServiceProvider serviceProvider, ILogger<Day02> logger)
            : base(serviceProvider, logger)
        {
            DirectInput =
@"1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc".Split("\r\n");

            IgnoreDirectInput();
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            return null;
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {

            return null;
        }
    }
}