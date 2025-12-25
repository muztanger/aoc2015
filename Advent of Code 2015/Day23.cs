using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advent_of_Code_2015
{
    /// <summary>
    /// Day 23: Opening the Turing Lock
    /// Simple assembly interpreter with registers a and b.
    /// Instructions: hlf, tpl, inc, jmp, jie, jio
    /// Part 1: Start with a=0, b=0. What's b after execution?
    /// Part 2: Start with a=1, b=0. What's b after execution?
    /// </summary>
    [TestClass]
    public class Day23
    {
        private static readonly string inputString = InputLoader.ReadAllText("day23.txt");

        static string exampleInput = @"inc a
jio a, +2
tpl a
inc a";

        class Computer
        {
            public long A { get; set; }
            public long B { get; set; }
            private int pc;
            private List<string[]> instructions;

            public Computer(string program, long initialA = 0, long initialB = 0)
            {
                A = initialA;
                B = initialB;
                pc = 0;
                instructions = program.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                    .Select(line => line.Trim().Replace(",", "").Split(' '))
                    .ToList();
            }

            public void Execute()
            {
                while (pc >= 0 && pc < instructions.Count)
                {
                    var parts = instructions[pc];
                    string op = parts[0];

                    switch (op)
                    {
                        case "hlf": // half
                            if (parts[1] == "a") A /= 2;
                            else B /= 2;
                            pc++;
                            break;

                        case "tpl": // triple
                            if (parts[1] == "a") A *= 3;
                            else B *= 3;
                            pc++;
                            break;

                        case "inc": // increment
                            if (parts[1] == "a") A++;
                            else B++;
                            pc++;
                            break;

                        case "jmp": // jump
                            pc += int.Parse(parts[1]);
                            break;

                        case "jie": // jump if even
                            long val = parts[1] == "a" ? A : B;
                            if (val % 2 == 0)
                                pc += int.Parse(parts[2]);
                            else
                                pc++;
                            break;

                        case "jio": // jump if one
                            val = parts[1] == "a" ? A : B;
                            if (val == 1)
                                pc += int.Parse(parts[2]);
                            else
                                pc++;
                            break;

                        default:
                            throw new Exception($"Unknown instruction: {op}");
                    }
                }
            }
        }

        [TestMethod]
        public void Part1Examples()
        {
            var computer = new Computer(exampleInput);
            computer.Execute();
            Console.WriteLine($"Example: a={computer.A}, b={computer.B}");
            Assert.AreEqual(2L, computer.A);
        }

        [TestMethod]
        public void Part1()
        {
            var computer = new Computer(inputString);
            computer.Execute();
            Assert.AreEqual(184, (int)computer.B);
            Console.WriteLine($"Part 1: Register b = {computer.B}");
        }

        [TestMethod]
        public void Part2Examples()
        {
            var computer = new Computer(exampleInput, initialA: 1);
            computer.Execute();
            Console.WriteLine($"Example with a=1: a={computer.A}, b={computer.B}");
        }

        [TestMethod]
        public void Part2()
        {
            var computer = new Computer(inputString, initialA: 1);
            computer.Execute();
            Assert.AreEqual(231, (int)computer.B);
            Console.WriteLine($"Part 2: Register b = {computer.B}");
        }
    }
}