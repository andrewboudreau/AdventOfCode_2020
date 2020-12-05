using System;

namespace AdventOfCode_2020.Common.DataStructures
{
    public record Position(int X, int Y)
    {
        public static Position Zero { get; internal set; } = new Position(0, 0);
    }

    public record Tile(Position Position, string Description)
    {
        public Tile(int x, int y, string description)
            : this(new Position(x, y), description)
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
                if (position.X < 0)
                {
                    throw new InvalidOperationException($"Negative X coordinates not supported. Width: {width} Length: {tiles.Length} Position:{position.Y}");
                }

                if (position.Y >= tiles.Length / width)
                {
                    return default;
                }

                var x = position.X % width;
                var offset = (width * position.Y) + x;

                if (offset >= tiles.Length)
                {
                    throw new InvalidOperationException($"Offset beyond last position index. Offset: {offset} Rows: {tiles.Length / width} Width: {width} Length: {tiles.Length} {position}");
                }

                return tiles[offset];
            }
        }

        public T MoveBy(Position position)
        {
            current = current with
            {
                X = current.X + position.X,
                Y = current.Y + position.Y
            };

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

        public T Current => this[current];
    }
}
