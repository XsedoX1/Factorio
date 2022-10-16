using FactorioHelper.Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;


namespace FactorioHelper
{
    public partial class App : Application
    {
        
        public Window Window { get; set; }
        public App()
        {
            this.InitializeComponent();

            
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            Window = new MainWindow();
            Window.Activate();

            using(var db= new SQLiteContext())
            {
                db.Database.Migrate();
            }
            
        }
    }
}