# [Advent of Code 2020](https://adventofcode.com/2020)
Solutions to the [Advent of Code](https://adventofcode.com/2020) Challenges for 2020

Written in [C#](https://docs.microsoft.com/en-us/dotnet/csharp/) and [.NET5](https://dotnet.microsoft.com/download/dotnet/5.0) using [Visual Studio Community](https://visualstudio.microsoft.com/vs/community/).

## Day01
Finding target value by using a pairs of numbers. Used a hashset to see if the target difference of a sample exists.
The second day worked very easily based on the part 1 solution.
```csharp
if (values.Contains(target - value))
{
    return (value, target - value);
}
```

## Day02
Setting up the `InputStringParsers` pattern. Parsing data from the previous years was always a pain so I want to keep the step separate and simple this year.
Working with functions, simple fun, building a validation system. Trying very hard to keep the solution method simple by creating DSL but also SIMPLE SIMPLE SIMPLE!
```csharp
foreach (var input in inputLine)
{
    isValid += RepeatedCharacterRequirment(input.GetParameters(), input.GetPassword());
}
```

## Day03
Introduced the `Grid` and started building up `Common` namespace.

## Day04
Working more with C# records and a domain. Started using more `IEnumerable` with `yield return` which is feeling way more comfortable this year.
```chsarp
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
```

## Day05
Binary numbers! I really feel like my experience with emudev really helped identify the gimmick in this one. that the characters map directly to the binary value. I was so happy to be able to identify that when reading the problem initially. I created lots of variants cause, why not. `BinaryToIntegerWithLinqAggregate` `BinaryToIntegerWithForLoop`

```csharp
public static IEnumerable<BoardingPass> ToBoardingPasses(this IEnumerable<string> inputs)
{
    foreach (var encoded in inputs)
    {
        // take the first seven bits and create the binary version of that value
        var row = InputToInt(encoded[..7]);

        // take the last 3 bits and create the binary version of that value
        var column = InputToInt(encoded[7..]);

        yield return new BoardingPass(row, column);
    }
}

public static int InputToInt(this string encoded)
{
    var binary = encoded.Replace("F", "0").Replace("B", "1").Replace("L", "0").Replace("R", "1");
    return Convert.ToInt32(binary, 2);
}
```

###Day06
Some fun domain building, not sure if there is a much better approach, most likely. I do think you have to look at each item but I wonder if there is a a simple binary operations that can be applied in batch to find the requested queries.


## Previous Solutions
[2019 Solutions](https://github.com/andrewboudreau/AdventOfCode_2019/tree/master/AdventOfCode_2019)
