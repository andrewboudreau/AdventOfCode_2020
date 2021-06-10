using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week02
{
    public class Day14 : Day00
    {
        public Day14(IServiceProvider serviceProvider, ILogger<Day14> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = Day14ExampleInput.Split("\r\n");

            ValidateDirectInputCases(DirectInput);
            IgnoreDirectInput(true);
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var ids = new HashSet<int>();
            var values = new Dictionary<int, ulong>(600);

            var bootrom = inputs.ToValueMaskedBootrom();
            foreach (var section in bootrom.Sections)
            {
                foreach (var write in section.Writes())
                {
                    if (ids.Add(write.Address))
                    {
                        values.Add(write.Address, write.Value);
                    }
                    else
                    {
                        values[write.Address] = write.Value;
                    }
                }
            }

            ulong sum = 0;
            foreach (var x in values.Values)
            {
                sum += x;
            }

            AssertExpectedResult(8570568288597, sum);
            return $"sum is {sum}";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            return "no solution";
            //var memory = new List<ulong>();

            //var bootrom = inputs.ToMemoryDispatchedBootrom();
            //foreach (var section in bootrom.Sections)
            //{
            //    foreach (var write in section.Writes)
            //    {
            //    }
            //}

            //long sum = 123;
            //return $"sum is {sum}";
        }

        private void ValidateDirectInputCases(IEnumerable<string> inputs)
        {
        }

        public const string Day14ExampleInput =
@"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0";
    }
}
