using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode_2020.Common;
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
            //DirectInput = Day07ExampleInputAlt.Split("\r\n");

            ValidateDirectInputCases(DirectInput);
            IgnoreDirectInput(true);
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

            // Remove the original target bag
            var solution = paths.Except(new[] { target }).Count();

            AssertExpectedResult(148, solution);
            return $"{solution} paths to {target}. {rules.Count} rules parsed.";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var rules = inputs.ToBagRules().ToList();
            var start = new Bag("shiny", "gold");
            var root = rules.Single(r => r.Bag == start);

            int depthFirst(int multiplier, IEnumerable<Requirement> requirements)
            {
                if (requirements.IsEmpty())
                {
                    return multiplier;
                }

                var total = multiplier;
                foreach (var item in requirements)
                {
                    var children = rules.Single(r => r.Bag == item.Bag).Requirements;
                    var result = depthFirst(item.Count, children);
                    total += (multiplier * result);
                }

                return total;
            }

            // Remove the shiny gold bag from the total.
            var total = depthFirst(1, root.Requirements) - 1;

            AssertExpectedResult(24867, total);
            return $"Nesting {total} bags starting with {start}.";
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

        public const string Day07ExampleInputAlt = @"shiny gold bags contain 2 dark red bags.
dark red bags contain 2 dark orange bags.
dark orange bags contain 2 dark yellow bags.
dark yellow bags contain 2 dark green bags.
dark green bags contain 2 dark blue bags.
dark blue bags contain 2 dark violet bags.
dark violet bags contain no other bags.";
    }
}
