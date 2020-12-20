using System;
using System.Collections.Generic;

namespace AdventOfCode_2020.Common.DataStructures
{
    public record Position(int X, int Y)
    {
        public static Position Zero { get; internal set; } = new Position(0, 0);

        public static Position operator +(Position a, Position b) => new(a.X + b.X, a.Y + b.Y);
        public static Position operator -(Position a, Position b) => new(a.X - b.X, a.Y - b.Y);
    }

    public record Tile(Position Position, char Display, int Value)
    {
        public Tile(int x, int y, char display, int value = 0)
            : this(new Position(x, y), display, value)
        {
        }
    };

    /// <summary>
    /// A 2D rectangle shaped collection of tiles.
    /// </summary>
    /// <typeparam name="T">The type of grid tile. Contains any tile specific data.</typeparam>
    public class Grid<T>
    {
        private readonly int width;
        private readonly T[] tiles;
        private readonly Position start;

        private Position current;

        public Grid(int width, T[] tiles)
        {
            if (tiles.IsEmpty())
            {
                throw new ArgumentException($"Tiles can not be empty.");
            }

            if (tiles.Length % width != 0)
            {
                throw new ArgumentException($"total tiles {tiles.Length} is not divisible by width {width}.");
            }

            this.width = width;
            this.tiles = tiles;

            current = Position.Zero;
            start = Position.Zero;
        }

        public bool ThrowOutOfRangeExceptions { get; init; }

        public T this[int x, int y]
        {
            get
            {
                return this[new Position(x, y)];
            }
        }

        public T this[Position position]
        {
            get
            {
                if (IsOutOfBounds(position))
                {
                    return default;
                }

                return tiles[OffsetFrom(position)];
            }
            set
            {
                if (!IsOutOfBounds(position))
                {
                    tiles[OffsetFrom(position)] = value;
                }
            }
        }

        public IEnumerable<T> Tiles => tiles;

        public T Current => this[current];

        public bool VerticalWrapping { get; set; }

        public bool IsOutOfBounds()
        {
            return IsOutOfBounds(current);
        }

        public bool IsOutOfBounds(Position position)
        {
            if (position.X < 0 || (!VerticalWrapping && position.X >= width))
            {
                if (ThrowOutOfRangeExceptions)
                {
                    throw new IndexOutOfRangeException($"X coordinate is out of bounds. Width: {width} Length: {tiles.Length} Position:{position} Height:{tiles.Length / width} VerticalWrapping:{VerticalWrapping}");
                }

                return true;
            }

            if (position.Y < 0 || position.Y >= tiles.Length / width)
            {
                if (ThrowOutOfRangeExceptions)
                {
                    throw new IndexOutOfRangeException($"Y coordinate out of bounds. Width: {width} Length: {tiles.Length} Position:{position} Height:{tiles.Length / width}");
                }

                return true;
            }

            return false;
        }

        public T MoveBy(Position position)
        {
            current += position;
            return this[current];
        }

        public T MoveTo(Position position)
        {
            current = position;
            return this[current];
        }

        public void MoveToStart()
        {
            MoveTo(start);
        }

        public IEnumerable<IEnumerable<T>> GetRows()
        {
            int rows = width % tiles.Length;

            for (var row = 0; row < rows - 1; row++)
            {
                yield return GetRow(row);
            }
        }

        /// <summary>
        /// Enumerates the tiles for a row.
        /// </summary>
        /// <param name="i">The 0-based index for the row.</param>
        /// <returns>A row of tiles.</returns>
        public IEnumerable<T> GetRow(int row)
        {
            var start = row * width;
            var length = start + width;

            for (var offset = start; offset < length; offset++)
            {
                yield return tiles[offset];
            }
        }

        private int OffsetFrom(Position position)
        {
            return (width * position.Y) + position.X % width;
        }
    }
}
