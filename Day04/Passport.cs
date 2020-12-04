using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode_2020.Week01
{
    /// <summary>
    /// A key and value.
    /// </summary>
    public record Field(string Key, string Value);

    public record Passport(List<Field> Fields)
    {
        public void Add(Field field)
        {
            Fields.Add(field);
        }

        public string BirthYear => Fields.FirstOrDefault(f => f.Key == "byr").Value;
        public string IssueYear => Fields.FirstOrDefault(f => f.Key == "iyr").Value;
        public string ExpirationYear => Fields.FirstOrDefault(f => f.Key == "eyr").Value;
        public string Height => Fields.FirstOrDefault(f => f.Key == "hgt").Value;
        public string HairColor => Fields.FirstOrDefault(f => f.Key == "hcl").Value;
        public string EyeColor => Fields.FirstOrDefault(f => f.Key == "ecl").Value;
        public string PassportId => Fields.FirstOrDefault(f => f.Key == "pid").Value;
        public string CountryId => Fields.FirstOrDefault(f => f.Key == "cid").Value;

        public bool IsValid()
        {
            return HasAllRequiredFields() && AllFieldsAreValid();
        }

        public bool HasAllRequiredFields()
        {
            if (Fields.Count == 8)
            {
                return true;
            }
            else if (Fields.Count == 7)
            {
                return Fields.All(f => f.Key != "cid");
            }

            return false;
        }

        public bool AllFieldsAreValid()
        {
            var validationResults = Fields.Select(field =>
            {
                return field.Key switch
                {
                    "cid" => true,
                    "byr" => field.IsYearBetween(1920, 2002),
                    "iyr" => field.IsYearBetween(2010, 2020),
                    "eyr" => field.IsYearBetween(2020,2030),
                    "hgt" => field.IsHeightBetween((150, 193), (59, 76)),
                    "hcl" => field.IsHairColor(),
                    "ecl" => field.IsEyeColor(),
                    "pid" => field.IsPassportId(),
                    _ => throw new InvalidOperationException($"invalid Tile with value '{field.Key}'.")
                };
            });

            return validationResults.All(areValid => areValid);
        }
    }
}