
using FactorioHelper.Logic;
using FactorioHelper.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using Windows.Storage.Pickers;

namespace FactorioHelper.Pages
{
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            InitializeComponent();

            //itemsPathBox.Text = Settings.Path;    CHOOSING DIRECTORY NOT USED
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            MainListView.ItemsSource = SQLiteDBInterace.ItemsList;


        }

        private void Remove_Button(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Item item = button.DataContext as Item;
            if (item == null) return;
            SQLiteDBInterace.RemoveItem(item);

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
            if (clickedItem.Ingredients is not null && clickedItem.Ingredients.Count > 0)
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
            if (folder != null)
            {
                //itemsPathBox.Text = Settings.Path;    CHOOSING DIRECTORY NOT USED
            }

        }


    }
}