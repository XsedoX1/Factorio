using FactorioHelper.Logic;
using FactorioHelper.Models;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FactorioHelper.Pages
{
    public sealed partial class AddItemPage : Page
    {
        private Item item;

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
            base.OnNavigatedTo(e);
            item = null;
            AddItemPageListViewController.ListOfAvailableItems.Clear();
            AddItemPageListViewController.ListOfIngredients.Clear();

            AddItemPageListViewController.ListOfAvailableItems = SQLiteDBInterace.ItemsList;
            AvailableItemsComboBox.ItemsSource = AddItemPageListViewController.ListOfAvailableItems;
            AddedIngredients.ItemsSource = AddItemPageListViewController.ListOfIngredients;

            if (e.Parameter is Item)
            {
                item = e.Parameter as Item;

                AddItemPageListViewController.ListOfAvailableItems.Remove(item);
                foreach (var ingredient in item.Ingredients)
                {
                    AddItemPageListViewController.ListOfIngredients.Add(ingredient);
                    AddItemPageListViewController.ListOfAvailableItems.Remove(ingredient.Item);
                }

                machineChooser.SelectedIndex = item.IsAssemblingMachine;
                NameBox.Text = item.Name;
                TimeBox.Text = item.TimeToCraft.ToString();
                AmountCraftedBox.Text = item.AmountCrafted.ToString();
                AddEditItemButton.Content = "Edit item";
            }

        }


        private void Add_button(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (!int.TryParse((ItemAmountBox.Text), out int AmountNeededInt) || string.IsNullOrEmpty(ItemAmountBox.Text))
            {
                IngredientAmountBoxTip.IsOpen = true;
            }
            else if (!double.TryParse(TimeBox.Text, out double time) || string.IsNullOrEmpty(TimeBox.Text))
            {
                TimeBoxTip.IsOpen = true;
            }
            else if (AvailableItemsComboBox.SelectedIndex == -1)
            {
                ComboBoxTip.IsOpen = true;
            }
            else
            {
                AddItemPageListViewController.Add_Button(AvailableItemsComboBox.SelectedItem as Item,
                                AmountNeededInt,
                                time);
                ItemAmountBox.Text = "";
            }
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

            if (string.IsNullOrEmpty(NameBox.Text))
            {
                NameBoxTip.IsOpen = true;
            }
            else if (!int.TryParse((AmountCraftedBox.Text), out int AmountCraftedInt) || string.IsNullOrEmpty(AmountCraftedBox.Text))
            {
                AmountCraftedBoxTip.IsOpen = true;
            }
            else if (!double.TryParse(TimeBox.Text, out double time) || string.IsNullOrEmpty(TimeBox.Text))
            {
                TimeBoxTip.IsOpen = true;
            }
            else
            {
                if(item is not null)
                {
                    using(var db = new SQLiteContext())
                    {
                        db.Items.Attach(item);
                        foreach (Ingredient ingredient in AddItemPageListViewController.ListOfIngredients)
                            db.Items.Attach(ingredient.Item);

                        item.Name = NameBox.Text;
                        item.TimeToCraft = time;
                        item.AmountCrafted = AmountCraftedInt;
                        item.IsAssemblingMachine = machineChooser.SelectedIndex;
                        item.Ingredients = AddItemPageListViewController.ListOfIngredients;
                        db.SaveChanges();
                    }
                }
                else
                {
                    item = new Item(NameBox.Text, time, AmountCraftedInt, machineChooser.SelectedIndex, AddItemPageListViewController.ListOfIngredients);
                    SQLiteDBInterace.SaveItem(item);
                }
                

                
                this.Frame.GoBack();

            }
        }

    }
}