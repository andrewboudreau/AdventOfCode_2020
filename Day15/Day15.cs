using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2020.Week03
{
    public class Game15
    {
        private readonly int[] startingNumbers;

        public Game15(params int[] startingNumbers)
        {
            Turns = new List<Turn>(2021);
            History = new Dictionary<int, List<Turn>>();
            LastSpoken = 0;
            this.startingNumbers = startingNumbers;
        }

        public List<Turn> Turns { get; }

        public Dictionary<int, List<Turn>> History { get; }

        public int LastSpoken { get; private set; }

        public int TakeTurn()
        {
            return 0;
        }
    }


    public class Day15 : Day00
    {
        public Day15(IServiceProvider serviceProvider, ILogger<Day15> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = "0,3,6".Split(',');
            // DirectInput = "18,8,0,5,4,1,20".Split(',');
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var startingNumbers = inputs.Select(int.Parse).ToArray();
            var game = new Game15(startingNumbers);
            
            int loop = 1;
            while (loop < 2021)
            {

            }
            
            // var values = new List<int>(2021);
            // var startingNumbers = 

            // var game = new List<Turn>(2021)
            // {
            //     new (loop++, 0, startingNumbers.First())
            // };





            //foreach (var input in startingNumbers.Skip(1))
            //{
            //    loop++;
            //    if ((loop - 1) < 0)
            //    {
            //        history.Add(new Turn(loop++, 0, number));
            //        continue;
            //    }

            //    history.Add(new Turn(loop++, history[loop - 1].Spoken, number));
            //}

            while (loop < 2021)
            {
                loop++;

            }

            //for (loop = history.Count + 1; loop <= 10; loop++)
            //{
            //    logger.LogInformation($"Turn {loop}: Considering the last number spoken, {lastSpoken}.");

            //    var justHappened = history.ContainsKey(lastSpoken) && loop - history[lastSpoken] == 1;
            //    if (!history.ContainsKey(lastSpoken) || justHappened)
            //    {
            //        logger.LogInformation($"Since this is the first time {lastSpoken} had been spoken the {loop}th number is '0'.");
            //        lastSpoken = 0;
            //        history[lastSpoken] = loop;
            //        continue;
            //    }

            //    logger.LogInformation($"{loop}: {lastSpoken} was last spoken on ");
            //    lastSpoken = loop - history[lastSpoken];
            //    history[lastSpoken] = loop;
            //    logger.LogInformation($"{loop}: {lastSpoken}");
            //}

            return "foo";//$"{2020}: {game.Last().Round}{game.Last().PreviouslySpoken}{game.Last().Spoken}";
        }
    }

    public struct Turn
    {
        public int Round;
        public int PreviouslySpoken;
        public int Spoken;

        public Turn(int Round, int PreviouslySpoken, int Spoken)
        {
            this.Round = Round;
            this.PreviouslySpoken = PreviouslySpoken;
            this.Spoken = Spoken;
        }
    }
}
