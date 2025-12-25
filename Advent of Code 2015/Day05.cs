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
    public class Day05
    {

        private HashSet<char> mVowels = new HashSet<char>("aeiou".ToCharArray());
        private HashSet<string> mTabu = new HashSet<string>() { "ab", "cd", "pq", "xy" };

        [TestMethod]
        public void Part1Examples()
        {
            Assert.IsTrue(isNice("ugknbfddgicrmopn"));
            Assert.IsTrue(isNice("aaa"));
            Assert.IsFalse(isNice("jchzalrnumimnmhp"));
            Assert.IsFalse(isNice("haegwjzuvuyypxyu"));
            Assert.IsFalse(isNice("dvszwmarrgswjxmb"));
        }

        private bool isNice(string v)
        {
            //It contains at least three vowels(aeiou only), like aei, xazegov, or aeiouaeiouaeiou.
            //It contains at least one letter that appears twice in a row, like xx, abcdde(dd), or aabbccdd(aa, bb, cc, or dd).
            //It does not contain the strings ab, cd, pq, or xy, even if they are part of one of the other requirements.

            char last = '\b';
            bool twice = false;
            int vowels = 0;
            foreach (char c in v)
            {
                if (c == last) twice = true;
                if (mVowels.Contains(c)) vowels++;
                if (last == 'a' && c == 'b') return false;
                if (last == 'c' && c == 'd') return false;
                if (last == 'p' && c == 'q') return false;
                if (last == 'x' && c == 'y') return false;
                last = c;
            }

            return twice && vowels >= 3;
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
                    if (isNice(line)) sum++;
                }
            }
            Assert.AreEqual(238, sum);
            Console.WriteLine(sum);
        }



        [TestMethod]
        public void Part2Examples()
        {
            Assert.IsTrue(isNice2("qjhvhtzxzqqjkmpb"));
            Assert.IsTrue(isNice2("xxyxx"));
            Assert.IsFalse(isNice2("uurcxstgmygtbstg"));
            Assert.IsFalse(isNice2("ieodomkazucvgmuy"));
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
                    if (isNice2(line)) sum++;
                }
            }
            Assert.AreEqual(69, sum);
            Console.WriteLine(sum);
        }

        private bool isNice2(string v)
        {
            // Now, a nice string is one with all of the following properties:
            // It contains a pair of any two letters that appears at least twice in the string without overlapping, like xyxy (xy) or aabcdefgaa (aa), but not like aaa (aa, but it overlaps).
            // It contains at least one letter which repeats with exactly one letter between them, like xyx, abcdefeghi (efe), or even aaa.
            var pairs = new Dictionary<string, List<int>>();
            var stack = new Stack<char>();
            bool isRepeated = false;
            int i = 0;
            foreach (char c in v)
            {
                switch (stack.Count)
                {
                    case 0:
                        stack.Push(c);
                        break;
                    case 1:
                        { 
                            var l1 = stack.Peek();
                            var key = string.Concat<char>(new char[] { l1, c });
                            if (!pairs.ContainsKey(key))
                            {
                                pairs[key] = new List<int>();
                            }
                            pairs[key].Add(i);
                            stack.Push(c);
                        }
                        break;
                    default:
                        {
                            var l1 = stack.Pop();
                            var key = string.Concat<char>(new char[] { l1, c });
                            if (!pairs.ContainsKey(key))
                            {
                                pairs[key] = new List<int>();
                            }
                            pairs[key].Add(i);
                            if (c == stack.Peek()) isRepeated = true;
                            stack.Push(l1);
                            stack.Push(c);
                        }
                        break;
                }
                i++;
            }
            if (!isRepeated) return false;
            foreach (var p in pairs)
            {
                if (p.Value.Count >= 2)
                {
                    if (p.Value.Max() - p.Value.Min() >= 2) return true;
                }
            }


            return false;
        }

        private static readonly string inputString = InputLoader.ReadAllText("day05.txt");
}
}
