using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020
{
    public record Operation(string Name, int Operands);

    public enum OperationName
    {
        NOP,
        ACC,
        JMP
    }

    public record Operand(int Value);

    public record Instruction(OperationName Operation, Operand Operand);

    public class Cpu
    {
        private readonly Instruction[] program;
        private readonly ILogger<Cpu> logger;
        private int pc;
        private int acc;

        public Cpu(Instruction[] program, ILogger<Cpu> logger)
        {
            this.program = program;
            this.logger = logger;
        }

        public int PC => pc;

        public int Accumulator => acc;

        public bool Step()
        {
            var result = Execute(program[pc]);
            pc += result;

            var proceed = pc.IsInBounds(0, program.Length);
            return proceed;
        }

        public int Execute(Instruction instruction)
        {
            //logger.LogDebug($"PC:{PC:X2} {instruction.Operation} {(instruction.Operand.Value > 0 ? "+" : "-")}{Math.Abs(instruction.Operand.Value)}");

            switch (instruction.Operation)
            {
                case OperationName.NOP:
                    return 1;

                case OperationName.ACC:
                    acc += instruction.Operand.Value;
                    return 1;

                case OperationName.JMP:
                    return instruction.Operand.Value;

                default:
                    throw new NotImplementedException(instruction.Operation.ToString());
            }
        }
    }


    public static class CpuExtensions
    {
        public static bool IsInBounds(this int value, int min, int max)
        {
            return value >= min && value < max;
        }
    }
}