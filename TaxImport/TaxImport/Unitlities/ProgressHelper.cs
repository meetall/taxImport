namespace TaxImport.Unitlities
{
    public class ProgressHelper
    {
        /// <summary>
        /// Defines how many lines should be processed one time
        /// </summary>
        public const int ReportLine = 35;

        /// <summary>
        /// total size
        /// </summary>
        private readonly long _size = 0;

        /// <summary>
        /// current data has been processed
        /// </summary>
        private long _current = 0;


        public ProgressHelper(long total)
        {            
            _size = total;
        }

        public int GetProgress(int newLen)
        {
            _current += newLen;
            return (int)(_current*100/_size);
        }
    }
}
