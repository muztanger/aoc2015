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
        public async Task SetupDays()
        {
            for (int day = 1; day <= 25; day++)
            {
                await AocSetup.SetupDayAsync(day);
            }
        }
    }
}