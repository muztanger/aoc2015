using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advent_of_Code_2015
{
    [TestClass]
    public class Day04
    {
        private static readonly string inputString = InputLoader.ReadAllText("day04.txt");

        [TestMethod]
        public void Part1Examples()
        {

            Assert.AreEqual(609043, fivezeros("abcdef"));
            Assert.AreEqual(1048970, fivezeros("pqrstuv"));
        }

        private int fivezeros(string key)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                int i = 0;
                string hash = "moop";
                bool found = false;
                while(!found)
                {
                    hash = GetMd5Hash(md5Hash, $"{key}{i}");
                    //Console.WriteLine(hash);
                    found = hash.StartsWith("00000");
                    if (found)
                    {
                        return i;
                    }
                    i++;
                }
            }
            throw new Exception("not found...");
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        [TestMethod]
        public void Part1()
        {
            int result = fivezeros(inputString);
            Assert.AreEqual(282749, result);
            Console.WriteLine(result);
        }

        private int sixzeros(string key)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                int i = 0;
                string hash = "moop";
                bool found = false;
                while (!found)
                {
                    hash = GetMd5Hash(md5Hash, $"{key}{i}");
                    found = hash.StartsWith("000000");
                    if (found)
                    {
                        return i;
                    }
                    i++;
                }
            }
            throw new Exception("not found...");
        }


        [TestMethod]
        public void Part2()
        {
            int result = sixzeros(inputString);
            Assert.AreEqual(9962624, result);
            Console.WriteLine(result);
        }
    }
}
