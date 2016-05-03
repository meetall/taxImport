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
    public class CurrencyCodeValidatorTest
    {

        [TestMethod]
        public void InvalidCodeReturnFalse()
        {
            Assert.IsFalse(CurrencyCodeValidator.ValidateCurrencyCode("Test"));
        }

        [TestMethod]
        public void ValidCodeReturnTrue()
        {
            Assert.IsTrue(CurrencyCodeValidator.ValidateCurrencyCode("GBP"));
            Assert.IsTrue(CurrencyCodeValidator.ValidateCurrencyCode("gbp"));
            
            Assert.IsTrue(CurrencyCodeValidator.ValidateCurrencyCode("USD"));
        }
    }
}
