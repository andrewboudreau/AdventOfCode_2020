using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week01
{
    // Day02 Domain
    public record Parameters(char Character, int FirstNumber, int SecondNumber);

    /// <summary>
    /// Solutions to https://adventofcode.com/2020/day/2
    /// </summary>
    public class Day02 : Day00
    {
        public Day02(IServiceProvider serviceProvider, ILogger<Day02> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = @"1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc".Split("\r\n");

            IgnoreDirectInput();
        }

        protected override string Solve(IEnumerable<string> inputLine)
        {
            var valid = 0;
            foreach (var input in inputLine)
            {
                var parameters = input.GetParameters();
                var password = input.GetPassword();

                logger.LogTrace($"{parameters} on Password: {password}.");
                valid += RepeatedCharacterRequirment(parameters, password);
            }

            return $"There are {valid} valid passwords using {nameof(RepeatedCharacterRequirment)}.";
        }

        protected override string Solve2(IEnumerable<string> inputLine)
        {
            var valid = 0;
            foreach (var input in inputLine)
            {
                // Note: Be careful; Toboggan Corporate Policies have no concept of "index zero"!
                var parameters = input.GetParameters();
                var adjustedParameters = parameters with { FirstNumber = parameters.FirstNumber - 1, SecondNumber = parameters.SecondNumber - 1 };

                valid += CharacterPositionRequirment(adjustedParameters, input.GetPassword());
            }

            return $"There are {valid} valid passwords using {nameof(CharacterPositionRequirment)}.";
        }

        public static int RepeatedCharacterRequirment(Parameters parameters, string password)
        {
            var count = 0;
            for (var i = 0; i < password.Length; i++)
            {
                if (password[i] == parameters.Character)
                {
                    count++;
                }
            }

            return (parameters.FirstNumber <= count && count <= parameters.SecondNumber) ? 1 : 0;
        }

        public static int CharacterPositionRequirment(Parameters parameters, string password)
        {
            if (password[parameters.FirstNumber] == password[parameters.SecondNumber])
            {
                return 0;
            }

            if (password[parameters.FirstNumber] == parameters.Character || password[parameters.SecondNumber] == parameters.Character)
            {
                return 1;
            }

            return 0;
        }
    }
}