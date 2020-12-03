using System.Collections.Generic;

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
    }
}
