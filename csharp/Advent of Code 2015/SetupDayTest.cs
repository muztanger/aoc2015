using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advent_of_Code_2015
{
    /// <summary>
    /// Test class to easily setup new days.
    /// Uncomment and modify the day number, then run the test.
    /// </summary>
    [TestClass]
    public class SetupDayTest
    {
        [TestMethod]
        public async Task SetupDay()
        {
            // Change this to the day you want to setup
            int day = 25;
            
            await AocSetup.SetupDayAsync(day);
        }
    }
}