using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
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
#if DEBUG
            var watch = System.Diagnostics.Stopwatch.StartNew();
#endif
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

            // Due to the limitation of Linq to Excel, worksheet has to call to list 
            // to make Skip work.
            var data = book.Worksheet("Data").ToList();
            
            while (scanGroup * scanSize < total)
            {
                var query = data
                    .Skip(scanGroup * scanSize)
                    .Take(scanSize);                  
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
                reportProgress(progressHelper.GetProgress(scanSize));
                
            }

            reportProgress(100);
#if DEBUG
            watch.Stop();
#endif
            Debug.WriteLine(watch.ElapsedMilliseconds);

            return resultReport.GetResultReport(counter);
        }
    }
}
