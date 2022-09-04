using FactorioHelper.Items;
using FactorioHelper.Logic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FactorioHelper.Pages
{
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            InitializeComponent();

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
    }
}