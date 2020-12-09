using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using AdventOfCode_2020.Common;
using FluentAssertions;

namespace AdventOfCode_2020.Week02
{
    public class Day09 : Day00
    {
        public Day09(IServiceProvider serviceProvider, ILogger<Day09> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = Day09ExampleInput.Split("\r\n");

            ValidateDirectInputCases(DirectInput);
            IgnoreDirectInput();
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var (preamble, payload) = inputs.ToTransmission(25);
            var xmas = GetXmaxEncryption(preamble);

            var loop = 0;
            foreach (var data in payload)
            {
                loop++;
                if (!xmas.Add(data))
                {
                    AssertExpectedResult(50047984, data);
                    return $"{data} and doesn't have parts in the preamble.";
                };
            }

            return "no solution";
        }

        protected XmaxEncryption GetXmaxEncryption(long[] preamble)
        {
            var logger = ServiceProvider.GetService<ILogger<XmaxEncryption>>();
            return new XmaxEncryption(preamble, logger);
        }

        private void ValidateDirectInputCases(IEnumerable<string> inputs)
        {
            var (preamble, payload) = inputs.ToTransmission(5);
            var xmas = GetXmaxEncryption(preamble);

            foreach (var data in payload)
            {
                if (!xmas.Add(data))
                {
                    //data.Should().Be(127);
                };
            }

        }

        public const string Day09ExampleInput =
    @"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576";
    }
}
