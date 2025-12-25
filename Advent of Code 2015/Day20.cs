using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advent_of_Code_2015
{
    [TestClass]
    public class Day20
    {
        class SumFactors
        {
            static long[] SmallesPrimeFactor = new long[0];

            public static void Sieve(long MAX)
            {
                MAX++;
                if (SmallesPrimeFactor?.Count() > MAX) return;
                SmallesPrimeFactor = new long[MAX];

                SmallesPrimeFactor[1] = 1;
                for (long i = 2; i < MAX; i++)
                {
                    SmallesPrimeFactor[i] = i;
                }
                for (long i = 4; i < MAX; i += 2)
                {
                    SmallesPrimeFactor[i] = 2;
                }
                for (long i = 3; i * i < MAX; i++)
                {
                    if (SmallesPrimeFactor[i] == i)
                    {
                        for (long j = i * i; j < MAX; j += i)
                        {
                            if (SmallesPrimeFactor[j] == j)
                            {
                                SmallesPrimeFactor[j] = i;
                            }
                        }
                    }
                }
            }

            public static List<long> Factorization(long x)
            {
                x = (long)Math.Abs(x);
                var ret = new List<long>();
                while (x != 1 && x != 0)
                {
                    ret.Add(SmallesPrimeFactor[x]);
                    x /= SmallesPrimeFactor[x];
                }
                return ret;
            }

            public static long SumDivisors(long n)
            {
                var dict = new Dictionary<long, (long, long, long)>();
                foreach (var x in Factorization(n))
                {
                    if (dict.ContainsKey(x))
                    {
                        (var factor, var sum, var count) = dict[x];
                        factor *= x;
                        sum += factor;
                        count++;
                        dict[x] = (factor, sum, count);
                    }
                    else
                    {
                        dict[x] = (x, 1 + x, 1);
                    }
                }
                if (dict.Count == 0) return 1;
                return dict.Select(kv => kv.Value.Item2).Aggregate((x, y) => x * y);
            }
        }


        [TestMethod]
        [DataRow(1, 10)]
        [DataRow(2, 30)]
        [DataRow(3, 40)]
        [DataRow(4, 70)]
        [DataRow(5, 60)]
        [DataRow(6, 120)]
        [DataRow(7, 80)]
        [DataRow(8, 150)]
        [DataRow(9, 130)]
        public void ExamplePart1(int n, int expected)
        {
            SumFactors.Sieve(100);
            Assert.AreEqual(expected, SumFactors.SumDivisors(n) * 10);
        }

        [TestMethod]
        public void Part1()
        {
            int n = 1000000;
            SumFactors.Sieve(n);
            long firstNumber = -1;
            for (int i = 10000; i < n; i++)
            {
                long x = SumFactors.SumDivisors(i) * 10;
                if (x > 34000000)
                {
                    firstNumber = i;
                    break;
                }
            }
            Assert.AreEqual(786240, firstNumber);
        }

        [TestMethod]
        public void Part2()
        {
            int target = 34000000;
            int maxHouses = 1000000;
            long[] houses = new long[maxHouses];
            
            // Each elf delivers to only 50 houses
            for (int elf = 1; elf < maxHouses; elf++)
            {
                int visitCount = 0;
                for (int house = elf; house < maxHouses && visitCount < 50; house += elf)
                {
                    houses[house] += elf * 11;
                    visitCount++;
                }
            }
            
            long firstNumber = -1;
            for (int i = 1; i < maxHouses; i++)
            {
                if (houses[i] >= target)
                {
                    firstNumber = i;
                    break;
                }
            }
            
            Assert.AreEqual(831600, firstNumber);
        }
    }
}
