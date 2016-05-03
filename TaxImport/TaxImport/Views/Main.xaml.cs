using System.Windows;
using TaxImport.ViewModels;

namespace TaxImport.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();

            var mainViewModel = new MainViewModel();
            DataContext = mainViewModel;
        }
    }
}
