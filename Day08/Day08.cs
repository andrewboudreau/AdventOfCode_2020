using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week01
{
    public class Day08 : Day00
    {
        public Day08(IServiceProvider serviceProvider, ILogger<Day08> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = Day08ExampleInput.Split("\r\n");
            IgnoreDirectInput();
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var program = inputs.ToInstructionSet().ToArray();
            var cpu = GetCpu(program);

            var visited = new HashSet<int>();
            while (visited.Add(cpu.PC) && cpu.Step())
            {
            }

            AssertExpectedResult(1489, cpu.Accumulator);
            return $"ACC: {cpu.Accumulator} at PC:{cpu.PC} is the first repeated instruction after {visited.Count} operations.";
        }

        /// <summary>
        /// // 1619 is too high
        /// </summary>
        protected override string Solve2(IEnumerable<string> inputs)
        {
            var solution = 0;
            var source = inputs.ToInstructionSet().ToArray();

            for (var patchIndex = 0; patchIndex < source.Length; patchIndex++)
            {
                if (source[patchIndex].Operation == OperationName.ACC)
                {
                    continue;
                }

                var program = source.ToArray();
                program.Patch(patchIndex, ToggleJmpNop);

                Cpu cpu = GetCpu(program);

                var visited = new HashSet<int>();
                while (visited.Add(cpu.PC) && cpu.Step())
                {
                }

                if (cpu.PC == program.Length)
                {
                    solution = cpu.Accumulator;
                    logger.LogDebug($"{program[patchIndex]}");
                    AssertExpectedResult(1539, solution);
                    return $"Acc:{solution} PC:{cpu.PC:X2} PatchIndex:{patchIndex}";
                }
            }

            return "no solution";
        }

        protected Cpu GetCpu(Instruction[] memory)
        {
            return ServiceProvider.GetCpu(memory);
        }

        public static Instruction ToggleJmpNop(Instruction instruction)
        {
            if (instruction.Operation == OperationName.ACC)
            {
                throw new InvalidOperationException("ACC not supported to toggle.");
            }

            if (instruction.Operation == OperationName.JMP)
            {
                return instruction with { Operation = OperationName.NOP };
            }

            return instruction with { Operation = OperationName.JMP };
        }


        public const string Day08ExampleInput =
    @"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6";
    }
}
