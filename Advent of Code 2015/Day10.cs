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
        [TestMethod]
        public void Part1Examples()
        {
            Assert.AreEqual("11", lookAndSay("1"));
            Assert.AreEqual("21", lookAndSay("11"));
            Assert.AreEqual("1211", lookAndSay("21"));
            Assert.AreEqual("111221", lookAndSay("1211"));
            Assert.AreEqual("312211", lookAndSay("111221"));
        }

        private string lookAndSay(string v)
        {
            int last = Int32.Parse(Char.ToString(v.Last()));
            int count = 0;
            var result = new StringBuilder();
            foreach (char c in v.Reverse())
            {
                int x = Int32.Parse(Char.ToString(c));
                if (x == last)
                {
                    count++;
                }
                else
                {
                    result.Append($"{last}{count}");
                    count = 1;
                }
                last = x;
            }
            result.Append($"{last}{count}");
            return Common.Reverse(result.ToString());
        }


        [TestMethod]
        public void Part1()
        {
            string result = "1113122113";
            for (int i = 0; i < 40; i++)
            {
                result = lookAndSay(result);
            }
            Console.WriteLine(result.Length);
        }


        [TestMethod]
        public void Part2()
        {
            string result = "1113122113";
            for (int i = 0; i < 50; i++)
            {
                result = lookAndSay(result);
            }
            Console.WriteLine(result.Length);
        }
        }
    }
