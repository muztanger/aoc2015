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
    public class Day15
    {


        static string exampleInput = @"Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3
";
        [TestMethod]
        public void Part1Examples()
        {
            var ingredients = new List<int[]>();
            extract(ingredients, exampleInput);
            int N = 2;
            Assert.HasCount(N, ingredients);
            int psMax = int.MinValue;
            for (int k1 = 1; k1 <= 99; k1++)
            {
                int k2 = 100 - k1;
                int ps = 1;
                for (int k = 0; k < 4; k++) // ignoring calories
                {
                    Console.WriteLine($"{ps} *= {k1}*{ingredients[0][k]} + {k2}*{ingredients[1][k]}");
                    int x = k1 * ingredients[0][k] + k2 * ingredients[1][k];
                    if (x <= 0)
                    {
                        Console.WriteLine("---");
                        ps = 0;
                        break;
                    }
                    ps *= x;
                }
                psMax = Math.Max(psMax, ps);
            }
            Assert.AreEqual(62842880, psMax);
        }

        private static void extract(List<int[]> ingredients, string input)
        {
            using (StringReader reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    
                    var split = Regex.Split(line, "[, ]+");
                    //Console.WriteLine("Split: " + String.Join(";", split));
                    var properties = new int[5];
                    properties[0] = Int32.Parse(split[2]);
                    properties[1] = Int32.Parse(split[4]);
                    properties[2] = Int32.Parse(split[6]);
                    properties[3] = Int32.Parse(split[8]);
                    properties[4] = Int32.Parse(split[10]);
                    Console.WriteLine(line.Remove(line.IndexOf(':')) +":"  + String.Join(", ", properties));
                    ingredients.Add(properties);
                }
            }
        }

        [TestMethod]
        public void Part1()
        {
            var ingredients = new List<int[]>();
            extract(ingredients, inputString);
            int N = 4;
            Assert.HasCount(N, ingredients);
            int psMax = int.MinValue;

            for (int k1 = 1; k1 <= 97; k1++)
                for (int k2 = 1; k2 <= 97; k2++)
                    for (int k3 = 1; k3 <= 97; k3++)
                    {
                        int k4 = 100 - k1 - k2 - k3;
                        int ps = 1;
                        for (int k = 0; k < 4; k++) // ignoring calories
                        {
                            int x = k1 * ingredients[0][k]
                                + k2 * ingredients[1][k]
                                + k3 * ingredients[2][k]
                                + k4 * ingredients[3][k];

                            if (x <= 0)
                            {
                                ps = 0;
                                break;
                            }
                            ps *= x;
                        }
                        psMax = Math.Max(psMax, ps);
                    }
            Console.WriteLine($"psMax={psMax}");
        }



        [TestMethod]
        public void Part2Examples()
        {
            var ingredients = new List<int[]>();
            extract(ingredients, exampleInput);
            int N = 2;
            Assert.HasCount(N, ingredients);
            int psMax = int.MinValue;
            for (int k1 = 1; k1 <= 99; k1++)
            {
                int k2 = 100 - k1;
                int ps = 1;
                int calories = k1 * ingredients[0][4] + k2 * ingredients[1][4];
                if (calories != 500) continue;
                
                for (int k = 0; k < 4; k++) // ignoring calories
                {
                    Console.WriteLine($"{ps} *= {k1}*{ingredients[0][k]} + {k2}*{ingredients[1][k]}");
                    int x = k1 * ingredients[0][k] + k2 * ingredients[1][k];
                    if (x <= 0)
                    {
                        Console.WriteLine("---");
                        ps = 0;
                        break;
                    }
                    ps *= x;
                }
                psMax = Math.Max(psMax, ps);
            }
            Assert.AreEqual(57600000, psMax);
        }

        [TestMethod]
        public void Part2()
        {
            var ingredients = new List<int[]>();
            extract(ingredients, inputString);
            int N = 4;
            Assert.HasCount(N, ingredients);
            int psMax = int.MinValue;

            for (int k1 = 1; k1 <= 97; k1++)
                for (int k2 = 1; k2 <= 97; k2++)
                    for (int k3 = 1; k3 <= 97; k3++)
                    {
                        int k4 = 100 - k1 - k2 - k3;
                        int calories = k1 * ingredients[0][4]
                            + k2 * ingredients[1][4]
                            + k3 * ingredients[2][4]
                            + k4 * ingredients[3][4];

                        if (calories != 500) continue;

                        int ps = 1;
                        for (int k = 0; k < 4; k++) // ignoring calories
                        {
                            int x = k1 * ingredients[0][k]
                                + k2 * ingredients[1][k]
                                + k3 * ingredients[2][k]
                                + k4 * ingredients[3][k];

                            if (x <= 0)
                            {
                                ps = 0;
                                break;
                            }
                            ps *= x;
                        }
                        psMax = Math.Max(psMax, ps);
                    }
            Console.WriteLine($"psMax={psMax}");
        }

       

        private static readonly string inputString = InputLoader.ReadAllText("day15.txt");
    }
}
