using System;
using System.Collections.Generic;

namespace AdventOfCode_2020
{
    public static partial class InputStringParsers
    {
        public static IEnumerable<BoardingPass> ToBoardingPasses(this IEnumerable<string> inputs)
        {
            foreach (var encoded in inputs)
            {
                // take the first seven bits and create the binary version of that value
                var row = InputToInt(encoded[..7]);

                // take the last 3 bits and create the binary version of that value
                var column = InputToInt(encoded[7..]);

                yield return new BoardingPass(row, column);
            }
        }

        public static int InputToInt(this string encoded)
        {
            var binary = encoded.Replace("F", "0").Replace("B", "1").Replace("L", "0").Replace("R", "1");
            return Convert.ToInt32(binary, 2);
        }
    }
}