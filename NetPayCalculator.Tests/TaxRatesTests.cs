using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetPayCalculator;
using System;

namespace NetPayCalculator.Tests
{
    [TestClass]
    public class TaxRatesTests
    {
        [TestMethod]
        public void CalculateTotalDeductions_Works()
        {
            var taxes = new TaxRates(0.05m, 3);
            decimal gross = 1000m;
            // Federal 10% + State 5% + Benefit 3% = 18% => 180
            Assert.AreEqual(180m, taxes.CalculateTotalDeductions(gross));
        }

        [TestMethod]
        public void CalculateNetPay_Works()
        {
            var taxes = new TaxRates(0.05m, 3);
            decimal gross = 1000m;
            Assert.AreEqual(820m, taxes.CalculateNetPay(gross));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_TotalDeductionsOver100Percent_Throws()
        {
            var taxes = new TaxRates(0.9m, 20); // 10% + 90% + 20% = 120%
        }
    }
}
