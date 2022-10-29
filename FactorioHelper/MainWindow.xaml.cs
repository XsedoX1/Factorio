using FactorioHelper.Helpers;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using System.Diagnostics;
using System.IO;

namespace FactorioHelper
{
    public sealed partial class MainWindow : WindowEx
    {

        public MainWindow()
        {

            InitializeComponent();

            AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/icon.ico"));
            Content = null;
            Title = "AppDisplayName".GetLocalized();
        }

    }
}