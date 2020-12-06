using System;
using System.Collections.Generic;
using System.Linq;

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

            _ = Convert.ToInt32(binary, 2);
            _ = BinaryToIntegerWithLinqAggregate(binary);
            _ = BinaryToIntegerWithForLoop(binary);

            return Convert.ToInt32(binary, 2);
        }

        public static int BinaryToIntegerWithForLoop(this string encoded)
        {
            var value = 0;
            var upperBound = encoded.Length - 1;

            for (var index = upperBound; index >= 0; index--)
            {
                if (encoded[upperBound - index] == '1')
                {
                    value += (int)Math.Pow(2, index);
                }
            }

            return value;
        }
        public static int BinaryToIntegerWithLinqAggregate(this string encoded)
        {
            var place = encoded.Length - 1;
            return encoded.Aggregate(0,
                (acc, character) =>
                {
                    if (character == '1')
                    {
                        acc += (int)Math.Pow(2, place);
                    }

                    place--;
                    return acc;
                });
        }
    }
}