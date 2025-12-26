using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Advent_of_Code_2015
{
    [TestClass]
    public class Day09
    {

        private readonly Dictionary<(string, string), int> Distances = new Dictionary<(string, string), int>();
        private readonly HashSet<string> Cities = new HashSet<string>();

        static string exampleString = @"London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141";

        [TestMethod]
        public void Part1Examples()
        {
            using (StringReader reader = new StringReader(exampleString))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    parseLine(line);
                }
            }

            int min = shortestPath();
            Assert.AreEqual(605, min);

        }

        private int shortestPath()
        {
            int min = int.MaxValue;
            Common.ForAllPermutation(Cities.ToArray(), (vals) =>
            {
                int sum = 0;
                string last = null;
                foreach (string city in vals)
                {
                    if (last != null)
                    {
                        sum += Distances[(last, city)];
                    }
                    last = city;
                }
                min = Math.Min(min, sum);
                //Console.WriteLine($"{String.Join(", ", vals)}: {sum}");
                return false;
            });
            return min;
        }

        private void parseLine(string line)
        {
            var parts = line.Split();
            var from = parts[0];
            var to = parts[2];
            var distanse = Int32.Parse(parts[4]);

            Cities.Add(from);
            Cities.Add(to);

            Distances[(from, to)] = distanse;
            Distances[(to, from)] = distanse;
        }


        [TestMethod]
        public void Part1()
        {
            using (StringReader reader = new StringReader(inputString))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    parseLine(line);
                }
            }

            int min = shortestPath();
            Assert.AreEqual(251, min);
            Console.WriteLine(min);
        }


        [TestMethod]
        public void Part2Examples()
        {

        }

        [TestMethod]
        public void Part2()
        {
            using (StringReader reader = new StringReader(inputString))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    parseLine(line);
                }
            }

            int max = int.MinValue;
            Common.ForAllPermutation(Cities.ToArray(), (vals) =>
            {
                int sum = 0;
                string last = null;
                foreach (string city in vals)
                {
                    if (last != null)
                    {
                        sum += Distances[(last, city)];
                    }
                    last = city;
                }
                max = Math.Max(max, sum);
                return false;
            });

            Assert.AreEqual(898, max);
            Console.WriteLine(max);
        }

        private static readonly string inputString = InputLoader.ReadAllText("day09.txt");
        }
    }
