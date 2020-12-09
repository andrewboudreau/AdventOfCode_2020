using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week01
{
    public class Day08 : Day00
    {
        private readonly ILoggerFactory loggerFactory;

        public Day08(IServiceProvider serviceProvider, ILogger<Day08> logger, ILoggerFactory loggerFactory)
            : base(serviceProvider, logger)
        {
            this.loggerFactory = loggerFactory;

            DirectInput = Day08ExampleInput.Split("\r\n");
            //ValidateDirectInputCases(DirectInput);
            IgnoreDirectInput();
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var program = inputs.ToInstructionSet().ToArray();
            Cpu cpu = new(program, loggerFactory.CreateLogger<Cpu>());

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
            var source = inputs.ToInstructionSet().ToArray();

            for (var patchIndex = 0; patchIndex < source.Length; patchIndex++)
            {
                if (source[patchIndex].Operation == OperationName.ACC)
                {
                    continue;
                }

                var program = source.ToArray();
                Patch(program, patchIndex, ToggleJmpNop);

                Cpu cpu = new(program, loggerFactory.CreateLogger<Cpu>());
                var visited = new HashSet<int>();
                while (visited.Add(cpu.PC) && cpu.Step())
                {
                }

                if (cpu.PC == program.Length)
                {
                    return $"Acc:{cpu.Accumulator} PC:{cpu.PC:X2}";
                }
            }

            //var loop = 0;

            //for (int i = 0; i < program.Length; i++)
            //{
            //    loop++;

            //    if (program[i].Operation == OperationName.ACC)
            //    {
            //        logger.LogDebug($"Skipping ACC @ {i}");
            //    }

            //    program[i] = ToggleJmpNop(program[i]);

            //    Cpu cpu = new(program, loggerFactory.CreateLogger<Cpu>());
            //    var visited = new HashSet<int>();
            //    while (visited.Add(cpu.PC) && cpu.Step())
            //    {
            //    }

            //    if (cpu.PC == program.Length)
            //    {
            //        logger.LogInformation($"Acc:{cpu.Accumulator} PC:{cpu.PC:X2} {ToggleJmpNop(program[i])} -> {program[i]}");
            //        break;
            //    }

            //    program[i] = ToggleJmpNop(program[i]);
            //}

            //return $"Change instruction {loop} {program[loop]}. Looped {loop} times";
            return "no solution";
        }

        public static void Patch(Instruction[] program, int index, Func<Instruction, Instruction> update)
        {
            program[index] = update(program[index]);
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
