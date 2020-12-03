using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week01
{
    public class Day02 : Day00
    {
        public Day02(IServiceProvider serviceProvider, ILogger<Day02> logger)
            : base(serviceProvider, logger)
        {
            DirectInput =
@"1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc".Split("\r\n");

            IgnoreDirectInput();
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var valid = 0;
            foreach (var input in inputs)
            {
                var (Requirement, Password) = input.ToPolicy();
                logger.LogInformation($"{Requirement} on Password: {Password}.");

                valid += Requirement.IsValid(Password) ? 1 : 0;
            }

            return $"There are {valid} valid passwords.";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            return null;
        }
    }

    public record RepeatedCharacterRequirment(char character, int min, int max)
    {
        public bool IsValid(string password)
        {
            var count = 0;
            for (var i = 0; i < password.Length; i++)
            {
                if (password[i] == character)
                {
                    count++;
                }
            }

            return min <= count && count <= max;
        }
    }

    public static class StringExtensions
    {
        public static (RepeatedCharacterRequirment Requirement, string Password) ToPolicy(this string input)
        {
            var policy = input.Split(':')[0];
            var password = input.Split(':')[1].Trim();

            var range = policy.Split(' ')[0].Split('-').Select(int.Parse).ToList();
            var character = policy.Split(' ')[1].Single();

            return (new RepeatedCharacterRequirment(character, range[0], range[1]), password);
        }
    }
}