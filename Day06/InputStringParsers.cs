using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode_2020
{
    public static partial class InputStringParsers
    {
        public static IEnumerable<PassengerGroup> ToPassengerGroups(this IEnumerable<string> inputs)
        {
            var passengers = new List<Passenger>();

            var itr = inputs.GetEnumerator();
            while (itr.MoveNext())
            {
                if (string.IsNullOrEmpty(itr.Current))
                {
                    yield return new PassengerGroup(passengers);
                    passengers = new List<Passenger>();
                    continue;
                }

                passengers.Add(new Passenger(itr.Current));
            }
            // Flush
            if (passengers.Any())
            {
                yield return new PassengerGroup(passengers);
            }
        }
    }
}