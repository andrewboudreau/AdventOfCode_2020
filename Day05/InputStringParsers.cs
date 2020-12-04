using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode_2020
{
    public static partial class InputStringParsers
    {
        public static object ToDay05(this IEnumerable<string> inputs)
        {
            return inputs.Any();
        }
    }
}