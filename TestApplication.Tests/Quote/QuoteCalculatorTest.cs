using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestApplication.Quotes;

namespace TestApplication.Tests.Quote
{
    [TestClass]
    public class QuoteCalculatorTest
    {
        [TestMethod]
        public void GetQuoteCalculatorTest()
        {
            decimal rate = 7.0m;
            decimal amount = 1000m;
            int termInMonths = 36;
            var x = new QuoteCalculator().CalculateMonthlyRepayment(rate, amount, termInMonths);
            Assert.AreEqual(30.8770968653716198441163385m, x);
        }
    }
}
