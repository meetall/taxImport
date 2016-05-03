using System;

namespace TaxImport.Unitlities
{
    public static class TaxInfoValidator
    {
        public static bool IsValidTaxInfo(string[] line, ResultReport report)
        {
            double result;
            if (String.IsNullOrEmpty(line[0]) ||
                String.IsNullOrEmpty(line[1]) ||
                !CurrencyCodeValidator.ValidateCurrencyCode(line[2]) ||
                !Double.TryParse(line[3], out result))
            {
                report.AppendError(line);
                return false;
            }

            return true;
        }
    }
}
