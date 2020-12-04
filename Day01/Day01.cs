using System;
using System.Collections.Generic;
using System.Linq;

using AdventOfCode_2020.Common;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week01
{
    class Day01 : Day00
    {
        public Day01(IServiceProvider serviceProvider, ILogger<Day01> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = @"1721
 979
 366
 299
 675
1456".Split("\r\n");

            IgnoreDirectInput();
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var values = inputs.ToHashSet();
            var (first, second) = FindTwoNumbersThatSumToValue(values);

            return $"Found the values {first:N0} & {second:N0} which have a product of {first * second:N0}.";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var values = inputs.ToHashSet();
            foreach (var value in values)
            {
                var result = FindTwoNumbersThatSumToValue(values.Except(new[] { value }).ToHashSet(), 2020 - value);
                if (result != default)
                {
                    var (first, second) = result;
                    return $"Found the values {value:N0}, {first:N0}, {second:N0} have a product of {value * first * second:N0}.";
                }
            }

            throw new InvalidOperationException("Could not solve.");
        }

        private static (int, int) FindTwoNumbersThatSumToValue(HashSet<int> values, int target = 2020)
        {
            foreach (var value in values)
            {
                if (values.Contains(target - value))
                {
                    return (value, target - value);
                }
            }

            return default;
        }
    }
}