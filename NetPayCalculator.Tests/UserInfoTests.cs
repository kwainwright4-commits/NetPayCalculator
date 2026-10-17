using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetPayCalculator;
using System;

namespace NetPayCalculator.Tests
{
    [TestClass]
    public class UserInfoTests
    {
        [TestMethod]
        public void GrossPay_NoOvertime_CalculatedCorrectly()
        {
            var user = new UserInfo("Alice", 40m, 10m);
            Assert.AreEqual(400m, user.GrossPay);
        }

        [TestMethod]
        public void GrossPay_WithOvertime_CalculatedCorrectly()
        {
            var user = new UserInfo("Bob", 45m, 10m);
            // 40 * 10 + 5 * 10 * 1.5 = 400 + 75 = 475
            Assert.AreEqual(475m, user.GrossPay);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NegativeHours_Throws()
        {
            var user = new UserInfo("X", -1m, 10m);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NegativePay_Throws()
        {
            var user = new UserInfo("X", 10m, -5m);
        }
    }
}
