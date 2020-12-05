using System.Collections.Generic;

namespace AdventOfCode_2020.Common
{
    public static class IEnumeratorExtensions
    {
        public static T GetNext<T>(this IEnumerator<T> itr)
        {
            if (itr.MoveNext())
            {
                return itr.Current;
            }

            return default;
        }
    }
}
