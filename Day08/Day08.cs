using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week01
{
    public class Day08 : Day00
    {
        public Day08(IServiceProvider serviceProvider, ILogger<Day08> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = Day08ExampleInput.Split("\r\n");

            ValidateDirectInputCases(DirectInput);
            IgnoreDirectInput(false);
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var foo = inputs.ToDay08Object();
            return "N/A";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            return base.Solve2(inputs);
        }

        private void ValidateDirectInputCases(IEnumerable<string> inputs)
        {
        }

        public const string Day08ExampleInput =
    @"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6";
    }
}
