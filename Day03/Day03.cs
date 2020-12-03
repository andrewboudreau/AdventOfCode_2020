using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode_2020.Common.DataStructures;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week01
{
    /// <summary>
    /// Solutions for https://adventofcode.com/2020/day/3 parts 1 and 2.
    /// </summary>
    public class Day03 : Day00
    {
        public Day03(IServiceProvider serviceProvider, ILogger<Day03> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = @"..##.......
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
            var grid = inputs.ToDay3Grid();
            var slope = new Position(3, 1);

            var trees = grid
                .TilesAlongSlope(slope)
                .Sum(x => x.TreeCount);

            return $"{trees} trees on the slope right:{slope.X}, down:{slope.Y}.";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var grid = inputs.ToDay3Grid();

            // Slopes from adventofcode.com/2020/day/3#part2
            var slopes = new Position[] {
                new(1, 1),
                new(3, 1),
                new(5, 1),
                new(7, 1),
                new(1, 2)
            };

            var product = 1;
            foreach (var slope in slopes)
            {
                var trees = grid
                    .TilesAlongSlope(slope)
                    .Sum(x => x.TreeCount);

                product *= trees;
                grid.MoveToStart();

                logger.LogInformation($"{trees} trees found on {slope}.");
            }

            return $"{product} is the product of the tree counts for the {slopes.Length} slopes.";
        }

        /// <summary>
        /// Same solution but more LINQ.
        /// </summary>
        protected static int Linq_Solve2(Grid<Day3Tile> grid, Position[] slopes)
        {
            int CountTrees(Position slope)
            {
                var total = grid.TilesAlongSlope(slope).Sum(x => x.TreeCount);
                grid.MoveToStart();
                return total;
            }

            var product = slopes.Aggregate(1, (acc, slope) => acc * CountTrees(slope));
            return product;
        }
    }

    public record Tree(Position Position)
        : Day3Tile(1, Position, nameof(Tree));

    public record OpenSquare(Position Position)
        : Day3Tile(0, Position, nameof(OpenSquare));

    public record Day3Tile(int TreeCount, Position Position, string Description)
        : Tile(Position, Description);
}