using System;
using System.Collections.Generic;
using AdventOfCode_2020.Week02;

namespace AdventOfCode_2020
{
    public static partial class InputStringParsers
    {
        public static IEnumerable<RouteStep> ToNavigationInstruction(this IEnumerable<string> inputs)
        {
            char action;
            int value;

            foreach (var item in inputs)
            {
                action = item[0];
                value = int.Parse(item[1..]);

                yield return action switch
                {
                    'N' => new North(value),
                    'S' => new South(value),
                    'E' => new East(value),
                    'W' => new West(value),
                    'L' => new Left(value),
                    'R' => new Right(value),
                    'F' => new Forward(value),
                    _ => throw new NotSupportedException($"Unknown action type '{action}'.")
                };
            }
        }
    }
}