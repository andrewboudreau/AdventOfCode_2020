using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode_2020
{
    public static partial class InputStringParsers
    {
        const short bitOffset = (sizeof(ulong) * 8) - 36;

        public static ulong To36BitValue(ulong value) => value << bitOffset >> bitOffset;

        public class InitProgram<T>
        {
            private readonly List<T> sections;

            public InitProgram() : this(new List<T>())
            {
            }

            public InitProgram(IEnumerable<T> sections)
            {
                this.sections = sections.ToList();
            }

            public void AddMaskedSection(T section)
            {
                sections.Add(section);
            }

            public IEnumerable<T> Sections => sections;
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

                Console.WriteLine("off : " + Convert.ToString((long)turnOffMask, 2).PadLeft(36, '0'));
                Console.WriteLine("on  : " + Convert.ToString((long)turnOnMask, 2).PadLeft(36, '0'));

                writes = new List<(int Address, ulong Value)>();
            }

            public IEnumerable<(int Address, ulong Value)> Writes => writes;

            public void AddWriteOperation(int address, ulong value)
            {
                Console.WriteLine($"initial value: {value}");

                //convert to 36-bit
                value = To36BitValue(value);
                value |= turnOnMask;
                value &= turnOffMask;

                Console.WriteLine($"masked value: {value}");

                writes.Add((address, value));
            }
        }

        public class MaskedSectionV2
        {
            private readonly List<(int Address, ulong Value)> writes;

            private readonly ulong turnOnMask;
            private List<int> fluentBits = new List<int>();

            public MaskedSectionV2(string mask)
            {
                Console.WriteLine("mask: " + mask.Substring(bitOffset));

                for (var i = 0; i < mask.Length; i++)
                {
                    if (mask[i] == 'X')
                    {
                        fluentBits.Add(i);
                    }
                }

                Console.WriteLine("on  : " + Convert.ToString((long)turnOnMask, 2).PadLeft(36, '0'));

                turnOnMask = mask.ToMaskAsLong(x => x == '1');
                writes = new List<(int Address, ulong Value)>();
            }

            public IEnumerable<(int Address, ulong Value)> Writes()
            {
                var mask = new BitArray(36);
                foreach (var write in writes)
                {
                    var address = (uint)write.Address | turnOnMask;
                    mask.SetAll(false);
                    int length = (int)Math.Pow(2, fluentBits.Count - 1);

                    for (long i = 0; i < length; i++)
                    {
                        var binary = new BitArray(new[] { (int)i });

                        for (int x = 0; i < binary.Length; i++)
                        {
                            mask.Set(fluentBits[x], binary[x]);
                        }


                        yield return ((int)address, write.Value);
                    }
                }
            }

            public void AddWriteOperation(int address, ulong value)
            {
                writes.Add((address, value));
            }
        }


        public static InitProgram<MaskedSectionV2> ToValueMaskedBootrom(this IEnumerable<string> inputs)
        {
            var program = new InitProgram<MaskedSectionV2>();
            MaskedSectionV2 mask = null;

            foreach (var current in inputs)
            {
                var parts = current.Split(" = ");
                if (parts[0] == "mask")
                {
                    if (mask is not null)
                    {
                        program.AddMaskedSection(mask);
                    }

                    mask = new MaskedSectionV2(parts[1]);
                    continue;
                }

                mask.AddWriteOperation(int.Parse(parts[0][4..^1]), ulong.Parse(parts[1]));
            }

            if (mask.Writes().Any())
            {
                program.AddMaskedSection(mask);
            }

            return program;
        }

        public static InitProgram<MaskedSection> ToMemoryDispatchedBootrom(this IEnumerable<string> inputs)
        {
            var program = new InitProgram<MaskedSection>();
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
            // don't forget to reverse
            var bools = input.Reverse().Select(bitFactory).ToArray();

            var bits = new BitArray(bools);
            var bytes = bits.ToByte();
            var number = BitConverter.ToUInt64(bytes);

            return To36BitValue(number);
        }

        public static byte[] ToByte(this BitArray bits)
        {
            //const int size = sizeof(ulong);
            //if (bits.Count != size * 8)
            //{
            //    throw new ArgumentException($"Expecting exactly {size * 8} bits.", nameof(bits));
            //}

            byte[] bytes = new byte[8];
            bits.CopyTo(bytes, 0);
            return bytes;
        }
    }
}