using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2020.Common
{
    public static partial class EnumerableExtensions
    {
        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            return collection.Count == 0;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Any() == false;
        }

        public static int[] IntegersFromCsv(this string csv)
        {
            return csv.Split(",").Select(x => int.Parse(x.Trim())).ToArray();
        }

        public static IEnumerable<int> ToInts(this IEnumerable<string> intPerLine)
        {
            return intPerLine.Select(int.Parse);
        }

        public static HashSet<int> ToHashSet(this IEnumerable<string> items)
        {
            return new HashSet<int>(items.Select(int.Parse));
        }

        public static IEnumerable<T> SkipFirst<T>(this IEnumerable<T> items)
        {
            return items.Skip(1);
        }
    }

    public static partial class EnumerableExtensions
    {
        public static IEnumerable<(T, T)> MakePairs<T>(this IEnumerable<T> set)
        {
            var i = 0;
            foreach (var item in set)
            {
                foreach (var other in set.Skip(i++))
                {
                    if (!other.Equals(item))
                    {
                        yield return (item, other);
                    }
                }
            }
        }

        public static IEnumerable<(T Yours, T Other)> MakePairs<T>(this IEnumerable<T> set, T item)
        {
            foreach (var other in set)
            {
                if (!other.Equals(item))
                {
                    yield return (item, other);
                }
            }
        }
    }
}
