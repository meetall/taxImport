using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaxImport.UnitTest
{
    [TestClass]
    public class DataHelper
    {
        //[TestMethod]
        public void CreateTestCSV()
        {
            using (StreamWriter sw = new StreamWriter("taxTestCSV.csv"))
            {
                for (int i = 0; i < 1000; i++)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append("Account" + i+",");
                    sb.Append("Description" + i + ",");
                    sb.Append("GBP,");
                    sb.Append(i);
                    sw.WriteLine(sb.ToString());
                }
                
            }
        }
    }
}
