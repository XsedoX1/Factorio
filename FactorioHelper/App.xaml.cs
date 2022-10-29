using FactorioHelper.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using System;
using WinUIEx;
using Microsoft.Extensions.DependencyInjection;
using FactorioHelper.Activation;
using FactorioHelper.Services;
using FactorioHelper.Interfaces.Services;
using FactorioHelper.Core.Interfaces;
using FactorioHelper.Core.Services;
using FactorioHelper.ViewModels;
using FactorioHelper.Pages;
using FactorioHelper.Logic;
using UnhandledExceptionEventArgs = Microsoft.UI.Xaml.UnhandledExceptionEventArgs;
using FactorioHelper.EFCore.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FactorioHelper
{
    public partial class App : Application
    {

        public static WindowEx MainWindow { get; } = new MainWindow();

        public IHost Host
        {
            get;
        }

        public static T GetService<T>()
            where T : class
        {
            if((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
            {
                throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
            }
            return service;
        }

        public App()
        {
            InitializeComponent();

            Host = Microsoft.Extensions.Hosting.Host
                .CreateDefaultBuilder()
                .UseContentRoot(AppContext.BaseDirectory)
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

                    services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
                    services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
                    services.AddSingleton<IActivationService, ActivationService>();
                    services.AddSingleton<IPageService, PageService>();
                    services.AddSingleton<INavigationService, NavigationService>();

                    services.AddSingleton<IFileService, FileService>();

                    services.AddTransient<ItemInfoViewModel>();
                    services.AddTransient<ItemInfoPage>();

                    services.AddTransient<AddItemViewModel>();
                    services.AddTransient<AddItemPage>();

                    services.AddTransient<MainViewModel>();
                    services.AddTransient<MainPage>();

                    services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
                    services.AddDbContext<SQLiteContext>(options =>
                    {
                        options.UseSqlite(context.Configuration.GetConnectionString("Default"));
                    });

                    services.AddTransient<IDBService, DBService>();


                })
                .Build();

            UnhandledException += App_UnhandledException;

        }

        private void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

        }

        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);

            await App.GetService<IActivationService>().ActivateAsync(args);

        }
    }
}