using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Numerics;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Advent_of_Code_2015
{
    [TestClass]
    public class Day12
    {
        [TestMethod]
        public void Part1Examples()
        {
            Assert.AreEqual(6, jsonSum("[1,2,3]"));
            Assert.AreEqual(6, jsonSum("{\"a\":2,\"b\":4}"));
            Assert.AreEqual(3, jsonSum("[[[3]]])"));
            Assert.AreEqual(3, jsonSum("{\"a\":{\"b\":4},\"c\":-1}"));
            Assert.AreEqual(0, jsonSum("{\"a\":[-1,1]}"));
            Assert.AreEqual(0, jsonSum("[-1,{\"a\":1}]"));
            Assert.AreEqual(0, jsonSum("[]"));
            Assert.AreEqual(0, jsonSum("{}"));
        }

        private int jsonSum(string v)
        {
            Regex rx = new Regex(@"(-?\d+)",RegexOptions.Compiled);
            MatchCollection matches = rx.Matches(v);
            int sum = 0;
            foreach (Match match in matches)
            {
                //Console.WriteLine($"match={match}");
                sum += int.Parse(match.ToString());
            }
            return sum;
        }

        [TestMethod]
        public void Part1()
        {
            Console.WriteLine(jsonSum(inputString));
        }

        [TestMethod]
        public void Part2()
        {
            JArray a = JArray.Parse(inputString);
            int sum = 0;
            foreach (var e in a)
            {
                //Console.WriteLine(e);
                add(e, ref sum);
            }
            // incorrect: 191164
            Console.WriteLine(sum);
        }

        private void add(JToken e, ref int sum)
        {
            switch (e.Type)
            {
                case JTokenType.Object:
                    JToken red = e.SelectToken("red");
                    bool isAdded = true;
                    foreach (JProperty x in e)
                    {
                        if (x.Value.Type == JTokenType.String)
                        {
                            string value = (string)x.Value;
                            if (value.Equals("red"))
                            {
                                isAdded = false;
                                break;
                            }
                        }
                    }
                    if (isAdded)
                    {
                        foreach (JProperty x in e)
                        {
                            add(x.Value, ref sum);
                        }
                    }
                    break;
                case JTokenType.Array:
                    JArray arr = (JArray) e;
                    foreach (var x in arr)
                    {
                        add(x, ref sum);
                    }
                    break;
                case JTokenType.Integer:
                    sum += e.Value<int>();
                    break;
                default:
                    //Regex rx = new Regex(@"(-?\d+)", RegexOptions.Compiled);
                    //MatchCollection matches = rx.Matches(e.ToString());
                    ////Console.Write(e + "\t");
                    //    //Console.WriteLine($"Type {e.Type} skipped: {e}");
                    //if (matches.Count > 0)
                    //{
                    //}
                    break;
            }
        }

        private static readonly string inputString = InputLoader.ReadAllText("day12.txt");
    }
}
