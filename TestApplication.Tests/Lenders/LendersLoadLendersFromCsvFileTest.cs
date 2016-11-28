using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestApplication.Exceptions;

namespace TestApplication.Tests.Lenders
{
    [TestClass]
    public class LendersLoadLendersFromCsvFileTest
    {
        

        [TestMethod]
        public void LoadLendersFromCsvFileTest()
        {
            const string fileName = "Blah.csv";
            
                var x = TestApplication.Lenders.Lenders.LoadLendersFromCsvFile(fileName);
                Assert.AreEqual(false, x.IsValid);
                Assert.AreEqual("Can't read the CSV file", x.Message);
        }
    }
}