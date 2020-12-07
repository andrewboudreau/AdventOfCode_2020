using System;
using System.Collections.Generic;
using System.Linq;

using AdventOfCode_2020.LuggageBags;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week01
{
    public class Day07 : Day00
    {
        public Day07(IServiceProvider serviceProvider, ILogger<Day07> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = Day07ExampleInput.Split("\r\n");

            ValidateDirectInputCases(DirectInput);
            IgnoreDirectInput();
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var target = new Bag("shiny", "gold");
            var paths = new HashSet<Bag>() { target };

            var rules = inputs.ToBagRules().ToList();

            int previous = 0;
            while (paths.Count != previous)
            {
                previous = paths.Count;
                foreach (var rule in rules)
                {
                    if (rule.Requirements.Any(req => paths.Contains(req.Bag)))
                    {
                        paths.Add(rule.Bag);
                    }
                }
            }

            var solution = paths.Except(new[] { target }).Count();
            AssertExpectedResult(148, solution);
            return $"{solution} paths to {target}. {rules.Count} rules parsed.";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            return base.Solve2(inputs);
        }

        private void ValidateDirectInputCases(IEnumerable<string> inputs)
        {
            var rules = inputs.ToBagRules();
            AssertExpectedResult("light", rules.First().Bag.Finish);
            AssertExpectedResult("red", rules.First().Bag.Color);
            AssertExpectedResult(9, rules.Count());
        }

        public const string Day07ExampleInput =
    @"light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.";
    }
}
