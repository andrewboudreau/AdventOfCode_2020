using System;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020
{
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