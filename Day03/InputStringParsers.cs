using System;
using System.Collections.Generic;

using AdventOfCode_2020.Common.DataStructures;

namespace AdventOfCode_2020.Week01
{
    public static partial class InputStringParsers
    {

        public static Grid<GroundTile> ToMapOfTrees(this IEnumerable<string> inputs)
        {
            var grid = inputs.ToGrid(TileFactory);
            return grid;
        }

        private static GroundTile TileFactory(Position position, char character)
        {
            return character switch
            {
                '.' => new OpenSquare(position),
                '#' => new Tree(position),
                _ => throw new InvalidOperationException($"invalid Tile with value '{character}'.")
            };
        }
    }
}