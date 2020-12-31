using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode_2020
{
    public static partial class InputStringParsers
    {
        const short bitOffset = (sizeof(ulong) * 8) - 36;

        public class InitProgram
        {
            private readonly List<MaskedSection> sections;

            public InitProgram()
                : this(new List<MaskedSection>())
            {
            }

            public InitProgram(IEnumerable<MaskedSection> sections)
            {
                this.sections = sections.ToList();
            }

            public void AddMaskedSection(MaskedSection section)
            {
                sections.Add(section);
            }

            public IEnumerable<MaskedSection> Sections => sections;
        }

        public class MaskedSection
        {
            private readonly ulong turnOffMask;
            private readonly ulong turnOnMask;

            private readonly List<(int Address, ulong Value)> writes;

            public MaskedSection(string mask)
            {
                Console.WriteLine("mask: " + mask.Substring(bitOffset));

                // https://en.wikipedia.org/wiki/Mask_(computing)#Common_bitmask_functions
                // array of bits where the '0' of the input are set to '1' in the mask.
                turnOnMask = mask.ToMaskAsLong(x => x == '1');
                turnOffMask = mask.ToMaskAsLong(x => x != '0');

                Console.WriteLine("off : " + Convert.ToString((long)turnOffMask, 2).PadLeft(36,'0'));
                Console.WriteLine("on  : " + Convert.ToString((long)turnOnMask, 2).PadLeft(36, '0'));

                writes = new List<(int Address, ulong Value)>();
            }

            public IEnumerable<(int Address, ulong Value)> Writes => writes;

            public void AddWriteOperation(int address, ulong value)
            {
                Console.WriteLine($"initial value: {value}");

                //convert to 36-bit
                value = (value << bitOffset) >> bitOffset;
                value |= turnOnMask;
                value &= turnOffMask;

                Console.WriteLine($"masked value: {value}");

                writes.Add((address, value));
            }
        }

        public static InitProgram ToDay14Object(this IEnumerable<string> inputs)
        {
            var program = new InitProgram();
            MaskedSection mask = null;

            foreach (var current in inputs)
            {
                var parts = current.Split(" = ");
                if (parts[0] == "mask")
                {
                    if (mask is not null)
                    {
                        program.AddMaskedSection(mask);
                    }

                    // prepends unmasked 'X' to pad input to be 64 characters .
                    mask = new MaskedSection(parts[1].PadLeft(64, 'X'));
                    continue;
                }

                mask.AddWriteOperation(int.Parse(parts[0][4..^1]), ulong.Parse(parts[1]));
            }

            if (mask.Writes.Any())
            {
                program.AddMaskedSection(mask);
            }

            return program;
        }

        public static ulong ToMaskAsLong(this string input, Func<char, bool> bitFactory)
        {
            var bools = input.Reverse().Select(bitFactory).ToArray();
            var bits = new BitArray(bools);
            var bytes = bits.ToByte();
            var number = BitConverter.ToUInt64(bytes);

            Console.WriteLine("   1: " + Convert.ToString((long)number, 2).PadLeft(36, '0'));
            number = number << bitOffset;
            Console.WriteLine("   2: " + Convert.ToString((long)number, 2).PadLeft(64, '0'));
            number = number >> bitOffset;
            Console.WriteLine("   3: " + Convert.ToString((long)number, 2).PadLeft(36, '0'));

            return number;
        }

        public static byte[] ToByte(this BitArray bits)
        {
            const int size = sizeof(ulong);
            if (bits.Count != size * 8)
            {
                throw new ArgumentException($"Expecting exactly {size * 8} bits.", nameof(bits));
            }

            byte[] bytes = new byte[size];
            bits.CopyTo(bytes, 0);
            return bytes;
        }
    }
}