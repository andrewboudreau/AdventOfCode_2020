using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode_2020.LuggageBags
{
    public record Bag(string Finish, string Color);

    public record Requirement(int Count, Bag Bag);

    public record Rule(Bag Bag, params Requirement[] Requirements);
}
