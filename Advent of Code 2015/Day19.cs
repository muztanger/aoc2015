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

        //public IEnumerable<char> Select(Func<char, T> selector)
        //{
        //    return mList.Select(selector);
        //}

        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var item in mList)
            {
                result.Append((char)item);
            }
            return result.ToString();
        }


        //public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector);
        //{
        //}
    }

    [TestClass]
    public class Day19
    {

        [TestMethod]
        public void ExamplePart1()
        {
            string example = @"H => HO
H => OH
O => HH

HOH
";

            var replacements = new Dictionary<string, List<string>>();
            var replacementLengths = new HashSet<int>();
            string toReplace = null;
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
                            /*Replacements:
                               H: HO, OH
                               O: HH
                            latestString=H
                            current=H
                            Add: _H_OH
                            Add: _H_OH
                            latestString=O
                            current=O
                            Add: H_O_H
                            latestString=H
                            current=H
                            Add: HO_H_
                            Add: HO_H_
                            result:
                               HOH*/
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
            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void Part1()
        {
            var replacements = new Dictionary<string, List<string>>();
            var replacementLengths = new HashSet<int>();
            string toReplace = null;
            using (StringReader reader = new StringReader(inputString))
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

            //Console.WriteLine("replacements:");
            //foreach (var kv in replacements)
            //{
            //    Console.WriteLine($"   {kv.Key}: {String.Join(", ", kv.Value)}");
            //}

            int maxLength = replacementLengths.Max();
            var latest = new ForgetfullList(maxLength);
            var result = new HashSet<string>();
            for (int i = 0; i < toReplace.Length; i++)
            {
                latest.Add(toReplace[i]);
                var latestString = latest.ToString();
                //Console.WriteLine($"latestString={latestString}");
                for (int len = 1; len <= latestString.Length; len++)
                {
                    var current = latestString.Substring(latestString.Length - len, len);
                    //Console.WriteLine($"current={current}");
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
                            //Console.WriteLine($"Add: {prefix}_{replacement}_{postfix}");
                            result.Add(prefix + replacement + postfix);
                        }
                    }
                }
            }
            Assert.AreEqual(576, result.Count);

        }

        [TestMethod]
        public void Part2()
        {

        }

        static string inputString = @"Al => ThF
Al => ThRnFAr
B => BCa
B => TiB
B => TiRnFAr
Ca => CaCa
Ca => PB
Ca => PRnFAr
Ca => SiRnFYFAr
Ca => SiRnMgAr
Ca => SiTh
F => CaF
F => PMg
F => SiAl
H => CRnAlAr
H => CRnFYFYFAr
H => CRnFYMgAr
H => CRnMgYFAr
H => HCa
H => NRnFYFAr
H => NRnMgAr
H => NTh
H => OB
H => ORnFAr
Mg => BF
Mg => TiMg
N => CRnFAr
N => HSi
O => CRnFYFAr
O => CRnMgAr
O => HP
O => NRnFAr
O => OTi
P => CaP
P => PTi
P => SiRnFAr
Si => CaSi
Th => ThCa
Ti => BP
Ti => TiTi
e => HF
e => NAl
e => OMg

ORnPBPMgArCaCaCaSiThCaCaSiThCaCaPBSiRnFArRnFArCaCaSiThCaCaSiThCaCaCaCaCaCaSiRnFYFArSiRnMgArCaSiRnPTiTiBFYPBFArSiRnCaSiRnTiRnFArSiAlArPTiBPTiRnCaSiAlArCaPTiTiBPMgYFArPTiRnFArSiRnCaCaFArRnCaFArCaSiRnSiRnMgArFYCaSiRnMgArCaCaSiThPRnFArPBCaSiRnMgArCaCaSiThCaSiRnTiMgArFArSiThSiThCaCaSiRnMgArCaCaSiRnFArTiBPTiRnCaSiAlArCaPTiRnFArPBPBCaCaSiThCaPBSiThPRnFArSiThCaSiThCaSiThCaPTiBSiRnFYFArCaCaPRnFArPBCaCaPBSiRnTiRnFArCaPRnFArSiRnCaCaCaSiThCaRnCaFArYCaSiRnFArBCaCaCaSiThFArPBFArCaSiRnFArRnCaCaCaFArSiRnFArTiRnPMgArF
";
    }
}
