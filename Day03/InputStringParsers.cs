using System;
using System.Collections.Generic;
using System.Linq;

using AdventOfCode_2020.Common.DataStructures;

namespace AdventOfCode_2020.Week01
{
    public static partial class InputStringParsers
    {
        public static Grid<Day3Tile> ToDay3Grid(this IEnumerable<string> inputs)
        {
            static Day3Tile tileFactory(Position position, char character)
            {
                return character switch
                {
                    '.' => new OpenSquare(position),
                    '#' => new Tree(position),
                    _ => throw new InvalidOperationException($"invalid Tile with value '{character}'.")
                };
            }

            var grid = inputs.ToGrid(tileFactory);
            return grid;
        }
    }
}