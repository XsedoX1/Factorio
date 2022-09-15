using FactorioHelper.Data;
using FactorioHelper.Items;
using FactorioHelper.Logic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.Storage.Pickers;

namespace FactorioHelper.Pages
{
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            InitializeComponent();

            itemsPathBox.Text = Settings.Path;

            MainPageListViewController.Updater();
            MainListView.ItemsSource = MainPageListViewController.ListOfItems;
        }

        private void Remove_Button(object sender, RoutedEventArgs e)
        {
            MainPageListViewController.Remove_Button(sender, e);
        }

        private void Edit_Button(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Item item = button.DataContext as Item;
            this.Frame.Navigate(typeof(AddItemPage), item);
        }

        public void Add_Item_Button(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddItemPage));
        }

        private void MainListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Item clickedItem = e.ClickedItem as Item;
            if (clickedItem.Ingredients.Count > 0)
                this.Frame.Navigate(typeof(ItemInfoPage), clickedItem);
            else
            {
                NothingToCalculateTip.Subtitle = clickedItem.Name + " has no ingredients.";
                NothingToCalculateTip.IsOpen = true;
            }
        }

        private async void Set_Directory_Button(object sender, RoutedEventArgs e)
        {
            var folderPicker = new FolderPicker();
            folderPicker.FileTypeFilter.Add("*");
            var window = (Application.Current as App)?.Window as MainWindow;
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);
            folderPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;

            Windows.Storage.StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if(folder!=null)
            {
                Settings.Path = folder.Path;
                itemsPathBox.Text = Settings.Path;
            }

        }
    }
}