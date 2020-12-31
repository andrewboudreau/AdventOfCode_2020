using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode_2020
{
    public static partial class InputStringParsers
    {
        public static IEnumerable<string> ToDay16Object(this IEnumerable<string> inputs)
        {
            foreach (var item in inputs)
            {
                yield return item;
            }
        }
    }
}