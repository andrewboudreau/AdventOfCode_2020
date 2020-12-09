using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020
{
    public class XmaxEncryption
    {
        private readonly List<long> transmission;
        private readonly ILogger<XmaxEncryption> logger;

        private readonly Range range;

        public XmaxEncryption(long[] preamble, ILogger<XmaxEncryption> logger)
        {
            range = Range.StartAt(Index.FromEnd(preamble.Length));
            transmission = preamble.ToList();
            this.logger = logger;
        }

        public bool Add(long payload)
        {
            var (Offset, Length) = range.GetOffsetAndLength(transmission.Count);
            var hash = new HashSet<long>(transmission.Skip(Offset).Take(Length));

            foreach (var value in hash)
            {
                var diff = payload - value;
                if (diff == payload)
                {
                    continue;
                }

                if (hash.Contains(diff))
                {
                    transmission.Add(payload);
                    return true;
                }
            }

            return false;
        }
    }
}
