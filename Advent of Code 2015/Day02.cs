using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advent_of_Code_2015
{
    [TestClass]
    public class Day02
    {
        [TestMethod]
        public void Part1Examples()
        {

            Assert.AreEqual(58, paper(2, 3, 4));
            Assert.AreEqual(43, paper(1, 1, 10));

        }

        private int paper(int v1, int v2, int v3)
        {
            // Needed paper: 2 * (l*w + w*h + h*l) + min(l*w, w*h, h*l)

            int a1 = v1 * v2;
            int a2 = v2 * v3;
            int a3 = v3 * v1;

            int e = Math.Min(Math.Min(a1, a2), a3);

            return 2 * (a1 + a2 + a3) + e;
        }

        [TestMethod]
        public void Part1()
        {
            int sum = 0;
            using (StringReader reader = new StringReader(inputString))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] split = line.Split('x');
                    sum += paper(Int32.Parse(split[0]), Int32.Parse(split[1]), Int32.Parse(split[2]));
                }
            }
            Assert.AreEqual(1586300, sum);
            Console.WriteLine(sum);
        }

        [TestMethod]
        public void Part2Examples()
        {
            Assert.AreEqual(34, ribbon(2, 3, 4));
            Assert.AreEqual(14, ribbon(1, 1, 10));
        }

        private int ribbon(int v1, int v2, int v3)
        {
            var arr = new List<int>() { v1, v2, v3 };
            arr.Sort();
            return 2 * (arr[0] + arr[1]) + arr[0] * arr[1] * arr[2];
        }

        [TestMethod]
        public void Part2()
        {
            int sum = 0;
            using (StringReader reader = new StringReader(inputString))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] split = line.Split('x');
                    sum += ribbon(Int32.Parse(split[0]), Int32.Parse(split[1]), Int32.Parse(split[2]));
                }
            }
            Assert.AreEqual(3737498, sum);
            Console.WriteLine(sum);
        }

        private static readonly string inputString = InputLoader.ReadAllText("day02.txt");
    }
}
