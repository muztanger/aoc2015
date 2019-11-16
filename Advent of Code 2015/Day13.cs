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

        private static int maxScore(HashSet<string> guests, Dictionary<(string, string), int> happiness, StringReader reader)
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
                Console.WriteLine(max);
            }
        }

        [TestMethod]
        public void Part2()
        {
       
        }

       

        static string inputString = @"Alice would lose 2 happiness units by sitting next to Bob.
Alice would lose 62 happiness units by sitting next to Carol.
Alice would gain 65 happiness units by sitting next to David.
Alice would gain 21 happiness units by sitting next to Eric.
Alice would lose 81 happiness units by sitting next to Frank.
Alice would lose 4 happiness units by sitting next to George.
Alice would lose 80 happiness units by sitting next to Mallory.
Bob would gain 93 happiness units by sitting next to Alice.
Bob would gain 19 happiness units by sitting next to Carol.
Bob would gain 5 happiness units by sitting next to David.
Bob would gain 49 happiness units by sitting next to Eric.
Bob would gain 68 happiness units by sitting next to Frank.
Bob would gain 23 happiness units by sitting next to George.
Bob would gain 29 happiness units by sitting next to Mallory.
Carol would lose 54 happiness units by sitting next to Alice.
Carol would lose 70 happiness units by sitting next to Bob.
Carol would lose 37 happiness units by sitting next to David.
Carol would lose 46 happiness units by sitting next to Eric.
Carol would gain 33 happiness units by sitting next to Frank.
Carol would lose 35 happiness units by sitting next to George.
Carol would gain 10 happiness units by sitting next to Mallory.
David would gain 43 happiness units by sitting next to Alice.
David would lose 96 happiness units by sitting next to Bob.
David would lose 53 happiness units by sitting next to Carol.
David would lose 30 happiness units by sitting next to Eric.
David would lose 12 happiness units by sitting next to Frank.
David would gain 75 happiness units by sitting next to George.
David would lose 20 happiness units by sitting next to Mallory.
Eric would gain 8 happiness units by sitting next to Alice.
Eric would lose 89 happiness units by sitting next to Bob.
Eric would lose 69 happiness units by sitting next to Carol.
Eric would lose 34 happiness units by sitting next to David.
Eric would gain 95 happiness units by sitting next to Frank.
Eric would gain 34 happiness units by sitting next to George.
Eric would lose 99 happiness units by sitting next to Mallory.
Frank would lose 97 happiness units by sitting next to Alice.
Frank would gain 6 happiness units by sitting next to Bob.
Frank would lose 9 happiness units by sitting next to Carol.
Frank would gain 56 happiness units by sitting next to David.
Frank would lose 17 happiness units by sitting next to Eric.
Frank would gain 18 happiness units by sitting next to George.
Frank would lose 56 happiness units by sitting next to Mallory.
George would gain 45 happiness units by sitting next to Alice.
George would gain 76 happiness units by sitting next to Bob.
George would gain 63 happiness units by sitting next to Carol.
George would gain 54 happiness units by sitting next to David.
George would gain 54 happiness units by sitting next to Eric.
George would gain 30 happiness units by sitting next to Frank.
George would gain 7 happiness units by sitting next to Mallory.
Mallory would gain 31 happiness units by sitting next to Alice.
Mallory would lose 32 happiness units by sitting next to Bob.
Mallory would gain 95 happiness units by sitting next to Carol.
Mallory would gain 91 happiness units by sitting next to David.
Mallory would lose 66 happiness units by sitting next to Eric.
Mallory would lose 75 happiness units by sitting next to Frank.
Mallory would lose 99 happiness units by sitting next to George.
";
    }
}
