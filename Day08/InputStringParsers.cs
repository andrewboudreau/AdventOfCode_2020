using System;
using System.Collections.Generic;
using System.Linq;

using AdventOfCode_2020.Common;

namespace AdventOfCode_2020
{
    public static partial class InputStringParsers
    {
        public static IEnumerable<Instruction> ToInstructionSet(this IEnumerable<string> inputs)
        {
            foreach (var instruction in inputs)
            {
                var (operation, value, _) = instruction.Split(" ");

                var operationName = operation switch
                {
                    "nop" => OperationName.NOP,
                    "acc" => OperationName.ACC,
                    "jmp" => OperationName.JMP,
                    _ => throw new NotImplementedException($"'{value.ToUpper()}' is not a supported instruction name.")
                };

                yield return new Instruction(operationName, new Operand(int.Parse(value)));
            }
        }
    }
}