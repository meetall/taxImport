using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using Prism.Mvvm;
using TaxImport.Unitlities;

namespace TaxImport.Models
{
    public class MainModel : BindableBase
    {
        #region Members

        private BackgroundWorker _fileReaderWorker;

        #endregion Members

        #region Properties

        /// <summary>
        /// The text will be displayed for reporting the import result
        /// </summary>
        private string _importResult;

        public String ImportResult
        {
            get { return _importResult; }
            set { SetProperty(ref _importResult, value); }
        }

        /// <summary>
        /// Selected file path will be displayed
        /// </summary>
        private string _filePath;

        public String FilePath
        {
            get { return _filePath; }
            set { SetProperty(ref _filePath, value); }
        }

        private int _progressValue;

        public int ProgressValue
        {
            get { return _progressValue; }
            set { SetProperty(ref _progressValue, value); }
        }
        #endregion Properties

        #region Functions

        public void ImportButtonClicked()
        {
            if (SelectFilePath())
            {
                _fileReaderWorker = new BackgroundWorker { WorkerReportsProgress = true };
                _fileReaderWorker.DoWork += ReadFile;
                _fileReaderWorker.ProgressChanged += ReportProgress;

                _fileReaderWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Read the selected file
        /// </summary>
        private void ReadFile(object sender, DoWorkEventArgs e)
        {
            if (File.Exists(_filePath))
            {
                if (Path.GetExtension(_filePath) == ".csv")
                {
                    ImportResult = CSVReader.ReadCSVFile(_filePath, _fileReaderWorker.ReportProgress);
                }
                else if (Path.GetExtension(_filePath) == ".xlsx")
                {
                    ImportResult = XlsxReader.ReadXlsxFile(_filePath, _fileReaderWorker.ReportProgress);
                }
                else
                {
                    MessageBox.Show("The selected file is not in correct Format.");
                }
            }
            else
            {
                MessageBox.Show("The selected file does not exist.");
            }
        }


        /// <summary>
        /// Select the file path to be processed
        /// </summary>
        private bool SelectFilePath()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "csv File|*.csv|xlsx File|*.xlsx",
                FilterIndex = 1,
                Multiselect = false,
                Title = "Please select the tax file to be processed"
            };

            // Call the ShowDialog method to show the dialog box.
            bool? userClickedOk = openFileDialog.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOk == true)
            {
                FilePath = openFileDialog.FileName;
            }

            return userClickedOk != null && userClickedOk.Value;
        }

        private void ReportProgress(object sender, ProgressChangedEventArgs e)
        {
            ProgressValue = e.ProgressPercentage;
        }

        #endregion Functions
    }
}
