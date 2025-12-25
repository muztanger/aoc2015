using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advent_of_Code_2015
{
    /// <summary>
    /// Day 24: It Hangs in the Balance
    /// Balance packages across compartments with equal weight.
    /// Find configuration with minimum packages in first group, then minimum quantum entanglement.
    /// Part 1: 3 groups. Part 2: 4 groups.
    /// </summary>
    [TestClass]
    public class Day24
    {
        private static readonly string inputString = InputLoader.ReadAllText("day24.txt");

        static string exampleInput = @"1
2
3
4
5
7
8
9
10
11";

        static List<int> ParseInput(string input)
        {
            return input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .OrderByDescending(x => x)
                .ToList();
        }

        static long FindMinQuantumEntanglement(List<int> packages, int groups)
        {
            int totalWeight = packages.Sum();
            if (totalWeight % groups != 0) return -1;
            
            int targetWeight = totalWeight / groups;
            
            // Try to find smallest group size that can achieve target weight
            for (int groupSize = 1; groupSize <= packages.Count; groupSize++)
            {
                var validGroups = new List<long>();
                
                // Find all combinations of groupSize that sum to targetWeight
                FindCombinations(packages, 0, new List<int>(), groupSize, targetWeight, validGroups);
                
                if (validGroups.Count > 0)
                {
                    return validGroups.Min();
                }
            }
            
            return -1;
        }

        static void FindCombinations(List<int> packages, int start, List<int> current, int targetSize, int targetSum, List<long> results)
        {
            if (current.Count == targetSize)
            {
                if (current.Sum() == targetSum)
                {
                    long qe = 1;
                    foreach (var p in current)
                        qe *= p;
                    results.Add(qe);
                }
                return;
            }
            
            int remaining = targetSize - current.Count;
            for (int i = start; i <= packages.Count - remaining; i++)
            {
                current.Add(packages[i]);
                FindCombinations(packages, i + 1, current, targetSize, targetSum, results);
                current.RemoveAt(current.Count - 1);
            }
        }

        [TestMethod]
        public void Part1Examples()
        {
            var packages = ParseInput(exampleInput);
            long qe = FindMinQuantumEntanglement(packages, 3);
            Console.WriteLine($"Example Part 1: Quantum Entanglement = {qe}");
            Assert.AreEqual(99L, qe);
        }

        [TestMethod]
        public void Part1()
        {
            var packages = ParseInput(inputString);
            long qe = FindMinQuantumEntanglement(packages, 3);
            Console.WriteLine($"Part 1: Quantum Entanglement = {qe}");
        }

        [TestMethod]
        public void Part2Examples()
        {
            var packages = ParseInput(exampleInput);
            long qe = FindMinQuantumEntanglement(packages, 4);
            Console.WriteLine($"Example Part 2: Quantum Entanglement = {qe}");
            Assert.AreEqual(44L, qe);
        }

        [TestMethod]
        public void Part2()
        {
            var packages = ParseInput(inputString);
            long qe = FindMinQuantumEntanglement(packages, 4);
            Console.WriteLine($"Part 2: Quantum Entanglement = {qe}");
        }
    }
}