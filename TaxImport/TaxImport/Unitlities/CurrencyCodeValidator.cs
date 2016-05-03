using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TaxImport.Unitlities
{
    public static class CurrencyCodeValidator
    {
        private static List<string> allIsoCurrencySymboList;

        /// <summary>
        /// Create the Symbol list in constructor
        /// </summary>
        static CurrencyCodeValidator()
        {            
            allIsoCurrencySymboList = CultureInfo.GetCultures(CultureTypes.AllCultures)
                        .Where(c => !c.IsNeutralCulture)
                        .Select(culture =>
                        {
                            try
                            {
                                return new RegionInfo(culture.LCID);
                            }
                            catch
                            {
                                return null;
                            }
                        })
                        .Where(c=>c!=null)
                        .Select(c=>c.ISOCurrencySymbol)
                        .ToList();
        }
        

        public static bool ValidateCurrencyCode(string code)
        {
            var symbol = allIsoCurrencySymboList.FirstOrDefault(c => c == code.ToUpper());

            return !String.IsNullOrEmpty(symbol);
        }
    }
}
