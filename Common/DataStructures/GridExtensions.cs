using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode_2020.Common.DataStructures
{
    public static class GridExtensions
    {
        /// <summary>
        /// Enumerates the tiles along the give slope. Starting at the grid's Current location.
        /// </summary>
        public static IEnumerable<T> TilesAlongSlope<T>(this Grid<T> grid, Position slope)
            where T : Tile
        {
            while (grid.MoveBy(slope) != default)
            {
                yield return grid.Current;
            }
        }

        /// <summary>
        /// Enumerates the tiles along the give slope.
        /// </summary>
        public static IEnumerable<T> TilesAlongSlope<T>(this Grid<T> grid, Position slope, Position from)
            where T : Tile
        {
            var current = from;
            while (!grid.IsOutOfBounds(current += slope))
            {
                yield return grid[current];
            }
        }

        /// <summary>
        /// Enumerates the neighbor tiles.
        /// </summary>
        public static IEnumerable<T> AdjacentTo<T>(this Grid<T> grid, Position seat)
            where T : Tile
        {
            var options = new Position[] {
                new(-1,-1),
                new( 0,-1),
                new( 1,-1),
                new(-1, 0),
                new( 1, 0),
                new(-1, 1),
                new( 0, 1),
                new( 1, 1),
            };

            foreach (var option in options)
            {
                var result = grid[seat + option];
                if (result != default)
                {
                    yield return result;
                }
            }
        }

        /// <summary>
        /// Enumerates the neighbor tiles.
        /// </summary>
        public static IEnumerable<IEnumerable<T>> AdjacentInAllDirections<T>(this Grid<T> grid, Position from)
            where T : Tile
        {
            var slopes = new Position[8] {
                new(-1,-1),
                new( 0,-1),
                new( 1,-1),
                new(-1, 0),
                new( 1, 0),
                new(-1, 1),
                new( 0, 1),
                new( 1, 1),
            };

            foreach (var slope in slopes)
            {
                yield return grid.TilesAlongSlope(slope, from);
            }
        }

        public static void Print<T>(this Grid<T> grid, Action<string> output, bool renderValue = false)
           where T : Tile
        {
            foreach (var row in grid.GetRows())
            {
                output(string.Join("", row.Select(r => renderValue ? (r.Display == '.' ? r.Display : r.Value.ToString().First()) : r.Display)));
            }
        }

        public static Grid<T> ToGrid<T>(this IEnumerable<string> inputs, Func<Position, char, T> tileFactory)
            where T : Tile
        {
            var width = inputs.First().Length;

            var tiles = new List<T>();
            var index = 0;

            foreach (var tileType in inputs.SelectMany(x => x))
            {
                var position = new Position(index % width, index / width);
                tiles.Add(tileFactory(position, tileType));

                index++;
            }

            return new Grid<T>(width, tiles.ToArray());
        }
    }
}
