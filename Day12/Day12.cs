using System;
using System.Collections.Generic;
using AdventOfCode_2020.Common.DataStructures;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week02
{
    public class Day12 : Day00
    {
        public Day12(IServiceProvider serviceProvider, ILogger<Day12> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = Day12ExampleInput.Split("\r\n");

            //ValidateDirectInputCases(DirectInput);
            IgnoreDirectInput();
            IgnorePartOne = true;
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            return "skipped";
            var ship = new Ship();
            ship.Navigate(inputs.ToNavigationInstruction());

            //AssertExpectedResult(1007, ship.ManhattanDistance);
            return $"{ship.ManhattanDistance} from start for {ship.Position}.";
        }
        protected override string Solve2(IEnumerable<string> inputs)
        {
            var waypoint = new Position(10, 1);
            var ship = new WaypointedShip(waypoint);
            var navigation = inputs.ToNavigationInstruction();

            ship.Navigate(navigation);

            //AssertExpectedResult(0, ship.ManhattanDistance);
            return $"{ship.ManhattanDistance} from start for {ship.Position}.";
            //15076 too low.
            //33304 too low.
        }

        private void ValidateDirectInputCases(IEnumerable<string> inputs)
        {
            var ship = new Ship();
            var route = inputs.ToNavigationInstruction();
            ship.Navigate(route);

            AssertExpectedResult(25, ship.ManhattanDistance);
        }

        public const string Day12ExampleInput =
@"F10
N3
F7
R90
F11";
    }
}
