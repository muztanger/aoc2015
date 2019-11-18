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
    public class Day17
    {
        // Example usage
        //List<List<int>> combos = GetAllCombos(new int[] { 1, 2, 3 }.ToList());
        [TestMethod]
        public void ExamplePart1()
        {
            int[] buckets = { 20, 15, 10, 5, 5 };
            const int N = 25;
            int count = waysOfN(buckets, N);
            Assert.AreEqual(4, count);
        }

        private static int waysOfN(int[] buckets, int N)
        {
            int count = 0;
            var combos = Common.GetAllCombos(new List<int>(buckets));
            foreach (var combo in combos)
            {
                int sum = 0;
                foreach (var x in combo)
                {
                    sum += x;
                    if (sum > N) break;
                }
                if (sum == N) count++;
            }

            return count;
        }

        int[] buckets = { 50
            ,44
            ,11
            ,49
            ,42
            ,46
            ,18
            ,32
            ,26
            ,40
            ,21
            ,7
            ,18
            ,43
            ,10
            ,47
            ,36
            ,24
            ,22
            ,40};

        [TestMethod]
        public void Part1()
        {
            const int N = 150;
            int count = waysOfN(buckets, N);
            Console.WriteLine(count);
        }


        [TestMethod]
        public void Part2()
        {
            
            const int N = 150;
            var combos = Common.GetAllCombos(new List<int>(buckets));
            var count = new Dictionary<int, int>();
            foreach (var combo in combos)
            {
                int sum = 0;
                foreach (var x in combo)
                {
                    sum += x;
                    if (sum > N) break;
                }
                if (sum == N)
                {
                    int key = combo.Count;
                    if (count.ContainsKey(key))
                    {
                        count[key]++;
                    }
                    else
                    {
                        count[key] = 1;
                    }
                }
            }
            int min = count.Select(x => x.Key).Min();
            
            Console.WriteLine(count[min]);
        }
    }
}
