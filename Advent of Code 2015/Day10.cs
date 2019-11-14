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


        // https://stackoverflow.com/questions/228038/best-way-to-reverse-a-string?page=1&tab=votes#tab-top
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }



        static string exampleString = @"";

        [TestMethod]
        public void Part1Examples()
        {
            /*
             * 
    1 becomes 11 (1 copy of digit 1).
    11 becomes 21 (2 copies of digit 1).
    21 becomes 1211 (one 2 followed by one 1).
    1211 becomes 111221 (one 1, one 2, and two 1s).
    111221 becomes 312211 (three 1s, two 2s, and one 1).
*/
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
            return Reverse(result.ToString());
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

        static string inputString = @"";
        }
    }
