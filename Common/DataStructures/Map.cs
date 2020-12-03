
using System;
using AdventOfCode_2020.Common;

namespace AdventOfCode_2020.Common.DataStructures
{

        public record Position(int X, int Y);

        public record Tile(Position Position, TileType Type)
        {
            public Tile(int x, int y, TileType type)
                : this(new Position(x, y), type)
            {
            }
        };

        public record TileType(string Name);

        public class Map<TTile>
        {
            private readonly int width;
            private readonly TTile[] tiles;
            private Position current;

            public Map(int width, TTile[] tiles)
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
                current = new Position(0, 0);
            }

            public TTile this[int x, int y]
            {
                get
                {
                    return this[new Position(x, y)];
                }
            }

            public TTile this[Position position]
            {
                get
                {
                    if (position.X < 0)
                    {
                        throw new InvalidOperationException($"Negative X coordinates not supported. Width: {width} Length: {tiles.Length} Position:{position.Y}");
                    }

                    if (position.Y >= tiles.Length / width)
                    {
                        var ex = new InvalidOperationException($"Vertical position too large. Rows: {tiles.Length / width} Width: {width} Length: {tiles.Length} {position}");
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

            public TTile MoveBy(Position position)
            {
                current = current with
                {
                    X = current.X + position.X,
                    Y = current.Y + position.Y
                };

                return this[current];
            }

            public TTile Current => this[current];
        }

        public class Map : Map<Tile>
        {
            public Map(int width, Tile[] tiles)
                : base(width, tiles)
            {
            }
        }
    }
