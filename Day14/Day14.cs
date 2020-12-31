using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Numerics;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week02
{
    public class Day14 : Day00
    {
        public static ulong[] Memory = new ulong[65_600];

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

            var program = inputs.ToDay14Object();
            foreach (var section in program.Sections)
            {
                foreach (var write in section.Writes)
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
