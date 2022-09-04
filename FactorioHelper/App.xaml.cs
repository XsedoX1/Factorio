using Microsoft.UI.Xaml;



namespace FactorioHelper
{
    public partial class App : Application
    {
        private Window main_window;
        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            main_window = new MainWindow();
            main_window.Activate();
        }
    }
}