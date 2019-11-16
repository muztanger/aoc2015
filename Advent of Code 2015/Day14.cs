using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Numerics;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Advent_of_Code_2015
{
    [TestClass]
    public class Day14
    {


        enum State { Flying, Resting };

        class Competitor
        {
            public int Distance { get ; private set ; }
            public string Name { get; private set; }
            public int Points { get; set; } = 0;

            readonly int mSpeed;
            readonly int mFly;
            readonly int mRest;

            State state = State.Flying;
            int mTime = 0;
            
            public Competitor(string mName, int mSpeed, int mFly, int mRest)
            {
                Name = mName;
                this.mSpeed = mSpeed;
                this.mFly = mFly;
                this.mRest = mRest;
            }

            public void Tick()
            {
                if (state == State.Flying)
                {
                    Distance += mSpeed;
                }
                mTime++;
                if (state == State.Resting && mTime >= mRest)
                {
                    state = State.Flying;
                    mTime = 0;
                }
                else if (state == State.Flying && mTime >= mFly)
                {
                    state = State.Resting;
                    mTime = 0;
                }
            }
        }

        static string exampleInput = @"Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.
Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.
";
        [TestMethod]
        public void Part1Examples()
        {
            var competitors = new List<Competitor>();
            extractCompetitors(competitors, exampleInput);
            for (int i = 0; i < 1000; i++)
            {
                foreach (var c in competitors)
                {
                    c.Tick();
                }
            }
            foreach (var c in competitors)
            {
                Console.WriteLine($"{c.Name}: {c.Distance}");
            }
        }

        private static void extractCompetitors(List<Competitor> competitors, string input)
        {
            using (StringReader reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var split = line.Split();
                    //mName, int mSpeed, int mFly, int mRest)
                    var name = split[0];
                    var speed = Int32.Parse(split[3]);
                    var fly = Int32.Parse(split[6]);
                    var rest = Int32.Parse(split[13]);
                    competitors.Add(new Competitor(name, speed, fly, rest));
                }
            }
        }

        [TestMethod]
        public void Part1()
        {
            var competitors = new List<Competitor>();
            extractCompetitors(competitors, inputString);
            for (int i = 0; i < 2503; i++)
            {
                foreach (var c in competitors)
                {
                    c.Tick();
                }
            }
            int d = int.MinValue;
            foreach (var c in competitors)
            {
                Console.WriteLine($"{c.Name}: {c.Distance}");
                d = Math.Max(d, c.Distance);
            }
            Console.WriteLine($"Longest distance: {d}");
        }



        [TestMethod]
        public void Part2Examples()
        {
            var competitors = new List<Competitor>();
            extractCompetitors(competitors, exampleInput);
            for (int i = 0; i < 1000; i++)
            {
                var comparer = new GeneralizedComparer<int>();
                var score = new SortedList<int, Competitor>(comparer);
                foreach (var c in competitors)
                {
                    c.Tick();
                    score.Add(c.Distance, c);
                }
                int lastDistance = score.Last().Value.Distance;
                foreach (var s in score.Reverse())
                {
                    if (s.Value.Distance != lastDistance) break;
                    s.Value.Points += 1;
                }
            }
            foreach (var c in competitors)
            {
                Console.WriteLine($"{c.Name}: {c.Points}");
            }
        }

        [TestMethod]
        public void Part2()
        {
            var competitors = new List<Competitor>();
            extractCompetitors(competitors, inputString);
            for (int i = 0; i < 2503; i++)
            {
                var comparer = new GeneralizedComparer<int>();
                var score = new SortedList<int, Competitor>(comparer);
                foreach (var c in competitors)
                {
                    c.Tick();
                    score.Add(c.Distance, c);
                }
                int lastDistance = score.Last().Value.Distance;
                foreach (var s in score.Reverse())
                {
                    if (s.Value.Distance != lastDistance) break;
                    s.Value.Points += 1;
                }
            }
            int bestScore = int.MinValue;
            foreach (var c in competitors)
            {
                Console.WriteLine($"{c.Name}: {c.Points}");
                bestScore = Math.Max(bestScore, c.Points);
            }
            Console.WriteLine($"Best score: {bestScore}");
        }

       

        static string inputString = @"Rudolph can fly 22 km/s for 8 seconds, but then must rest for 165 seconds.
Cupid can fly 8 km/s for 17 seconds, but then must rest for 114 seconds.
Prancer can fly 18 km/s for 6 seconds, but then must rest for 103 seconds.
Donner can fly 25 km/s for 6 seconds, but then must rest for 145 seconds.
Dasher can fly 11 km/s for 12 seconds, but then must rest for 125 seconds.
Comet can fly 21 km/s for 6 seconds, but then must rest for 121 seconds.
Blitzen can fly 18 km/s for 3 seconds, but then must rest for 50 seconds.
Vixen can fly 20 km/s for 4 seconds, but then must rest for 75 seconds.
Dancer can fly 7 km/s for 20 seconds, but then must rest for 119 seconds.
";
    }
}
