using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2015
{
    public class ForgetfullList
    {
        public int Count { get => mNumElements; }

        private readonly LinkedList<char> mList;
        private int mNumElements = 0;
        private readonly int N;


        public ForgetfullList(int maxCount)
        {
            N = maxCount;
            mList = new LinkedList<char>();
        }

        public void Add(char item)
        {
            while (mNumElements >= N)
            {
                mList.RemoveFirst();
                mNumElements--;
            }
            mList.AddLast(item);
            mNumElements++;
            mList.Select(x => x);
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var item in mList)
            {
                result.Append((char)item);
            }
            return result.ToString();
        }
    }

    [TestClass]
    public class Day19
    {
        private static readonly string inputString = InputLoader.ReadAllText("day19.txt");

        [TestMethod]
        public void ExamplePart1()
        {
            string example = @"H => HO
H => OH
O => HH

HOH
";
            Dictionary<string, List<string>> replacements;
            HashSet<int> replacementLengths;
            string toReplace;
            parse(example, out replacements, out replacementLengths, out toReplace);

            Console.WriteLine("replacements:");
            foreach (var kv in replacements)
            {
                Console.WriteLine($"   {kv.Key}: {String.Join(", ", kv.Value)}");
            }

            int maxLength = replacementLengths.Max();
            var latest = new ForgetfullList(maxLength);
            var result = new HashSet<string>();
            for (int i = 0; i < toReplace.Length; i++)
            {
                latest.Add(toReplace[i]);
                var latestString = latest.ToString();
                Console.WriteLine($"latestString={latestString}");
                for (int len = 1; len <= latestString.Length; len++)
                {
                    var current = latestString.Substring(latestString.Length - len, len);
                    Console.WriteLine($"current={current}");
                    if (replacements.ContainsKey(current))
                    {
                        foreach (var replacement in replacements[current])
                        {
                            string prefix = toReplace.Remove(i - len + 1);
                            var postfix = "";
                            if (i + 1 < toReplace.Length)
                            {
                                postfix = toReplace.Substring(i + 1);
                            }
                            Console.WriteLine($"Add: {prefix}_{replacement}_{postfix}");
                            result.Add(prefix + replacement + postfix);
                        }
                    }
                }
            }
            Console.WriteLine("result:");
            foreach (var elem in result)
            {
                Console.WriteLine("   " + elem);
            }
            Assert.HasCount(4, result);
        }

        private static void parse(string example, out Dictionary<string, List<string>> replacements, out HashSet<int> replacementLengths, out string toReplace)
        {
            replacements = new Dictionary<string, List<string>>();
            replacementLengths = new HashSet<int>();
            toReplace = null;
            using (StringReader reader = new StringReader(example))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("=>"))
                    {
                        var split = Regex.Split(line.Trim(), "[ =>]+");
                        if (replacements.ContainsKey(split[0]))
                        {
                            replacements[split[0]].Add(split[1]);
                        }
                        else
                        {
                            replacementLengths.Add(split[0].Length);
                            replacements[split[0]] = new List<string>() { split[1] };
                        }
                    }
                    else if (!line.Equals(""))
                    {
                        toReplace = line.Trim();
                    }
                }
            }
        }

        private static List<string> Tokenize(string molecule)
        {
            var matches = Regex.Matches(molecule, "[A-Z][a-z]?");
            return matches.Cast<Match>().Select(m => m.Value).ToList();
        }

        [TestMethod]
        public void Part1()
        {
            var replacements = new Dictionary<string, List<string>>();
            var replacementLengths = new HashSet<int>();
            string toReplace = null;
            parse(inputString, out replacements, out replacementLengths, out toReplace);

            int maxLength = replacementLengths.Max();
            var latest = new ForgetfullList(maxLength);
            var result = new HashSet<string>();
            for (int i = 0; i < toReplace.Length; i++)
            {
                latest.Add(toReplace[i]);
                var latestString = latest.ToString();
                for (int len = 1; len <= latestString.Length; len++)
                {
                    var current = latestString.Substring(latestString.Length - len, len);
                    if (replacements.ContainsKey(current))
                    {
                        foreach (var replacement in replacements[current])
                        {
                            string prefix = toReplace.Remove(i - len + 1);
                            var postfix = "";
                            if (i + 1 < toReplace.Length)
                            {
                                postfix = toReplace.Substring(i + 1);
                            }
                            result.Add(prefix + replacement + postfix);
                        }
                    }
                }
            }
            Assert.HasCount(576, result);

        }

        [TestMethod]
        public void Part2()
        {
            Dictionary<string, List<string>> replacements;
            HashSet<int> replacementLengths;
            string toReplace;
            parse(inputString, out replacements, out replacementLengths, out toReplace);

            var tokens = Tokenize(toReplace);
            int numTokens = tokens.Count;
            int numRn = tokens.Count(t => t == "Rn");
            int numAr = tokens.Count(t => t == "Ar");
            int numY = tokens.Count(t => t == "Y");

            int steps = numTokens - numRn - numAr - 2 * numY - 1;

            Assert.AreEqual(207, steps);
        }
    }
}
