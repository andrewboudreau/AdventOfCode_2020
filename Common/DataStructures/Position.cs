using System;
using System.Linq;

namespace AdventOfCode_2020.Common.DataStructures
{
    public record Position(int X, int Y)
    {
        /// <summary>
        /// 90∘  (x, y)=(−y,x)
        /// 180∘ (x, y)=(−x,−y)
        /// 270∘ (x.y)=(y, -x)
        /// </summary>
        /// <param name="pivot">location of pivot</param>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public Position RotateAroundOrigin(int degrees)
        {
            var valid = new[] { 0, 90, 180, 270, 360 };
            if (!valid.Contains(Math.Abs(degrees)))
            {
                throw new NotSupportedException($"Input must be 0,90,180,270,360. Not '{degrees}'.");
            }

            var angle = (360 + degrees) % 360;
            if (!valid.Take(4).Contains(angle))
            {
                throw new NotSupportedException($"Angle must be 0, 90, 180, 270. Not '{angle}'.");
            }

            var turns = angle / 90;
            return turns switch
            {
                0 => this,
                1 => new Position(Y, -X),
                2 => new Position(-X, -Y),
                3 => new Position(-Y, X),
                _ => throw new NotSupportedException($"Invalid number of turns {turns}.")
            };
        }

        public static Position Zero { get; internal set; } = new Position(0, 0);

        public static Position operator +(Position a, Position b) => new(a.X + b.X, a.Y + b.Y);
        public static Position operator -(Position a, Position b) => new(b.X - a.X, b.Y - a.Y);
  
        public static Position operator *(int scalar, Position a) => new(a.X * scalar, a.Y * scalar);
    }
}
