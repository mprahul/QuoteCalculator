using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestApplication.Tests.Lenders
{
    [TestClass]
    public class LendersGetLenderByAmountTest
    {
        [TestMethod]
        public void GetLenderByAmountTest()
        {
            const string fileName = "Test.csv";

            var lendersList = TestApplication.Lenders.Lenders.LoadLendersFromCsvFile(fileName);
            var x = lendersList.Value.GetLenderByAmount(1000m);
            Assert.AreEqual(4800m,x.Value.Amount);
            Assert.AreEqual("Jane",x.Value.Name);
            Assert.AreEqual(7.0m,x.Value.Rate);
            
        }

        
        [TestMethod]
        public void GetLenderByAmountTestToFail()
        {
            const string fileName = "Test.csv";
            var lendersList = TestApplication.Lenders.Lenders.LoadLendersFromCsvFile(fileName);
            var x = lendersList.Value.GetLenderByAmount(16500m);
            Assert.AreEqual("No lenders available for the requested amount",x.Message);
        }
    }
}
