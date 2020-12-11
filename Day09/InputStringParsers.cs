using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode_2020
{
    public static partial class InputStringParsers
    {
        public static (long[] Preamble, long[] Payload) ToTransmission(this IEnumerable<string> inputs, int preamble)
        {
            return (
                inputs.Take(preamble).Select(long.Parse).ToArray(),
                inputs.Skip(preamble).Select(long.Parse).ToArray());
        }
    }
}