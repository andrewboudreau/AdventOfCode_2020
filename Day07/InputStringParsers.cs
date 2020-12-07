using System.Collections.Generic;
using System.Linq;

using AdventOfCode_2020.Common;
using AdventOfCode_2020.LuggageBags;

namespace AdventOfCode_2020
{
    public static partial class InputStringParsers
    {
        public const string Delimiter = " bags contain ";
        public static IEnumerable<Rule> ToBagRules(this IEnumerable<string> inputs)
        {
            foreach (var input in inputs)
            {
                var bag = input.GetBag();
                var contents = input.GetContentRequirements().ToArray();

                yield return new Rule(bag, contents);
            }
        }

        public static Bag GetBag(this string input)
        {
            var index = input.IndexOf(Delimiter);
            var (Finish, Color, _) = input.Substring(0, index).Split(' ');
            return new Bag(Finish, Color);
        }

        public static IEnumerable<Requirement> GetContentRequirements(this string input)
        {
            var start = input.IndexOf(Delimiter) + Delimiter.Length;

            return input[start..].Split(',')
                .Select(x => x.Trim().Split(' '))
                .Where(parts => parts[0] != "no")
                .Select(parts => new Requirement(int.Parse(parts[0]), new Bag(parts[1], parts[2])));
        }
    }
}