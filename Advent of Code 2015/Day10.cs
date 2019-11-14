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
    public class Day10
    {

 

        static string exampleString = @"";

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
            //ForAllPermutation(Cities.ToArray(), (vals) =>
            //{
            //    int sum = 0;
            //    string last = null;
            //    foreach (string city in vals)
            //    {
            //        if (last != null)
            //        {
            //            sum += Distances[(last, city)];
            //        }
            //        last = city;
            //    }
            //    min = Math.Min(min, sum);
            //    //Console.WriteLine($"{String.Join(", ", vals)}: {sum}");
            //    return false;
            //});
            return min;
        }

        private void parseLine(string line)
        {
            var parts = line.Split();
            var from = parts[0];
            var to = parts[2];
            var distanse = Int32.Parse(parts[4]);

            //Cities.Add(from);
            //Cities.Add(to);

            //Distances[(from, to)] = distanse;
            //Distances[(to, from)] = distanse;
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

            

        }

        static string inputString = @"";
        }
    }
