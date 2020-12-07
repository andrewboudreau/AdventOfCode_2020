using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode_2020
{
    /// <summary>
    /// A grouping of passengers, each passenger has a set of answers.
    /// </summary>
    public record PassengerGroup(IEnumerable<Passenger> Passengers)
    {
        /// <summary>
        /// Returns true if any of the passengers in the group answered 'YES' to the question.
        /// </summary>
        /// <param name="questions">The question id</param>
        /// <returns>True if anyone answered 'YES'.</returns>
        public IEnumerable<bool> Any(IEnumerable<char> questions)
        {
            foreach (var question in questions)
            {
                yield return Any(question);
            }
        }

        /// <summary>
        /// Returns true if all passengers in the group answered 'YES' to the question.
        /// </summary>
        /// <param name="questions">The question id</param>
        /// <returns>True if everyone answered 'YES'.</returns>
        public IEnumerable<bool> All(IEnumerable<char> questions)
        {
            foreach (var question in questions)
            {
                yield return All(question);
            }
        }

        public bool Any(char question)
        {
            return Passengers.Any(p => p.Yes.Contains(question));
        }

        public bool All(char question)
        {
            return Passengers.All(p => p.Yes.Contains(question));
        }
    };

    /// <summary>
    /// A set of answers.
    /// </summary>
    public record Passenger
    {
        private readonly HashSet<char> questions;

        public Passenger(string questionsAnsweredYes)
        {
            questions = new HashSet<char>(questionsAnsweredYes);
        }

        public IEnumerable<char> Yes => questions;
    }

    /// <summary>
    /// All known questions 'a' to 'z'.
    /// </summary>
    public class Questions : List<char>
    {
        protected Questions()
        {
            AddRange(Enumerable.Range(0, 26).Select(x => (char)('a' + x)));
        }

        public static Questions All { get; } = new Questions();
    }
}
