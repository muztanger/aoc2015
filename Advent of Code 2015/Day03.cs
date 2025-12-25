using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advent_of_Code_2015
{
    [TestClass]
    public class Day03
    {
        private static readonly string inputString = InputLoader.ReadAllText("day03.txt");

        public struct Pos
        {
            public int x;
            public int y;
            
            public Pos(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public override bool Equals(object obj)
            {
                if (obj is Pos other)
                {
                    return x == other.x && y == other.y;
                }
                return false;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(x, y);
            }
        }

        [TestMethod]
        public void Part1Examples()
        {
            Assert.AreEqual(2, move(">"));
            Assert.AreEqual(4, move("^>v<"));
            Assert.AreEqual(2, move("^v^v^v^v^v"));
        }

        private int move(string v)
        {
            var positions = new HashSet<Pos>();
            Pos pos = new Pos { x = 0, y = 0 };
            positions.Add(pos);
            foreach (char c in v)
            {
                switch (c)
                {
                    case '>':
                        pos = new Pos(pos.x + 1, pos.y);
                        break;
                    case 'v':
                        pos = new Pos(pos.x, pos.y + 1);
                        break;
                    case '<':
                        pos = new Pos(pos.x - 1, pos.y);
                        break;
                    case '^':
                        pos = new Pos(pos.x, pos.y - 1);
                        break;
                }
                positions.Add(pos);
            }
            return positions.Count;
        }

        [TestMethod]
        public void Part1()
        {
            int result = move(inputString);
            Assert.AreEqual(2572, result);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void Part2Examples()
        {
            Assert.AreEqual(3, robo("^v"));
            Assert.AreEqual(3, robo("^>v<"));
            Assert.AreEqual(11, robo("^v^v^v^v^v"));
        }

        private int robo(string v)
        {
            var positions = new HashSet<Pos>();
            Pos santa = new Pos { x = 0, y = 0 };
            Pos robo = new Pos { x = 0, y = 0 };
            int i = 0;
            positions.Add(santa);
            foreach (char c in v)
            {
                if (i % 2 == 0)
                {
                    santa = posFromChar(santa, c);
                    positions.Add(santa);
                }
                else
                {
                    robo = posFromChar(robo, c);
                    positions.Add(robo);
                }
                i++;
            }
            return positions.Count;
        }

        private static Pos posFromChar(Pos pos, char c)
        {
            switch (c)
            {
                case '>':
                    return new Pos(pos.x + 1, pos.y);
                case 'v':
                    return new Pos(pos.x, pos.y + 1);
                case '<':
                    return new Pos(pos.x - 1, pos.y);
                case '^':
                    return new Pos(pos.x, pos.y - 1);
            }
            return pos;
        }

        [TestMethod]
        public void Part2()
        {
            int result = robo(inputString);
            Assert.AreEqual(2631, result);
            Console.WriteLine(result);
        }
    }
}
