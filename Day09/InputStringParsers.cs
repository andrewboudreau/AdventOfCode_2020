using System.Collections.Generic;
using System.Linq;


namespace AdventOfCode_2020
{
    public static partial class InputStringParsers
    {
        public static IEnumerable<string> ToTransmission(this IEnumerable<string> inputs)
        {
            foreach (var input in inputs)
            {
                yield return input;
            }
        }
    }
}