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
    public class Day13
    {

        static string exampleInput = @"Alice would gain 54 happiness units by sitting next to Bob.
Alice would lose 79 happiness units by sitting next to Carol.
Alice would lose 2 happiness units by sitting next to David.
Bob would gain 83 happiness units by sitting next to Alice.
Bob would lose 7 happiness units by sitting next to Carol.
Bob would lose 63 happiness units by sitting next to David.
Carol would lose 62 happiness units by sitting next to Alice.
Carol would gain 60 happiness units by sitting next to Bob.
Carol would gain 55 happiness units by sitting next to David.
David would gain 46 happiness units by sitting next to Alice.
David would lose 7 happiness units by sitting next to Bob.
David would gain 41 happiness units by sitting next to Carol.
";

        [TestMethod]
        public void Part1Examples()
        {
            var guests = new HashSet<string>();
            var happiness = new Dictionary<(string, string), int>();
            using (StringReader reader = new StringReader(exampleInput))
            {
                int max = maxScore(guests, happiness, reader);
                Assert.AreEqual(330, max);
            }
        }

        private static int maxScore(HashSet<string> guests, Dictionary<(string, string), int> happiness, StringReader reader, bool addMe = false)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var split = line.Split();
                var who = split[0];
                var sign = split[2].Equals("gain") ? 1 : -1;
                var points = sign * Int32.Parse(split[3]);
                var next = split[10].Substring(0, split[10].Length - 1);
                guests.Add(who);
                guests.Add(next);
                happiness.Add((who, next), points);
            }
            if (addMe)
            {
                foreach (var guest in guests)
                {
                    happiness.Add(("yourself", guest), 0);
                    happiness.Add((guest, "yourself"), 0);
                }
                guests.Add("yourself");
            }
            int max = int.MinValue;

            Common.ForAllPermutation(guests.ToArray(), (vals) =>
            {
                int sum = 0;
                //Console.WriteLine($"Guests: {String.Join(",", vals)}");
                //Console.WriteLine("Happiness:");
                for (int i = 0; i < vals.Length; i++)
                {
                    int nextGuest = (i + 1) % vals.Length;
                    int previousGuest = i == 0 ? vals.Length - 1 : i - 1;
                    sum += happiness[(vals[i], vals[nextGuest])];
                    //Console.WriteLine($"  {(vals[i], vals[nextGuest])} {happiness[(vals[i], vals[nextGuest])]}");
                    sum += happiness[(vals[i], vals[previousGuest])];
                    //Console.WriteLine($"  {(vals[i], vals[previousGuest])} {happiness[(vals[i], vals[previousGuest])]}");
                }
                //Console.WriteLine($"Sum: {sum} \n");
                max = Math.Max(max, sum);
                return false;
            });
            return max;
        }

        [TestMethod]
        public void Part1()
        {
            var guests = new HashSet<string>();
            var happiness = new Dictionary<(string, string), int>();
            using (StringReader reader = new StringReader(inputString))
            {
                int max = maxScore(guests, happiness, reader);
                Assert.AreEqual(664, max);
                Console.WriteLine(max);
            }
        }

        [TestMethod]
        public void Part2()
        {
            var guests = new HashSet<string>();
            var happiness = new Dictionary<(string, string), int>();
            using (StringReader reader = new StringReader(inputString))
            {
                int max = maxScore(guests, happiness, reader, true);
                Assert.AreEqual(640, max);
                Console.WriteLine(max);
            }
        }

       

        private static readonly string inputString = InputLoader.ReadAllText("day13.txt");
    }
}
