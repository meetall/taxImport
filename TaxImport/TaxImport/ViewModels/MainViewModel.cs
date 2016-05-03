using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using Prism.Commands;
using TaxImport.Models;
using TaxImport.Unitlities;

namespace TaxImport.ViewModels
{
    public delegate void ReportProgress(int progress);    

    public class MainViewModel : BindableBase
    {
        #region Members

        private MainModel _mainModel;

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
            get { return _filePath;}
            set { SetProperty(ref _filePath, value); }
        }

        /// <summary>
        /// Progress value used for progress bar
        /// </summary>
        private int _progressValue;

        public int ProgressValue
        {
            get { return _progressValue; }
            set { SetProperty(ref _progressValue, value); }
        }

        /// <summary>
        /// Control whether import button is enable or not
        /// </summary>
        private bool _buttonEnable = true;

        public bool ButtonEnable
        {
            get { return _buttonEnable; }
            set { SetProperty(ref _buttonEnable, value); }
        }
        #endregion Properties

        #region Delegate Command

        public DelegateCommand ImportClicked { get; set; }

        #endregion Delegate Command

        #region Constructor

        public MainViewModel()
        {
            InitializeDelegateCommand();

            _mainModel = new MainModel();

            _mainModel.PropertyChanged += MainModelPropertyChanged;
        }       

        private void InitializeDelegateCommand()
        {
            ImportClicked = new DelegateCommand(ImportClickedExe);

        }

        #endregion Constructor

        #region Utilities

        private void MainModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ImportResult")
            {
                ImportResult = _mainModel.ImportResult;
            }
            else if (e.PropertyName == "FilePath")
            {
                FilePath = _mainModel.FilePath;
            }
            else if(e.PropertyName =="ProgressValue")
            {
                ProgressValue = _mainModel.ProgressValue;
                if (ProgressValue != 0 && ProgressValue != 100)
                {
                    ButtonEnable = false;
                }
                else
                {
                    ButtonEnable = true;
                }
            }
            
        }

        private void ImportClickedExe()
        {
           _mainModel.ImportButtonClicked();
        }
  
        #endregion Utilities
    }
}
