using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week01
{
    public record Tree() : TreeTile(1, nameof(Tree));
    public record OpenSquare() : TreeTile(0, nameof(OpenSquare));
    public record TreeTile(int TreeCount, string Name) : TileType(Name);

    public class Day03 : Day00
    {
        public Day03(IServiceProvider serviceProvider, ILogger<Day03> logger)
            : base(serviceProvider, logger)
        {
            DirectInput =
@"..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#".Split("\r\n");

            IgnoreDirectInput();
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            // start by counting all the trees you would encounter for the slope right 3, down 1.
            var map = inputs.ToMap();
            var slope = new Position(3, 1);
            var trees = 0;

            do
            {
                var tile = map.Current.Type as TreeTile;
                trees += tile.TreeCount;
                map.MoveBy(slope);
            }
            while (map.Current != default);

            return $"{trees} trees on the slope right:{slope.X}, down:{slope.Y}.";
        }

        protected override string Solve2(IEnumerable<string> inputs)
    }

    public static class InputStringParsers
    {
        public static Map<Tile> ToMap(this IEnumerable<string> inputs)
        {
            var width = inputs.First().Length;

            var tiles = new List<Tile>();
            var index = 0;

            foreach (var tile in inputs.SelectMany(x => x))
            {
                var position = new Position(index % width, index / width);
                switch (tile)
                {
                    case '.':
                        tiles.Add(new Tile(position, new OpenSquare()));
                        break;

                    case '#':
                        tiles.Add(new Tile(position, new Tree()));
                        break;

                    default:
                        throw new InvalidOperationException($"invalid Tile with value '{tile}'.");
                }

                index++;
            }

            return new Map<Tile>(width, tiles.ToArray());
        }
    }
}