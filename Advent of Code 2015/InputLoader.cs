using System;
using System.IO;

namespace Advent_of_Code_2015
{
    internal static class InputLoader
    {
        public static string ReadAllText(string fileName)
        {
            var baseDir = AppContext.BaseDirectory;
            var path = Path.Combine(baseDir, "Inputs", fileName);
            return File.ReadAllText(path);
        }
    }
}
