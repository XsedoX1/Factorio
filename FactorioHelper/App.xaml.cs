using FactorioHelper.Data;
using Microsoft.UI.Xaml;



namespace FactorioHelper
{
    public partial class App : Application
    {
        public Window Window { get; set; }
        public App()
        {
            this.InitializeComponent();
            if (Settings.Path is null)
                Settings.Path = Constants.PROJECT_DIR;
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            Window = new MainWindow();
            Window.Activate();
            
        }
    }
}