using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using AdventOfCode_2020.Common.DataStructures;
using System.Linq;

namespace AdventOfCode_2020.Week02
{
    public static class Seats
    {
        public const char NoSeat = '.';
        public const char Empty = 'L';
        public const char Occupied = '#';
    }

    public static class TileExtensions
    {
        public static bool IsOccupied(this Tile tile) => tile.Display == Seats.Occupied;
        public static bool IsEmpty(this Tile tile) => tile.Display == Seats.Empty;
        public static bool HasSeat(this Tile tile) => tile.Display != Seats.NoSeat;

        public static Grid<Tile> ToSeatLayout(this IEnumerable<string> inputs)
        {
            return inputs.ToGrid((position, c) =>
            {
                return c switch
                {
                    Seats.NoSeat => new Tile(position, Seats.NoSeat, 0),
                    Seats.Empty => new Tile(position, Seats.Empty, 0),
                    Seats.Occupied => new Tile(position, Seats.Occupied, 0),
                    _ => throw new InvalidOperationException($"Invalid input character '{c}'.")
                };
            });
        }
    }

    public class Day11 : Day00
    {

        public Day11(IServiceProvider serviceProvider, ILogger<Day11> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = Day11ExampleInput.Split("\r\n");
            IgnoreDirectInput();
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var loops = 0;
            var current = inputs.ToSeatLayout();
            var next = inputs.ToSeatLayout();
            Grid<Tile> temp;

            int last = int.MinValue;
            int count = 0;

            while (count != last)
            {
                foreach (var tile in current.Tiles.Where(t => t.HasSeat()))
                {
                    var nebs = current.AdjacentTo(tile.Position).ToList();
                    var neighbors = current.AdjacentTo(tile.Position).Count(t => t.IsOccupied());

                    if (tile.IsEmpty() && neighbors == 0)
                    {
                        next[tile.Position] = tile with { Display = Seats.Occupied, Value = neighbors };
                    }
                    else if (tile.IsOccupied() && neighbors >= 4)
                    {
                        next[tile.Position] = tile with { Display = Seats.Empty, Value = neighbors };
                    }
                    else
                    {
                        next[tile.Position] = current[tile.Position] with { Value = neighbors };
                    }
                }

                // Swap buffers
                temp = current;
                current = next;
                next = temp;

                // Update totals
                last = count;
                count = current.Tiles.Count(seat => seat.IsOccupied());
                loops++;
            }

            var solution = current.Tiles.Count(seat => seat.IsOccupied());
            AssertExpectedResult(2126, solution);
            return $"{solution} seats occupied stabilized after {loops}.";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var loops = 0;
            var grid = inputs.ToSeatLayout();
            var buffer = inputs.ToSeatLayout();
            Grid<Tile> temp;

            long last = long.MinValue;
            long count = 0;

            var slopes = new Position[] {
                new(-1,-1),
                new( 0,-1),
                new( 1,-1),
                new(-1, 0),
                new( 1, 0),
                new(-1, 1),
                new( 0, 1),
                new( 1, 1),
            };

            while (count != last)
            {
                foreach (var current in grid.Tiles.Where(t => t.HasSeat()))
                {
                    int neighbors = 0;
                    foreach (var view in grid.AdjacentInAllDirections(from: current.Position))
                    {
                        var seat = view.FirstOrDefault(v => v.HasSeat());
                        if (seat != default)
                        {
                            neighbors += seat.IsOccupied() ? 1 : 0;
                        }
                    }

                    if (current.IsEmpty() && neighbors == 0)
                    {
                        buffer[current.Position] = current with { Display = Seats.Occupied, Value = neighbors };
                    }
                    else if (current.IsOccupied() && neighbors >= 5)
                    {
                        buffer[current.Position] = current with { Display = Seats.Empty, Value = neighbors };
                    }
                    else
                    {
                        buffer[current.Position] = grid[current.Position] with { Value = neighbors };
                    }
                }

                // Swap buffers
                temp = grid;
                grid = buffer;
                buffer = temp;

                // Update totals
                last = count;
                count = grid.Tiles.Count(seat => seat.IsOccupied());
                loops++;
                grid.Print(x => logger.LogInformation(x));
            }

            AssertExpectedResult(1914, count);
            return $"{count} seats occupied stabilized after {loops}.";
        }

        public const string Day11ExampleInput =
    @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";
    }
}
