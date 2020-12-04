using System.Collections.Generic;

using AdventOfCode_2020.Week01;

namespace AdventOfCode_2020
{
    using static AdventOfCode_2020.Common.StringArrayDeconstructExtensions;

    public static partial class InputStringParsers
    {
        private const string PassportDelimiter = "";
        private const string FieldDelimiter = " ";
        private const string KeyValueDelimiter = ":";

        public static IEnumerable<Passport> ToPassports(this IEnumerable<string> inputs)
        {
            var passport = Passport();

            foreach (var line in inputs)
            {
                if (line == PassportDelimiter)
                {
                    yield return passport;
                    passport = Passport();
                    continue;
                }

                var fields = line.Split(FieldDelimiter);
                foreach (var field in fields)
                {
                    var (Key, Value, _) = field.Split(KeyValueDelimiter);
                    passport.Add(new Field(Key, Value));
                }
            }
        }

        static Passport Passport()
        {
            return new Passport(new List<Field>(8));
        }
    }
}