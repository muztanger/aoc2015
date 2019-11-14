using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Reflection;
using System.Globalization;

namespace Advent_of_Code_2015
{
    /*
     
    "" is 2 characters of code (the two double quotes), but the string contains zero characters.
    "abc" is 5 characters of code, but 3 characters in the string data.
    "aaa\"aaa" is 10 characters of code, but the string itself contains six "a" characters and a single, escaped quote character, for a total of 7 characters in the string data.
    "\x27" is 6 characters of code, but the string itself contains just one - an apostrophe ('), escaped using hexadecimal notation.

    */

    [TestClass]
    public class Day08
    {
        static string CodePath = "..\\..\\..";
        enum State { Char, Escape, Hex };

        static string Eval(string line)
        {
            string result = "";

            var state = State.Char;
            var tmp = new StringBuilder();
            foreach (char c in line.Substring(1, line.Length - 2))
            {
                if (state == State.Hex)
                {
                    tmp.Append(c);
                    // collect two hex values and then return to State.Char
                    if (tmp.Length == 2)
                    {
                        result += (char) Int16.Parse(tmp.ToString(), NumberStyles.AllowHexSpecifier);
                        tmp.Clear();
                        state = State.Char;
                    }
                }
                else if (state == State.Escape)
                {
                    switch (c)
                    {
                        case 'x':
                        state = State.Hex;
                            break;
                        case '\\':
                        case '"':
                            result += c;
                            state = State.Char;
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
                else
                {
                    switch (c)
                    {
                        case ('\\'):
                            state = State.Escape;
                            break;
                        default:
                            result += c;
                            break;
                    }
                }
            }
            return result;
        }

        [TestMethod]
        public void Part1Examples()
        {
            string path = Assembly.GetAssembly(typeof(Day08)).Location;
            //Console.WriteLine(path);
            using (StreamReader reader = new StreamReader(Path.Combine(path, "day08_part1_examples.txt")))
            {
                string line;
                line = reader.ReadLine();
                Assert.AreEqual(2, line.Length);
                Assert.AreEqual(0, Eval(line).Length);

                line = reader.ReadLine();
                Assert.AreEqual(5, line.Length);
                Assert.AreEqual(3, Eval(line).Length);

                line = reader.ReadLine();
                Assert.AreEqual(10, line.Length);
                Assert.AreEqual(7, Eval(line).Length);

                line = reader.ReadLine();
                Assert.AreEqual(6, line.Length);
                Assert.AreEqual(1, Eval(line).Length);
            }
        }
        
        [TestMethod]
        public void Part1()
        {
            int total = 0;
            using (StreamReader reader = new StreamReader(Path.Combine(CodePath, "day08_input.txt")))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    total += line.Length;
                    total -= Eval(line).Length;
                }
            }

            Console.WriteLine(total);
        }

        static string Encode(string line)
        {
            string result = "";

            foreach (char c in line)
            {
                switch (c)
                {
                    case '"':
                        result += "\\\"";
                        break;
                    case '\\':
                        result += "\\\\";
                        break;
                    default:
                        result += c;
                        break;
                }
            }
            return '"' + result + '"';
        }

        [TestMethod]
        public void Part2Examples()
        {
            //Console.WriteLine(path);
            using (StreamReader reader = new StreamReader(Path.Combine(CodePath, "day08_part1_examples.txt")))
            {
                /*
                    "" encodes to "\"\"", an increase from 2 characters to 6.
                    "abc" encodes to "\"abc\"", an increase from 5 characters to 9.
                    "aaa\"aaa" encodes to "\"aaa\\\"aaa\"", an increase from 10 characters to 16.
                    "\x27" encodes to "\"\\x27\"", an increase from 6 characters to 11.
                */
                string line;
                line = reader.ReadLine();
                Assert.AreEqual(2, line.Length);
                Assert.AreEqual(6, Encode(line).Length);

                line = reader.ReadLine();
                Assert.AreEqual(5, line.Length);
                Assert.AreEqual(9, Encode(line).Length);

                line = reader.ReadLine();
                Assert.AreEqual(10, line.Length);
                Assert.AreEqual(16, Encode(line).Length);

                line = reader.ReadLine();
                Assert.AreEqual(6, line.Length);
                Assert.AreEqual(11, Encode(line).Length);
            }
        }

        [TestMethod]
        public void Part2()
        {
            int total = 0;
            using (StreamReader reader = new StreamReader(Path.Combine(CodePath, "day08_input.txt")))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    total += Encode(line).Length;
                    total -= line.Length;
                }
            }

            Console.WriteLine(total);
        }

        

    }
}
