using System;
using System.Collections.Generic;

using AdventOfCode_2020.Common.DataStructures;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020
{
    public class Ship
    {
        private static readonly ILogger<Ship> logger = AdventOfCode.LogFactory.CreateLogger<Ship>();

        public Position Position { get; protected set; } = Position.Zero;

        public int Rotation { get; protected set; } = 90;

        public int ManhattanDistance => Math.Abs(Position.X) + Math.Abs(Position.Y);

        public virtual Ship Navigate(RouteStep step)
        {
            var destination = step.Run(this);
            Position = destination.Position;
            Rotation = (360 + destination.Rotation) % 360;
            logger.LogTrace($"{step}");
            logger.LogTrace($"`-{Position} Rotation: {Rotation}");
            return this;
        }

        public Ship Navigate(IEnumerable<RouteStep> steps)
        {
            foreach (var step in steps)
            {
                Navigate(step);
            }

            return this;
        }
    }

    public class WaypointedShip : Ship
    {
        private static readonly ILogger<WaypointedShip> logger = AdventOfCode.LogFactory.CreateLogger<WaypointedShip>();

        private readonly Ship waypoint;

        public WaypointedShip(Position waypoint)
        {
            this.waypoint = new Ship()
                .Navigate(new North(waypoint.Y))
                .Navigate(new East(waypoint.X));
        }

        public override Ship Navigate(RouteStep step)
        {
            switch (step)
            {
                case North north:
                    waypoint.Navigate(north);
                    break;

                case South south:
                    waypoint.Navigate(south);
                    break;

                case East east:
                    waypoint.Navigate(east);
                    break;

                case West west:
                    waypoint.Navigate(west);
                    break;

                case Left left:
                    RotateWayPoint(-left.Value);
                    break;

                case Right right:
                    RotateWayPoint(right.Value);
                    break;

                case Forward forward:
                    Position += forward.Value * waypoint.Position;
                    break;

                default:
                    throw new NotSupportedException($"{step} is not a supported type.");
            }

            return this;
        }

        public void RotateWayPoint(int degrees)
        {
            Position = Position.RotateAroundOrigin(degrees);
        }
    }

    public record RouteStep(Func<Ship, (Position Position, int Rotation)> Operation)
    {
        public (Position Position, int Rotation) Run(Ship ship) => Operation(ship);
    };

    public record North(int Value) : RouteStep(ship => (ship.Position + new Position(0, Value), ship.Rotation));
    public record South(int Value) : RouteStep(ship => (ship.Position + new Position(0, -Value), ship.Rotation));
    public record East(int Value) : RouteStep(ship => (ship.Position + new Position(Value, 0), ship.Rotation));
    public record West(int Value) : RouteStep(ship => (ship.Position + new Position(-Value, 0), ship.Rotation));

    public record Left(int Value) : RouteStep(ship => (ship.Position, ship.Rotation - Value));
    public record Right(int Value) : RouteStep(ship => (ship.Position, ship.Rotation + Value));

    public record Forward(int Value) : RouteStep(ship => ship.Rotation switch
        {
            0 => (ship.Position + new Position(0, Value), ship.Rotation),
            90 => (ship.Position + new Position(Value, 0), ship.Rotation),
            180 => (ship.Position + new Position(0, -Value), ship.Rotation),
            270 => (ship.Position + new Position(-Value, 0), ship.Rotation),
            _ => throw new InvalidOperationException($"{ship.Rotation} is an unsupported ship rotation.")
        });
}
