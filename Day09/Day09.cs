using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using AdventOfCode_2020.Common;

namespace AdventOfCode_2020.Week02
{
    public class Day09 : Day00
    {
        public Day09(IServiceProvider serviceProvider, ILogger<Day09> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = Day09ExampleInput.Split("\r\n");

            ValidateDirectInputCases(DirectInput);
            IgnoreDirectInput(true);
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var (preamble, payload) = inputs.ToTransmission(25);
            var xmas = GetXmasEncryption(preamble);

            foreach (var signal in payload)
            {
                if (!xmas.Add(signal))
                {
                    AssertExpectedResult(50_047_984, signal);
                    return $"{signal} does not contain parts in the preamble.";
                };
            }

            return "no solution";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            long target = 50_047_984;
            var values = inputs.ToLongs().ToArray();
            var xmas = GetXmasEncryption(values);
            xmas.StartWindow(2);

            long sum = 0;
            var loop = 0;

            do
            {
                if (loop++ > 10_000)
                    return "No solution after 10k loops";

                if (sum < target)
                    xmas.Increase();
                else
                    xmas.Decrease();

                sum = xmas.CalculateSum();
                logger.LogTrace($"{sum:N0} {(sum > target ? " > " : " < ")} {target:N0}");
            } while (sum != target);

            var range = xmas.SumRange.GetOffsetAndLength(values.Count());
            var min = values.Skip(range.Offset).Take(range.Length).Min();
            var max = values.Skip(range.Offset).Take(range.Length).Max();
            var solution = min + max;

            AssertExpectedResult(5407707, solution);
            return $"{solution} value found by summing the largest {max} and smallest {min} values between ∑[{xmas.SumRange.Start} .. {xmas.SumRange.End}] = {target}";
        }

        protected XmasEncryption GetXmasEncryption(long[] preamble)
        {
            var logger = ServiceProvider.GetService<ILogger<XmasEncryption>>();
            return new XmasEncryption(preamble, logger);
        }

        private void ValidateDirectInputCases(IEnumerable<string> inputs)
        {
            var (preamble, payload) = inputs.ToTransmission(5);
            var xmas = GetXmasEncryption(preamble);

            foreach (var data in payload)
            {
                if (!xmas.Add(data))
                {
                    AssertExpectedResult(127, data);
                    return;
                };
            }
        }

        public const string Day09ExampleInput = @"35
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
