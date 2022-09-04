using Microsoft.UI.Xaml;


namespace FactorioHelper
{
    public sealed partial class MainWindow : Window
    {

        public MainWindow()
        {
            this.InitializeComponent();
            MainFrame.Navigate(typeof(Pages.MainPage));
        }
    }
}