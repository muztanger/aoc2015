using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code_2015
{
    /// <summary>
    /// Script to download Advent of Code input and create template test classes.
    /// Usage: Set AOC_SESSION environment variable with your session cookie.
    /// Call: await AocSetup.SetupDayAsync(1);
    /// </summary>
    public static class AocSetup
    {
        private const int Year = 2015;
        private const string BaseUrl = "https://adventofcode.com";

        public static async Task SetupDayAsync(int day)
        {
            if (day < 1 || day > 25)
            {
                throw new ArgumentException("Day must be between 1 and 25", nameof(day));
            }

            string sessionCookie = Environment.GetEnvironmentVariable("AOC_SESSION");
            if (string.IsNullOrEmpty(sessionCookie))
            {
                throw new InvalidOperationException(
                    "AOC_SESSION environment variable not set. " +
                    "Get your session cookie from adventofcode.com (browser dev tools > Application > Cookies)");
            }

            // Download input
            string input = await DownloadInputAsync(day, sessionCookie);
            SaveInput(day, input);

            // Create test class if it doesn't exist
            CreateTestClass(day);

            Console.WriteLine($"Day {day:D2} setup complete!");
        }

        private static async Task<string> DownloadInputAsync(int day, string sessionCookie)
        {
            string url = $"{BaseUrl}/{Year}/day/{day}/input";

            using (var handler = new HttpClientHandler())
            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Add("Cookie", $"session={sessionCookie}");
                //client.DefaultRequestHeaders.Add("User-Agent", "github.com/muztanger/aoc2015 by muztanger@github");

                Console.WriteLine($"Downloading input from {url}...");
                HttpResponseMessage response = await client.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        $"Failed to download input. Status: {response.StatusCode}. " +
                        $"Make sure your session cookie is valid.");
                }

                return await response.Content.ReadAsStringAsync();
            }
        }

        private static void SaveInput(int day, string input)
        {
            // Get the project source directory
            string projectDir = GetProjectDirectory();
            string inputsDir = Path.Combine(projectDir, "Inputs");
            Directory.CreateDirectory(inputsDir);

            string fileName = $"day{day:D2}.txt";
            string filePath = Path.Combine(inputsDir, fileName);

            File.WriteAllText(filePath, input);
            Console.WriteLine($"Input saved to {filePath}");
        }

        private static void CreateTestClass(int day)
        {
            string className = $"Day{day:D2}";
            string fileName = $"{className}.cs";
            
            // Get the project source directory (go up from bin/Debug/net10.0)
            string projectDir = GetProjectDirectory();
            string filePath = Path.Combine(projectDir, fileName);
            
            // Check if file already exists
            if (File.Exists(filePath))
            {
                Console.WriteLine($"{fileName} already exists at {filePath}. Skipping test class creation.");
                return;
            }

            string template = GenerateTestClassTemplate(day, className);
            File.WriteAllText(filePath, template);
            Console.WriteLine($"Test class created: {filePath}");
        }

        private static string GetProjectDirectory()
        {
            // Start from the base directory (bin/Debug/net10.0)
            string baseDir = AppContext.BaseDirectory;
            
            // Navigate up to find the project directory
            // Typically: bin/Debug/net10.0 -> go up 3 levels
            DirectoryInfo dirInfo = new DirectoryInfo(baseDir);
            
            // Go up until we find the directory containing .csproj file
            while (dirInfo != null && dirInfo.Parent != null)
            {
                // Check if this directory contains a .csproj file
                if (dirInfo.GetFiles("*.csproj").Length > 0)
                {
                    return dirInfo.FullName;
                }
                dirInfo = dirInfo.Parent;
            }
            
            // Fallback: go up 3 directories from bin/Debug/net10.0
            dirInfo = new DirectoryInfo(baseDir);
            for (int i = 0; i < 3 && dirInfo.Parent != null; i++)
            {
                dirInfo = dirInfo.Parent;
            }
            
            return dirInfo.FullName;
        }

        private static string GenerateTestClassTemplate(int day, string className)
        {
            return $$"""
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advent_of_Code_2015
{
    [TestClass]
    public class {{className}}
    {
        private static readonly string inputString = InputLoader.ReadAllText("day{{day:D2}}.txt");

        static string exampleInput = @"";

        [TestMethod]
        public void Part1Examples()
        {
            // TODO: Add example tests
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void Part1()
        {
            // TODO: Implement Part 1
            Console.WriteLine("Part 1: Not implemented");
        }

        [TestMethod]
        public void Part2Examples()
        {
            // TODO: Add example tests
            Assert.Fail("Not implemented");
        }

        [TestMethod]
        public void Part2()
        {
            // TODO: Implement Part 2
            Console.WriteLine("Part 2: Not implemented");
        }
    }
}
""";
        }
    }
}