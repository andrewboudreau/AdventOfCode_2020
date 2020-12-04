using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode_2020.Common
{
    public static class StringArrayDeconstructExtensions
    {
        public static void Deconstruct<T>(this T[] list, out T first, out IList<T> rest)
        {
            first = list.Length > 0 ? list[0] : default; // or throw
            rest = list.Skip(1).ToList();
        }

        public static void Deconstruct<T>(this T[] list, out T first, out T second, out IList<T> rest)
        {
            first = list.Length > 0 ? list[0] : default; // or throw
            second = list.Length > 1 ? list[1] : default; // or throw
            rest = list.Skip(2).ToList();
        }
    }
}
