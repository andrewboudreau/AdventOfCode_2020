using System.Linq;

namespace AdventOfCode_2020.Week01
{
    public static class InputStringParserExtensions
    {
        public static Parameters GetParameters(this string input)
        {   // {first}-{second} {character}: {password} 
            var policy = input.Split(':')[0];
            var range = policy.Split(' ')[0].Split('-').Select(int.Parse).ToList();
            return new Parameters(policy.Split(' ')[1].Single(), range[0], range[1]);
        }

        public static string GetPassword(this string input)
        {
            return input.Split(':')[1].Trim();
        }

        /// <summary>
        /// Splits input into parameters and password parts. 
        /// </summary>
        public static (string ParameterString, string Password) SplitInputParts(this string input)
        {
            var parameters = input.Split(':')[0];
            var password = input.Split(':')[1].Trim();

            return (parameters, password);
        }
    }
}
