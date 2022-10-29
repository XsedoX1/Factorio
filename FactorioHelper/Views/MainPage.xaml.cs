using FactorioHelper.Models.Models;
using FactorioHelper.Services;
using FactorioHelper.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.ObjectModel;
using Windows.Storage.Pickers;

namespace FactorioHelper.Pages
{
    public sealed partial class MainPage : Page
    {

        public MainViewModel ViewModel { get; }
        private DBService _dBService { get; }
        private ObservableCollection<Item> _itemsList { get; set; }

        public MainPage()
        {
            _dBService = App.GetService<DBService>();
            ViewModel = App.GetService<MainViewModel>();
            _itemsList = _dBService.ItemsList;
            MainListView.ItemsSource = _itemsList;
            InitializeComponent();

            //itemsPathBox.Text = Settings.Path;    CHOOSING DIRECTORY NOT USED
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void Remove_Button(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Item? item = button?.DataContext as Item;
            if (item == null) return;
            _dBService.RemoveItem(item);
            _itemsList.Remove(item);

        }

        private void Edit_Button(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Item? item = button?.DataContext as Item;
            this.Frame.Navigate(typeof(AddItemPage), item);
        }

        public void Add_Item_Button(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddItemPage));
        }

        private void MainListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Item? clickedItem = e.ClickedItem as Item;
            if (clickedItem?.Ingredients is not null && clickedItem?.Ingredients.Count > 0)
                this.Frame.Navigate(typeof(ItemInfoPage), clickedItem);
            else
            {
                NothingToCalculateTip.Subtitle = clickedItem?.Name + " has no ingredients.";
                NothingToCalculateTip.IsOpen = true;
            }
        }


    }
}