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
            var width = inputs.First().Length;

            var tiles = new List<Day3Tile>();
            var index = 0;

            foreach (var tile in inputs.SelectMany(x => x))
            {
                var position = new Position(index % width, index / width);
                switch (tile)
                {
                    case '.':
                        tiles.Add(new OpenSquare(position));
                        break;

                    case '#':
                        tiles.Add(new Tree(position));
                        break;

                    default:
                        throw new InvalidOperationException($"invalid Tile with value '{tile}'.");
                }

                index++;
            }

            return new Grid<Day3Tile>(width, tiles.ToArray());
        }
    }
}