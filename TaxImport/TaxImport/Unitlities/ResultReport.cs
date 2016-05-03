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

        #region Functions
        /// <summary>
        /// Add the error message to the report
        /// </summary>
        /// <param name="strings"></param>
        public void AppendError(string[] strings)
        {
            if (!errorExists)
            {
                stringBuilder.AppendLine("The following lines are not imported:");
                errorExists = true;
            }
            stringBuilder.AppendLine("Account : " + (strings[0] ?? "")
                                     + " Description :" + (strings[1] ?? "")
                                     + " CurrencyCode :" + (strings[2] ?? "")
                                     + " Value :" + (strings[3] ?? ""));
        }

        /// <summary>
        /// This function is called in the last. The result report will be generated.
        /// </summary>
        /// <param name="processedLines"></param>
        /// <returns></returns>
        public string GetResultReport(int processedLines)
        {
            stringBuilder.AppendLine(processedLines + (processedLines > 1 ? " lines are " : "line is ") + "imported.");
            return stringBuilder.ToString();
        }
        #endregion Functions
    }

    
}
