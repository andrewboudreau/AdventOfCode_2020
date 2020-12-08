using System.Collections.Generic;
using System.Linq;

using AdventOfCode_2020.Common;

namespace AdventOfCode_2020
{
    public static partial class InputStringParsers
    {
        public static IEnumerable<string> ToDay08Object(this IEnumerable<string> inputs)
        {
            foreach (var input in inputs)
            {
                yield return input;
            }
        }
    }
}