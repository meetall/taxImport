using System.Text;

namespace TaxImport.Unitlities
{
    public class ResultReport
    {
        #region Members

        private StringBuilder stringBuilder;
        private bool errorExists = false;

        #endregion Members

        #region Constructor

        public ResultReport()
        {
            stringBuilder = new StringBuilder();    
        }

        #endregion Constructor

        public void AppendError(string[] strings)
        {
            if (!errorExists)
            {
                stringBuilder.AppendLine("The following lines are not imported:");
            }
            stringBuilder.AppendLine("Account : " + (strings[0] ?? "")
                                     + " Description :" + (strings[1] ?? "")
                                     + " CurrencyCode :" + (strings[2] ?? "")
                                     + " Value :" + (strings[3] ?? ""));
        }

        public string GetResultReport(int processedLines)
        {
            stringBuilder.AppendLine(processedLines + (processedLines > 1 ? " lines are " : "line is ") + "imported.");
            return stringBuilder.ToString();
        }
    }
}
