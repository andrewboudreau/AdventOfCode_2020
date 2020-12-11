using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020
{
    public class XmasEncryption
    {
        private readonly List<long> transmission;
        private readonly ILogger<XmasEncryption> logger;
        private readonly Range range;
        private Range sumRange;

        private SortedSet<long> preamble;

        public XmasEncryption(long[] preamble, ILogger<XmasEncryption> logger)
        {
            range = Range.StartAt(Index.FromEnd(preamble.Length));
            transmission = preamble.ToList();
            this.logger = logger;

            UpdatePreamble();
        }

        public Range SumRange => sumRange;

        public bool Add(long signal)
        {
            long diff = 0;
            foreach (var value in preamble)
            {
                diff = signal - value;
                if (diff == signal)
                {
                    continue;
                }

                if (preamble.Contains(diff))
                {
                    logger.LogTrace($"Parts found for {signal}: {value} + {diff} = {signal}.");
                    transmission.Add(signal);
                    UpdatePreamble();

                    return true;
                }
            }

            logger.LogTrace($"Parts missing for {signal - diff}.");
            return false;
        }

        public void StartWindow(int end)
        {
            sumRange = Range.EndAt(Index.FromStart(end));
        }

        public void Increase()
        {
            sumRange = new Range(sumRange.Start, sumRange.End.Value + 1);
        }

        public void Decrease()
        {
            if (sumRange.Start.Value + 1 >= sumRange.End.Value)
            {
                throw new InvalidOperationException("start shouldn't pass end");
            }

            sumRange = new Range(sumRange.Start.Value + 1, sumRange.End.Value);
        }

        public long CalculateSum()
        {
            var (offset, length) = sumRange.GetOffsetAndLength(transmission.Count);
            var sum = transmission.GetRange(offset, length).Sum();
            
            logger.LogTrace($"∑[{sumRange.Start.Value}..{sumRange.End.Value}] = {sum:N0}");
            return sum;
        }

        private void UpdatePreamble()
        {
            var (Offset, Length) = range.GetOffsetAndLength(transmission.Count);
            preamble = new SortedSet<long>(transmission.Skip(Offset).Take(Length));
        }
    }
}
