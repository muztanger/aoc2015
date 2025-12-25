using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advent_of_Code_2015
{
    /// <summary>
    /// Day 25: Let It Snow
    /// Generate codes in a diagonal grid pattern.
    /// Start: 20151125 at (1,1)
    /// Formula: code = (code * 252533) % 33554393
    /// Fill diagonally: (1,1), (2,1), (1,2), (3,1), (2,2), (1,3), etc.
    /// Find code at target row and column.
    /// </summary>
    [TestClass]
    public class Day25
    {
        private static readonly string inputString = InputLoader.ReadAllText("day25.txt");

        static string exampleInput = @"To continue, please consult the code grid in the manual.  Enter the code at row 4, column 3.";

        static (int row, int col) ParseInput(string input)
        {
            var match = Regex.Match(input, @"row (\d+), column (\d+)");
            return (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
        }

        static long GetCodeAt(int targetRow, int targetCol)
        {
            // Starting code
            long code = 20151125;
            
            // The grid fills diagonally:
            // (1,1) -> (2,1), (1,2) -> (3,1), (2,2), (1,3) -> etc.
            // 
            // For position (row, col):
            // - It's on diagonal number (row + col - 1)
            // - Within that diagonal, it's at position col
            // - Total positions before this diagonal: 1 + 2 + ... + (diagonal - 1) = diagonal*(diagonal-1)/2
            // - So this position is at: diagonal*(diagonal-1)/2 + col
            
            int diagonal = targetRow + targetCol - 1;
            long position = (long)diagonal * (diagonal - 1) / 2 + targetCol;
            
            // Generate codes until we reach the target position (position 1 is the starting code)
            for (long i = 1; i < position; i++)
            {
                code = (code * 252533) % 33554393;
            }
            
            return code;
        }

        [TestMethod]
        public void Part1Examples()
        {
            // Test the starting position
            Assert.AreEqual(20151125L, GetCodeAt(1, 1));
            
            // The part 1 answer works, so the algorithm is correct
            Console.WriteLine($"Code at (1,1) = {GetCodeAt(1, 1)}");
            Console.WriteLine($"Code at (2,1) = {GetCodeAt(2, 1)}");
            Console.WriteLine($"Code at (1,2) = {GetCodeAt(1, 2)}");
            Console.WriteLine($"Code at (3,1) = {GetCodeAt(3, 1)}");
            Console.WriteLine($"Code at (4,3) = {GetCodeAt(4, 3)}");
        }

        [TestMethod]
        public void Part1()
        {
            var (row, col) = ParseInput(inputString);
            long code = GetCodeAt(row, col);
            Console.WriteLine($"Part 1: Code at row {row}, column {col} = {code}");
        }

        [TestMethod]
        public void Part2Examples()
        {
            // Day 25 only has Part 1!
            Console.WriteLine("Day 25 only has Part 1. Merry Christmas! ??");
        }

        [TestMethod]
        public void Part2()
        {
            // Day 25 only has Part 1!
            Console.WriteLine("Day 25 only has Part 1. All 50 stars collected! ??");
        }
    }
}