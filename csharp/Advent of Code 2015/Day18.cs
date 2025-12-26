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
    public class Day18    {

        private bool[,] mGrid1 = new bool[100, 100];
        private bool[,] mGrid2 = new bool[100, 100];
        private List<bool[,]> mGrids;

        public Day18()
        {
            mGrids = new List<bool[,]>() { mGrid1, mGrid2 };
            foreach (var grid in mGrids)
            {
                resetGrid(grid);
            }
        }

        private static void resetGrid(bool[,] grid)
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    grid[i, j] = false;
                }
            }
        }

        [TestMethod]
        public void Part1()
        {
            using (StringReader reader = new StringReader(inputString))
            {
                string line;
                int y = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    for (int x = 0; x < line.Length; x++)
                    {
                        if (line[x] == '.')
                        {
                            mGrid1[x, y] = false;
                        }
                        else
                        {
                            mGrid1[x, y] = true;
                        }
                    }
                    y++;
                }
            }
            for (int i = 0; i < 100; i++)
            {
                int current = i % 2;
                int next = (i + 1) % 2;
                for (int y = 0; y < 100; y++)
                {
                    for (int x = 0; x < 100; x++)
                    {
                        int count = 0;
                        for (int dy = -1; dy <= 1; dy++)
                            for (int dx = -1; dx <= 1; dx++)
                            {
                                if (dx == 0 && dy == 0) continue;

                                int y2 = y + dy;
                                if (y2 < 0 || y2 >= 100) continue;

                                int x2 = x + dx;
                                if (x2 < 0 || x2 >= 100) continue;

                                if (mGrids[current][x2, y2]) count++;
                            }
                        if (mGrids[current][x,y])
                        {
                            if (count < 2 || count > 3)
                            {
                                mGrids[next][x,y] = false;
                            }
                            else mGrids[next][x, y] = true;
                        }
                        else
                        {
                            if (count == 3)
                            {
                                mGrids[next][x, y] = true;
                            }
                            else mGrids[next][x, y] = false;
                        }
                    }
                }
            }
            int result = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    if (mGrid1[i, j]) result++;
                }
            }
            Assert.AreEqual(1061, result);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void Part2()
        {
            using (StringReader reader = new StringReader(inputString))
            {
                string line;
                int y = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    for (int x = 0; x < line.Length; x++)
                    {
                        if (line[x] == '.')
                        {
                            mGrid1[x, y] = false;
                        }
                        else
                        {
                            mGrid1[x, y] = true;
                        }
                    }
                    y++;
                }
            }
            for (int i = 0; i < 100; i++)
            {
                int current = i % 2;
                int next = (i + 1) % 2;
                for (int y = 0; y < 100; y++)
                {
                    for (int x = 0; x < 100; x++)
                    {
                        if (x == 0 && y == 0) { mGrids[next][x, y] = true; continue; }
                        if (x == 0 && y == 99) { mGrids[next][x, y] = true; continue; }
                        if (x == 99 && y == 0) { mGrids[next][x, y] = true; continue; }
                        if (x == 99 && y == 99) { mGrids[next][x, y] = true; continue; }

                        int count = 0;
                        for (int dy = -1; dy <= 1; dy++)
                            for (int dx = -1; dx <= 1; dx++)
                            {
                                if (dx == 0 && dy == 0) continue;

                                int y2 = y + dy;
                                if (y2 < 0 || y2 >= 100) continue;

                                int x2 = x + dx;
                                if (x2 < 0 || x2 >= 100) continue;
                                if (x2 == 0 && y2 == 0) { count++; continue; }
                                if (x2 == 0 && y2 == 99) { count++; continue; }
                                if (x2 == 99 && y2 == 0) { count++; continue; }
                                if (x2 == 99 && y2 == 99) { count++; continue; }

                                if (mGrids[current][x2, y2]) count++;
                            }
                        if (mGrids[current][x, y])
                        {
                            if (count < 2 || count > 3)
                            {
                                mGrids[next][x, y] = false;
                            }
                            else mGrids[next][x, y] = true;
                        }
                        else
                        {
                            if (count == 3)
                            {
                                mGrids[next][x, y] = true;
                            }
                            else mGrids[next][x, y] = false;
                        }
                    }
                }
            }

            int result = 0;
            mGrid1[0, 0] = true; 
            mGrid1[0, 99] = true;
            mGrid1[99, 0] = true;
            mGrid1[99, 99] = true;
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    if (mGrid1[i, j]) result++;
                }
            }
            Assert.AreEqual(1006, result);
            Console.WriteLine(result);
        }
        
        private static readonly string inputString = InputLoader.ReadAllText("day18.txt");
}
}
