using System;
using System.Linq;

namespace AdventOfCode_2020.Week01
{
    public static class PassportValidationExtensions
    {
        private static readonly char[] PassportCharacters = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

        private static readonly string[] EyeColors = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

        public static bool IsBetween(this int value, int min, int max)
        {
            return value >= min && value <= max;
        }

        public static bool IsBetween(this int value, (int Min, int Max) range)
        {
            return IsBetween(value, range.Min, range.Max);
        }

        public static bool IsYearBetween(this Field field, int min, int max)
        {
            if (int.TryParse(field.Value, out var year))
            {
                return year.IsBetween(min, max);
            }

            return false;
        }

        public static bool IsHeightBetween(this Field field, (int Min, int Max) rangeForCentimeters, (int Min, int Max) rangeForInches)
        {
            if (int.TryParse(field.Value[..^2], out var value))
            {
                if (field.Value[^2..] == "cm")
                {
                    return value.IsBetween(rangeForCentimeters);
                }

                if (field.Value[^2..] == "in")
                {
                    return value.IsBetween(rangeForInches);
                }
            }

            return false;
        }

        public static bool IsHairColor(this Field field)
        {
            if (field.Value.Length != 7)
            {
                return false;
            }

            if (field.Value.First() != '#')
            {
                return false;
            }

            return field.Value.Skip(1)
                .All(PassportCharacters.Contains);
        }

        public static bool IsEyeColor(this Field field)
        {
            return EyeColors.Contains(field.Value);
        }

        public static bool IsPassportId(this Field field)
        {
            if (field.Value.Length != 9)
            {
                return false;
            }

            return field.Value.All(char.IsDigit);
        }
    }
}