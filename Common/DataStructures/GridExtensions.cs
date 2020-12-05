using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode_2020.Common.DataStructures
{
    public static class GridExtensions
    {
        /// <summary>
        /// Enumerates the tiles along the give slope.
        /// </summary>
        public static IEnumerable<T> TilesAlongSlope<T>(this Grid<T> grid, Position slope)
            where T : Tile
        {
            do
            {
                yield return grid.Current;
                grid.MoveBy(slope);
            }
            while (grid.Current != default);
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
