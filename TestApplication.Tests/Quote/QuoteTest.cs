using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestApplication.Quotes;

namespace TestApplication.Tests.Quote
{
    [TestClass]
    public class QuoteTest
    {
        [TestMethod]
        public void CtreateNewQuoteTest()
        {
            var arg= new[]{"Test.csv", "1000"};
            var req = QuoteRequest.TryCreate(arg);
            var x = Quotes.Quote.TryCreate(req.Value, new QuoteCalculator());
            Assert.AreEqual(1000.0m, x.Value.RequestedAmount);
            Assert.AreEqual(7.0m, x.Value.Rate);
            Assert.AreEqual(30.8770968653716198441163385m, x.Value.MonthlyRepayment);
            Assert.AreEqual(1111.575487153378314388188186m, x.Value.TotalRepayment);
        }



        [TestMethod]
        public void ValidationTest()
        {
            var args = new[] {"Test.csv"};
            var req = QuoteRequest.TryCreate(args);
            Assert.AreEqual("Invalid parameters", req.Message);
        
        }


        [TestMethod]
        public void ValidationTest1()
        {
            string[] args = null;
            var req = QuoteRequest.TryCreate(args);
            Assert.AreEqual("Invalid parameters", req.Message);
        }



        [TestMethod]
        public void ValidationTest2()
        {
            var args = new[] { "Test.csv", "1010" };
            var req = QuoteRequest.TryCreate(args);
            Assert.AreEqual(("Invalid amount value: " + args[1]), req.Message);

        }
    }
}
