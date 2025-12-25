using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Numerics;

namespace Advent_of_Code_2015
{
    [TestClass]
    public class Day11
    {
        [TestMethod]
        public void Part1Examples()
        {
            /*
                Passwords must include one increasing straight of at least three letters, like abc, bcd, cde, and so on, up to xyz. They cannot skip letters; abd doesn't count.
                Passwords may not contain the letters i, o, or l, as these letters can be mistaken for other characters and are therefore confusing.
                Passwords must contain at least two different, non-overlapping pairs of letters, like aa, bb, or zz.
            */

            //Assert.AreEqual(3, strait);
            //Assert.IsTrue(tabu);
            //Assert.AreEqual(2, pairs.Count);

            /*
                For example:

                hijklmmn meets the first requirement (because it contains the straight hij) but fails the second requirement requirement (because it contains i and l).
                abbceffg meets the third requirement (because it repeats bb and ff) but fails the first requirement.
                abbcegjk fails the third requirement, because it only has one double letter (bb).
                The next password after abcdefgh is abcdffaa.
                The next password after ghijklmn is ghjaabcc, because you eventually skip all the passwords that start with ghi..., since i is not allowed.
            */

            Assert.IsFalse(meetsRequirements("hijklmmn"));
            Assert.IsFalse(meetsRequirements("abbceffg"));
            Assert.IsFalse(meetsRequirements("abbcegjk"));
            Assert.IsFalse(meetsRequirements("abcdefgh"));
            Assert.IsTrue(meetsRequirements("abcdffaa"));
            Assert.IsFalse(meetsRequirements("ghijklmn"));
            Assert.IsTrue(meetsRequirements("ghjaabcc"));
            
            string password = "abcdefgh";
            int count = 0;
            while (!meetsRequirements(password))
            {
                password = addPassword(password);
                //Console.WriteLine(password);
                count++;
                if (count > 27 * 27 * 27 * 27) throw new Exception("stop");
            }
            Assert.AreEqual("abcdffaa", password);

            password = "ghijklmn";
            count = 0;
            while (!meetsRequirements(password))
            {
                password = addPassword(password);
                //Console.WriteLine(password);
                count++;
                if (count > 27 * 27 * 27 * 27 * 27 * 27) throw new Exception("stop");
            }
            Assert.AreEqual("ghjaabcc", password);

            //Console.WriteLine($"nextPassword={password}");
        }

        private static string addPassword(string password)
        {
            var nums = password.ToArray();
            int index = nums.Length - 1;
            nums[index] += (char) 1;
            while (index >= 0 && nums[index] > 'z')
            {
                nums[index] = 'a';
                index--;
                nums[index] += (char) 1;
            }
            return new String(nums);
        }

        private static bool meetsRequirements(string password)
        {
            //var password = "abbceffg";
            int strait = 1;
            int repeat = 0;
            char last = 'A';
            bool tabu = false;
            var pairs = new HashSet<char>();
            foreach (char c in password)
            {
                switch (c)
                {
                    case 'i':
                    case 'o':
                    case 'l':
                        tabu = true;
                        break;
                    default:
                        break;
                }
                if (strait < 3)
                {
                    if (last + 1 == c)
                    {
                        strait++;
                    }
                    else
                    {
                        strait = 1;
                    }
                }
                if (c == last)
                {
                    repeat++;
                    if (repeat == 1)
                    {
                        pairs.Add(c);
                    }
                }
                else
                {
                    repeat = 0;
                }
                last = c;
            }
            return strait == 3 && !tabu && pairs.Count >= 2;
        }

        [TestMethod]
        public void Part1()
        {
            string password = "cqjxjnds";
            int count = 0;
            while (!meetsRequirements(password))
            {
                password = addPassword(password);
                //Console.WriteLine(password);
                count++;
                if (count % (27 * 27 * 27) == 0) Console.WriteLine($" now on : {password}");
            }
            Console.WriteLine(password);
        }


        [TestMethod]
        public void Part2()
        {
            // not correct: cqjxxyzz
            // not correct: cqjxxyzz
            // cqjxxyzz
            string password = "cqjxxzaa";
            int count = 0;
            while (!meetsRequirements(password))
            {
                password = addPassword(password);
                //Console.WriteLine(password);
                count++;
                if (count % (27 * 27 * 27) == 0) Console.WriteLine($" now on : {password}");
            }
            Console.WriteLine(password);
        }
        }
    }
