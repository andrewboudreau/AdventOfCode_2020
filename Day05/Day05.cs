using System;
using System.Collections.Generic;
using System.Linq;

using AdventOfCode_2020.Common;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week01
{
    public class Day05 : Day00
    {
        public Day05(IServiceProvider serviceProvider, ILogger<Day05> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = @"BFFFBBFRRR
FFFBBBFRRR
BBFFBBFRLL".Split("\r\n");

            ValidateDirectInputCases(DirectInput.ToBoardingPasses());
            IgnoreDirectInput();
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var passes = inputs.ToBoardingPasses();
            var largestId = passes.Max(x => x.Id);

            return $"Largest seat id is {largestId}.";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var passes = inputs.ToBoardingPasses()
                .OrderBy(x => x.Id)
                .ToArray();

            // Skip while the next sorted boarding pass id increases by 1.
            var seat = passes
                .SkipWhile((pass, index) => (pass.Id + 1) == passes[index + 1].Id)
                .First();

            return $"Your seat id is {seat.Id + 1}. The stopping condition was {seat}.";
        }

        private void ValidateDirectInputCases(IEnumerable<BoardingPass> boardingPasses)
        {
            var itr = boardingPasses.GetEnumerator();

            //row 70, column 7, seat ID 567 :BFFFBBFRRR
            var (row, column, id) = itr.GetNext();
            AssertExpectedResult($"{70},{7},{567}", $"{row},{column},{id}");

            //row 14, column 7, seat ID 119 :FFFBBBFRRR
            (row, column, id) = itr.GetNext();
            AssertExpectedResult($"{14},{7},{119}", $"{row},{column},{id}");

            //row 102, column 4, seat ID 820 :BBFFBBFRLL
            (row, column, id) = itr.GetNext();
            AssertExpectedResult($"{102},{4},{820}", $"{row},{column},{id}");
        }
    }
}