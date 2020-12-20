using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode_2020.Common.DataStructures;
using AdventOfCode_2020.Week02;

namespace AdventOfCode_2020
{
    public static partial class InputStringParsers
    {
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
}