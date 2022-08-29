using Factorio.Items;
using Factorio.Logic;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Factorio.Pages
{

    public sealed partial class AddItemPage : Page
    {

        public AddItemPage()
        {
            InitializeComponent();

        }

        private void Go_Back(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            AddItemPageListViewController.ListOfAvailableItems = MainPageListViewController.ListOfItems;
            AvailableItemsComboBox.ItemsSource = AddItemPageListViewController.ListOfAvailableItems;
            AddedIngredients.ItemsSource = AddItemPageListViewController.ListOfIngredients;

            if(e.Parameter is Item)
            {
                Item item = e.Parameter as Item;

                foreach(var ingredient in item.Ingredients)
                {
                    AddItemPageListViewController.ListOfIngredients.Add(ingredient);
                    AddItemPageListViewController.ListOfAvailableItems.Remove(ingredient.Item);
                }

                NameBox.Text = item.Name;
                TimeBox.Text = item.TimeToCraft.ToString();
                AmountCraftedBox.Text = item.AmountCrafted.ToString();
                AddEditItemButton.Content = "Edit item";
            }

        }

        private void Add_button(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            AddItemPageListViewController.Add_Button(AvailableItemsComboBox.SelectedItem as Item,
                Int32.Parse(ItemAmountBox.Text),
                Convert.ToDouble(TimeBox.Text));
            ItemAmountBox.Text = "";
        }

        private void OnlyNumbers(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {

            args.Cancel = args.NewText.Any(c =>
            {
                if (c.Equals('.') || char.IsDigit(c))
                    return false;
                else
                    return true;
            });

        }

        private void Remove_Ingredient_Button(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var button = sender as Button;
            Ingredient ingredient = button.DataContext as Ingredient;
            if (ingredient == null) return;
            AddItemPageListViewController.Remove_Ingredient_Button(ingredient);
        }

        private void Add_Whole_Item_Button(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {

            Item item = new Item(NameBox.Text, Convert.ToDouble(TimeBox.Text), Int32.Parse(AmountCraftedBox.Text), AddItemPageListViewController.ListOfIngredients);

            MainPageListViewController.AddEdit_Whole_Item(item);
            
            this.Frame.GoBack();
        }
    }
}