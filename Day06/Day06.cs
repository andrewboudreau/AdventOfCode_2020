using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week01
{
    public record PassengerGroup(IEnumerable<Passenger> Passengers)
    {
        public int GetNumberOfQuestionsAnsweredYes() => new HashSet<char>(Passengers.SelectMany(p => p.QuestionsAnsweredYes)).Count;
    };

    public record Passenger
    {
        private readonly HashSet<char> questions;

        public Passenger(string questionsAnsweredYes)
        {
            questions = new HashSet<char>(questionsAnsweredYes);
        }

        public IEnumerable<char> QuestionsAnsweredYes => questions;
    }

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
            var votes = passengers.SelectMany(p => p.QuestionsAnsweredYes).ToList();

            // Sum the number of unique questions which any passenger answered 'YES' for each group.
            var solution = groups.Sum(group => group.GetNumberOfQuestionsAnsweredYes());

            return $"{solution} is the sum of questions answered yes for each group.{Environment.NewLine}{votes.Count:N0} votes 'Yes' across {passengers.Count:N0} passengers in {groups.Count} groups.";
            // AssertExpectedResult(6585, solution);
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            return base.Solve2(inputs);
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