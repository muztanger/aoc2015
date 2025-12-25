using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Advent_of_Code_2015
{
    [TestClass]
    public class Day06
    {
        private bool[,] mGrid = new bool[1000,1000];
        private int[,] mGrid2 = new int[1000, 1000];

        [TestInitialize]
        public void Initialize()
        {
            // Reset grids before each test
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    mGrid[i, j] = false;
                    mGrid2[i, j] = 0;
                }
            }
        }

        private void zero(int x, int y)
        {
            mGrid2[x, y] = 0;
        }

        [TestMethod]
        public void Part1Examples()
        {
            //turn on 0,0 through 999,999 would turn on(or leave on) every light.
            doAction(0, 0, 999, 999, turnOn);

            //toggle 0,0 through 999,0 would toggle the first line of 1000 lights, turning off the ones that were on, and turning on the ones that were off.
            doAction(0, 0, 999, 0, toggle);

            //turn off 499,499 through 500,500 would turn off(or leave off) the middle four lights.
            doAction(0, 0, 999, 999, turnOff);
            
        }

        private void doAction(int x1, int y1, int x2, int y2, Action<int, int> action)
        {
            Assert.IsTrue(x1 <= x2, $"x1 ({x1}) should be <= x2 ({x2})");
            Assert.IsTrue(y1 <= y2, $"y1 ({y1}) should be <= y2 ({y2})");
            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    action(x, y);
                }
            }
        }

        private void turnOn(int x, int y)
        {
            mGrid[x, y] = true;
        }

        private void turnOff(int x, int y)
        {
            mGrid[x, y] = false;
        }

        private void toggle(int x, int y)
        {
            mGrid[x, y] = !mGrid[x, y];
        }
        

        [TestMethod]
        public void Part1()
        {
            using (var reader = new StringReader(InputLoader.ReadAllText("day06.txt")))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("turn on "))
                    {
                        // turn on 0,0 through 999,999
                        var split = line.Split(' ');
                        var pos1 = split[2].Split(',').Select(x => int.Parse(x)).ToArray<int>();
                        var pos2 = split[4].Split(',').Select(x => int.Parse(x)).ToArray<int>();
                        doAction(pos1[0], pos1[1], pos2[0], pos2[1], turnOn);
                    }
                    else if (line.StartsWith("toggle "))
                    {
                        // toggle 0,0 through 999,0
                        var split = line.Split(' ');
                        var pos1 = split[1].Split(',').Select(x => int.Parse(x)).ToArray<int>();
                        var pos2 = split[3].Split(',').Select(x => int.Parse(x)).ToArray<int>();
                        doAction(pos1[0], pos1[1], pos2[0], pos2[1], toggle);

                    }
                    else if (line.StartsWith("turn off "))
                    {
                        // turn off 499,499 through 500,500
                        var split = line.Split(' ');
                        var pos1 = split[2].Split(',').Select(x => int.Parse(x)).ToArray<int>();
                        var pos2 = split[4].Split(',').Select(x => int.Parse(x)).ToArray<int>();
                        doAction(pos1[0], pos1[1], pos2[0], pos2[1], turnOff);
                    }
                }
            }
            int sum = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (mGrid[i, j]) sum++;
                }
            }
            Console.WriteLine(sum);
        }

        private void turnOn2(int x, int y)
        {
            mGrid2[x, y]++;
        }

        private void turnOff2(int x, int y)
        {
            if (mGrid2[x, y] > 0) mGrid2[x, y]--;
        }

        private void toggle2(int x, int y)
        {
            mGrid2[x, y]+=2;
        }

        [TestMethod]
        public void Part2Examples()
        {

        }

        [TestMethod]
        public void Part2()
        {
            using (var reader = new StringReader(InputLoader.ReadAllText("day06.txt")))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("turn on "))
                    {
                        // turn on 0,0 through 999,999
                        var split = line.Split(' ');
                        var pos1 = split[2].Split(',').Select(x => int.Parse(x)).ToArray<int>();
                        var pos2 = split[4].Split(',').Select(x => int.Parse(x)).ToArray<int>();
                        doAction(pos1[0], pos1[1], pos2[0], pos2[1], turnOn2);
                    }
                    else if (line.StartsWith("toggle "))
                    {
                        // toggle 0,0 through 999,0
                        var split = line.Split(' ');
                        var pos1 = split[1].Split(',').Select(x => int.Parse(x)).ToArray<int>();
                        var pos2 = split[3].Split(',').Select(x => int.Parse(x)).ToArray<int>();
                        doAction(pos1[0], pos1[1], pos2[0], pos2[1], toggle2);

                    }
                    else if (line.StartsWith("turn off "))
                    {
                        // turn off 499,499 through 500,500
                        var split = line.Split(' ');
                        var pos1 = split[2].Split(',').Select(x => int.Parse(x)).ToArray<int>();
                        var pos2 = split[4].Split(',').Select(x => int.Parse(x)).ToArray<int>();
                        doAction(pos1[0], pos1[1], pos2[0], pos2[1], turnOff2);
                    }
                }
            }
            int sum = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    sum += mGrid2[i, j];
                }
            }
            // 16325717 wrong
            // 17325717 wrong
            Console.WriteLine(sum);
        }

        private static readonly string inputString = InputLoader.ReadAllText("day06.txt");
    }
}
