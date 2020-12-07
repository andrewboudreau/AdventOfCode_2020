using System.Collections.Generic;

namespace AdventOfCode_2020
{
    public static class SolutionAlternatives_Day06
    {
        public static string Solve2_Verbose(this IEnumerable<string> inputs)
        {
            int count;
            int sum = 0;

            foreach (var group in inputs.ToPassengerGroups())
            {
                count = 0;
                foreach (var question in Questions.All)
                {
                    if (group.All(question))
                    {
                        count++;
                    }
                }

                sum += count;
            }

            return $"{sum} is the sum of groups";
        }
    }
}
