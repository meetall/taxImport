using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxImport.Unitlities;

namespace TaxImport.UnitTest
{
    [TestClass]
    public class ProgressHelperTest
    {
        [TestMethod]
        public void ProgressReportTest()
        {
            ProgressHelper testProgressHelper = new ProgressHelper(1000);

            Assert.AreEqual(1, testProgressHelper.GetProgress(10));
            Assert.AreEqual(2, testProgressHelper.GetProgress(10));
            Assert.AreEqual(12,testProgressHelper.GetProgress(100));
        }
    }
}
