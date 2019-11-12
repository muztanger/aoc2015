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
    public class Day9
    {

        private readonly Dictionary<(string, string), int> Distances = new Dictionary<(string, string), int>();
        private readonly HashSet<string> Cities = new HashSet<string>();

        static string exampleString = @"London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141";

        [TestMethod]
        public void Part1Examples()
        {
            using (StringReader reader = new StringReader(exampleString))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    parseLine(line);
                }
            }

            int min = shortestPath();
            Assert.AreEqual(605, min);

        }

        private int shortestPath()
        {
            int min = int.MaxValue;
            ForAllPermutation(Cities.ToArray(), (vals) =>
            {
                int sum = 0;
                string last = null;
                foreach (string city in vals)
                {
                    if (last != null)
                    {
                        sum += Distances[(last, city)];
                    }
                    last = city;
                }
                min = Math.Min(min, sum);
                //Console.WriteLine($"{String.Join(", ", vals)}: {sum}");
                return false;
            });
            return min;
        }

        private void parseLine(string line)
        {
            var parts = line.Split();
            var from = parts[0];
            var to = parts[2];
            var distanse = Int32.Parse(parts[4]);

            Cities.Add(from);
            Cities.Add(to);

            Distances[(from, to)] = distanse;
            Distances[(to, from)] = distanse;
        }

        // Found on https://stackoverflow.com/questions/955982/tuples-or-arrays-as-dictionary-keys-in-c-sharp
        /// <summary>
        /// EO: 2016-04-14
        /// Generator of all permutations of an array of anything.
        /// Base on Heap's Algorithm. See: https://en.wikipedia.org/wiki/Heap%27s_algorithm#cite_note-3
        /// </summary>
        //public static class Permutations
        //{
        /// <summary>
        /// Heap's algorithm to find all pmermutations. Non recursive, more efficient.
        /// </summary>
        /// <param name="items">Items to permute in each possible ways</param>
        /// <param name="funcExecuteAndTellIfShouldStop"></param>
        /// <returns>Return true if cancelled</returns> 
        public static bool ForAllPermutation<T>(T[] items, Func<T[], bool> funcExecuteAndTellIfShouldStop)
        {
            int countOfItem = items.Length;

            if (countOfItem <= 1)
            {
                return funcExecuteAndTellIfShouldStop(items);
            }

            var indexes = new int[countOfItem];
            for (int i = 0; i < countOfItem; i++)
            {
                indexes[i] = 0;
            }

            if (funcExecuteAndTellIfShouldStop(items))
            {
                return true;
            }

            for (int i = 1; i < countOfItem;)
            {
                if (indexes[i] < i)
                { // On the web there is an implementation with a multiplication which should be less efficient.
                    if ((i & 1) == 1) // if (i % 2 == 1)  ... more efficient ??? At least the same.
                    {
                        Swap(ref items[i], ref items[indexes[i]]);
                    }
                    else
                    {
                        Swap(ref items[i], ref items[0]);
                    }

                    if (funcExecuteAndTellIfShouldStop(items))
                    {
                        return true;
                    }

                    indexes[i]++;
                    i = 1;
                }
                else
                {
                    indexes[i++] = 0;
                }
            }

            return false;
        }

        /// <summary>
        /// This function is to show a linq way but is far less efficient
        /// From: StackOverflow user: Pengyang : http://stackoverflow.com/questions/756055/listing-all-permutations-of-a-string-integer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        /// <summary>
        /// Swap 2 elements of same type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        [TestMethod]
        public void Part1()
        {
            using (StringReader reader = new StringReader(inputString))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    parseLine(line);
                }
            }

            int min = shortestPath();
            Console.WriteLine(min);
        }


        [TestMethod]
        public void Part2Examples()
        {

        }

        [TestMethod]
        public void Part2()
        {
            using (StringReader reader = new StringReader(inputString))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    parseLine(line);
                }
            }

            int max = int.MinValue;
            ForAllPermutation(Cities.ToArray(), (vals) =>
            {
                int sum = 0;
                string last = null;
                foreach (string city in vals)
                {
                    if (last != null)
                    {
                        sum += Distances[(last, city)];
                    }
                    last = city;
                }
                max = Math.Max(max, sum);
                //Console.WriteLine($"{String.Join(", ", vals)}: {sum}");
                return false;
            });

            Console.WriteLine(max);
        }

        static string inputString = @"Tristram to AlphaCentauri = 34
Tristram to Snowdin = 100
Tristram to Tambi = 63
Tristram to Faerun = 108
Tristram to Norrath = 111
Tristram to Straylight = 89
Tristram to Arbre = 132
AlphaCentauri to Snowdin = 4
AlphaCentauri to Tambi = 79
AlphaCentauri to Faerun = 44
AlphaCentauri to Norrath = 147
AlphaCentauri to Straylight = 133
AlphaCentauri to Arbre = 74
Snowdin to Tambi = 105
Snowdin to Faerun = 95
Snowdin to Norrath = 48
Snowdin to Straylight = 88
Snowdin to Arbre = 7
Tambi to Faerun = 68
Tambi to Norrath = 134
Tambi to Straylight = 107
Tambi to Arbre = 40
Faerun to Norrath = 11
Faerun to Straylight = 66
Faerun to Arbre = 144
Norrath to Straylight = 115
Norrath to Arbre = 135
Straylight to Arbre = 127";
        }
    }
