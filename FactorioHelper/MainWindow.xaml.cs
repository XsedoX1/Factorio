using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.UI.Xaml;
using System.Diagnostics;

namespace FactorioHelper
{
    public sealed partial class MainWindow : Window
    {

        public MainWindow()
        {

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            

            Title = $"FactorioHelper {versionInfo.FileVersion }";

            this.InitializeComponent();
            MainFrame.Navigate(typeof(Pages.MainPage));
        }

    }
}