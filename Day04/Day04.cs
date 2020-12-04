using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week01
{
    public class Day04 : Day00
    {
        public Day04(IServiceProvider serviceProvider, ILogger<Day04> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = ExampleInput;
            IgnoreDirectInput();
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var passports = inputs.ToPassports().ToList();
            var valid = passports.Count(p => p.HasAllRequiredFields());

            return $"{valid} of {passports.Count} passports have all the required fields.";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var passports = inputs.ToPassports().ToList();
            var valid = passports.Count(p => p.IsValid());

            return $"{valid} of {passports.Count} passports are valid.";
        }

        /// <summary>
        /// The tutorial input from https://adventofcode.com/2020/day/4
        /// </summary>
        private static readonly string[] ExampleInput = @"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in".Split("\r\n");
    }
}