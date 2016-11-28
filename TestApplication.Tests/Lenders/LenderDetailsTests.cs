using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestApplication.Exceptions;
using TestApplication.Lenders;

namespace TestApplication.Tests.Lenders
{
    [TestClass]
    public class LenderDetailsTests
    {
        [TestMethod]
        public void CreateTest_Success()
        {
            var x = LenderDetails.Create("RAHUL,7.0,5000");
            Assert.AreEqual("RAHUL", x.Name);
            Assert.AreEqual(7.0m, x.Rate);
            Assert.AreEqual(5000m, x.Amount);
        }
        
        [TestMethod]
        public void CreateTest_TwoColumns()
        {
            try
            {
                var x = LenderDetails.Create("RAHUL,7.0");
                Assert.Fail();
            }
            catch (InvalidCsvLendersFileFormatException e)
            {

            }
        }

        [TestMethod]
        public void CreateTest_RateType()
        {
            try
            {
                var x = LenderDetails.Create("RAHUL,INVALID,5000");
                Assert.Fail();
            }
            catch (InvalidCsvLendersFileFormatException e)
            {

            }
        }

        [TestMethod]
        public void CreateTest_AmountType()
        {
            try
            {
                var x = LenderDetails.Create("RAHUL,7.0,INVALID");
                Assert.Fail();
            }
            catch (InvalidCsvLendersFileFormatException e)
            {

            }
        }

        [TestMethod]
        public void CreateTest_Empty()
        {
            try
            {
                var x = LenderDetails.Create("");
                Assert.Fail();
            }
            catch (InvalidCsvLendersFileFormatException e)
            {

            }
        }
    }
}
