using System;
using System.IO;
using System.Text;
using TaxImport.DB;
using TaxImport.ViewModels;

namespace TaxImport.Unitlities
{
    /// <summary>
    /// Helper class to parse csv file
    /// </summary>
    public static class CSVReader
    {
        public static string ReadCSVFile(String file, ReportProgress reportProgress)
        {                                              
            // parent should check whether this is a valid CSV file           
            var reader = new StreamReader(File.OpenRead(file));

            int counter = 0;
            int processedData = 0;

            FileInfo info = new FileInfo(file);
            ProgressHelper progressHelper = new ProgressHelper(info.Length);

            // Connect to the database
            TaxModelContainer taxModelContainer = new TaxModelContainer();

            ResultReport resultReport = new ResultReport();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                processedData += Encoding.Default.GetByteCount(line ?? "");

                if (line != null)
                {
                    var values = line.Split(',');

                    if (TaxInfoValidator.IsValidTaxInfo(values, resultReport))
                    {
                        var newTax = new TaxInfo()
                        {
                            Account = values[0],
                            Description = values[1],
                            CurrencyCode = values[2],
                            Value = Convert.ToDouble(values[3])
                        };

                        taxModelContainer.TaxInfoes.Add(newTax);

                        counter++;
                    }                          
                }

                // Report progress every a few lines. The number is defined in Progress helper
                if (counter%ProgressHelper.ReportLine!=0)
                {
                    reportProgress(progressHelper.GetProgress(processedData));
                    processedData = 0;                    
                    taxModelContainer.SaveChanges();
                }

            }

            // Report the final state as complete
            reportProgress(100);
            // Save the unsaved items
            taxModelContainer.SaveChanges();

            return resultReport.GetResultReport(counter);
        } 
    }
}
