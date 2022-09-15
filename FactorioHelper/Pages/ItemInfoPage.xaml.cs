using FactorioHelper.Items;
using FactorioHelper.Logic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.ObjectModel;



namespace FactorioHelper.Pages
{
    public sealed partial class ItemInfoPage : Page
    {
        public Item Item { get; set; }
        public ObservableCollection<SummedIngredient> ListViewElements = new ObservableCollection<SummedIngredient>();
        private double craftingMultiplier = 0.5;

        public ItemInfoPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Item = e.Parameter as Item;
            TitleItem.Text = Item.Name;
            ItemInfoPageListViewController.FlattenIngredients(Item, ListViewElements);
            ItemPerSec.Text = Item.Name + "/s";
            amountOfMAchinesMainItemBlock.Text = "The amount of machines that craft " + Item.Name + ":";
        }

        private void Go_Back(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void CalculateListView()
        {
            
            if (!double.TryParse(TargetAmountPerSecond.Text, out double targetAmountPerSec) || string.IsNullOrEmpty(TargetAmountPerSecond.Text))
            {
                AmountPerSecBoxTip.IsOpen = true;
            }
            else if (ChangeMachine.SelectedIndex == -1)
            {
                ChangeMachineTip.IsOpen = true;
            }
            else
            {
                double mainItemAmountOfMachines = Math.Round(targetAmountPerSec / Item.AmountPerSec /craftingMultiplier, 2, MidpointRounding.AwayFromZero);
                double allMachinesCombined = mainItemAmountOfMachines;
                foreach (var element in ListViewElements)
                {
                    element.MachinesNeeded = Math.Round(((element.AmountNeededCombined / element.Item.AmountPerSec) / craftingMultiplier) * (targetAmountPerSec/Item.AmountCrafted), 2, MidpointRounding.AwayFromZero);
                    element.ItemNeededPerSec = Math.Round(element.AmountNeededCombined / (Item.TimeToCraft * targetAmountPerSec), 2, MidpointRounding.AwayFromZero);
                    allMachinesCombined += element.MachinesNeeded;
                }
                ItemInfoListView.ItemsSource = ListViewElements;
                allMachinesCombined = Math.Round(allMachinesCombined, 2, MidpointRounding.AwayFromZero);
                AllMachines.Text = allMachinesCombined.ToString();
                mainItemsMachinesBox.Text = mainItemAmountOfMachines.ToString();
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            CalculateListView();
        }

        private void ChangeMachine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!double.TryParse(TargetAmountPerSecond.Text, out double _) || string.IsNullOrEmpty(TargetAmountPerSecond.Text))
                return;

            if (sender is RadioButtons rb)
            {
                string craftingMetchod = rb.SelectedItem as string;

                switch (craftingMetchod)
                {

                    case "Assembling Machine 1":
                        craftingMultiplier = 0.5;
                        CalculateListView();
                        break;

                    case "Assembling Machine 2":
                        craftingMultiplier = 0.75;
                        CalculateListView();
                        break;

                    case "Assembling Machine 3":
                        craftingMultiplier = 1.25;
                        CalculateListView();
                        break;
                }
            }
        }
    }


}
