using System;
using Microsoft.Extensions.DependencyInjection;

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
        private readonly Instruction[] memory;
        private readonly ILogger<Cpu> logger;

        private int pc;
        private int acc;

        public Cpu(Instruction[] memory, ILogger<Cpu> logger)
        {
            this.memory = memory;
            this.logger = logger;
        }

        public int PC => pc;

        public int Accumulator => acc;

        public bool Step()
        {
            var result = Execute(memory[pc]);
            pc += result;

            var proceed = pc.IsInBounds(0, memory.Length);
            return proceed;
        }

        public int Execute(Instruction instruction)
        {
            logger.LogTrace($"PC:{PC:X2} {instruction.Operation} {(instruction.Operand.Value > 0 ? "+" : "-")}{Math.Abs(instruction.Operand.Value)}");

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

        public static Cpu GetCpu(this IServiceProvider services, Instruction[] memory)
        {
            return new Cpu(memory, services.GetService<ILogger<Cpu>>());
        }

        public static Instruction[] Patch(this Instruction[] memory, int index, Func<Instruction, Instruction> update)
        {
            memory[index] = update(memory[index]);
            return memory;
        }
    }
}