using System;

using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020
{
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
}