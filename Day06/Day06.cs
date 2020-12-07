using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week01
{
    public class Day06 : Day00
    {
        public Day06(IServiceProvider serviceProvider, ILogger<Day06> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = Day06ExampleInput.Split("\r\n");

            ValidateDirectInputCases(DirectInput);
            IgnoreDirectInput();
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var groups = inputs.ToPassengerGroups().ToList();
            var passengers = groups.SelectMany(g => g.Passengers).ToList();
            var votes = passengers.SelectMany(p => p.Yes).ToList();

            // Sum the number of unique questions which any passenger answered 'YES' for each group.
            var solution = groups
                .Sum(group =>
                    group.Any(Questions.All).Count(x => x == true));

            AssertExpectedResult(6585, solution);
            return $"{solution} is the sum of questions answered yes for each group.{Environment.NewLine}{votes.Count:N0} votes 'Yes' across {passengers.Count:N0} passengers in {groups.Count} groups.";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var solution = inputs.ToPassengerGroups()
                .Sum(group =>
                    group.All(Questions.All).Count(x => x == true));

            AssertExpectedResult(solution, 3276);
            return $"{solution} is the sum of groups";
        }

        private void ValidateDirectInputCases(IEnumerable<string> inputs)
        {
            var groups = inputs.ToPassengerGroups().Count();
            AssertExpectedResult(5, groups);
            AssertExpectedResult(11, inputs.ToPassengerGroups().Sum(g => g.Passengers.Count()));
        }

        public const string Day06ExampleInput =
    @"abc

a
b
c

ab
ac

a
a
a
a

b";
    }
}