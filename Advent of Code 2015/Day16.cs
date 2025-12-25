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
    public class Day16
    {

        [TestMethod]
        public void Part1()
        {
            /*  children: 3
                cats: 7
                samoyeds: 2
                pomeranians: 3
                akitas: 0
                vizslas: 0
                goldfish: 5
                trees: 3
                cars: 2
                perfumes: 1
                */
            var properties = new Dictionary<string, int>()
            {
                {"children", 3 },
                {"cats", 7 },
                {"samoyeds", 2 },
                {"pomeranians", 3 },
                {"akitas", 0 },
                {"vizslas", 0 },
                {"goldfish", 5 },
                {"trees", 3 },
                {"cars", 2 },
                {"perfumes",1  } 
            };

            List<int> matches = new List<int>();
            using (StringReader reader = new StringReader(inputString))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Sue 1: cars: 9, akitas: 3, goldfish: 0
                    var split = Regex.Split(line, "[ :,]+");
                    // Sue 1 cars 9 akitas 3 goldfish 0
                    string key = "";
                    int sue = -1;
                    bool match = true;
                    foreach (var elem in split)
                    {
                        if (key == "")
                        {
                            key = elem;
                        }
                        else if (key == "Sue")
                        {
                            sue = Int32.Parse(elem);
                            key = "";
                        }
                        else
                        {
                            var count = Int32.Parse(elem);
                            if (properties.ContainsKey(key))
                            {
                                if (count != properties[key])
                                {
                                    match = false;
                                    break;
                                }
                            }
                            else
                            {
                                match = false;
                                break;
                            }

                            key = "";
                        }
                    }
                    if (match)
                    {
                        matches.Add(sue);
                    }
                }
            }
            string result = "Matches: " + String.Join(", ", matches);
            Assert.AreEqual("Matches: 213", result);
            Console.WriteLine(result);
        }


        [TestMethod]
        public void Part2()
        {
            var properties = new Dictionary<string, int>()
            {
                {"children", 3 },
                {"cats", 7 },
                {"samoyeds", 2 },
                {"pomeranians", 3 },
                {"akitas", 0 },
                {"vizslas", 0 },
                {"goldfish", 5 },
                {"trees", 3 },
                {"cars", 2 },
                {"perfumes",1  }
            };

            List<int> matches = new List<int>();
            using (StringReader reader = new StringReader(inputString))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Sue 1: cars: 9, akitas: 3, goldfish: 0
                    var split = Regex.Split(line, "[ :,]+");
                    // Sue 1 cars 9 akitas 3 goldfish 0
                    string key = "";
                    int sue = -1;
                    bool match = true;
                    foreach (var elem in split)
                    {
                        if (key == "")
                        {
                            key = elem;
                        }
                        else if (key == "Sue")
                        {
                            sue = Int32.Parse(elem);
                            key = "";
                        }
                        else
                        {
                            var count = Int32.Parse(elem);
                            if (properties.ContainsKey(key))
                            {
                                switch (key)
                                {
                                    case "cats":
                                    case "trees":
                                        // cats. It doesn't differentiate individual breeds.
                                        // the cats and trees readings indicates that there are greater than
                                        if (count <= properties[key]) match = false;
                                        break;
                                    case "pomeranians":
                                    case "goldfish":
                                        // pomeranians and goldfish readings indicate that there are fewer than
                                        if (count >= properties[key]) match = false;
                                        break;
                                    default:
                                        if (count != properties[key]) match = false;
                                        break;
                                }
                                if (!match) break;
                            }
                            else
                            {
                                match = false;
                                break;
                            }

                            key = "";
                        }
                    }
                    if (match)
                    {
                        matches.Add(sue);
                    }
                }
            }
            string result = "Matches: " + String.Join(", ", matches);
            Assert.AreEqual("Matches: 323", result);
            Console.WriteLine(result);
        }



        private static readonly string inputString = InputLoader.ReadAllText("day16.txt");
    }
}
