using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Windows.Automation.Peers;
using LinqToExcel;
using TaxImport.DB;
using TaxImport.ViewModels;

namespace TaxImport.Unitlities
{
    public static class XlsxReader
    {
        public static string ReadXlsxFile(string file,ReportProgress reportProgress)
        {           
            // parent should check whether this is a valid CSV file           
            var book = new LinqToExcel.ExcelQueryFactory(file);

            int counter = 0;            

            // Connect to the database
            TaxModelContainer taxModelContainer = new TaxModelContainer();

            ResultReport resultReport = new ResultReport();

            // Define how many lines are scanned each time
            int scanSize = ProgressHelper.ReportLine;
            int total = book.Worksheet("Data").Count();
            int scanGroup = 0;

            ProgressHelper progressHelper = new ProgressHelper(total);

            
            while (scanGroup * scanSize < total)
            {
                var query = book.Worksheet("Data")
                    .ToList()
                    .Skip(scanGroup * scanSize)
                    .Take(scanSize); 
                    //.Select(row => new[]
                    //{
                    //    row["Account"].Cast<string>(),
                    //    row["Description"].Cast<string>(),
                    //    row["CurrencyCode"].Cast<string>(),
                    //    row["Value"].Cast<string>()

                    //})              
                    //.Where(item =>TaxInfoValidator.IsValidTaxInfo(item,resultReport))
                ;

                var enumerable = query as IList<Row> ?? query.ToList();                

                foreach (var q in enumerable)
                {
                    var tempStringArr = new string[4]{q[0],q[1],q[2],q[3]};
                    if (TaxInfoValidator.IsValidTaxInfo(tempStringArr, resultReport))
                    {
                        counter++;
                        taxModelContainer.TaxInfoes.Add(new TaxInfo()
                        {
                            Account = q[0],
                            Description = q[1],
                            CurrencyCode = q[2],
                            Value = Convert.ToDouble(q[3])
                        });
                    }
                }
                taxModelContainer.SaveChanges();
                scanGroup++;
                reportProgress(progressHelper.GetProgress(scanGroup*scanSize));
                
            }

            reportProgress(100);

            return resultReport.GetResultReport(counter);
        }
    }
}
