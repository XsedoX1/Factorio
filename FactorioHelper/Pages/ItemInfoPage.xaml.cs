using FactorioHelper.Logic;
using FactorioHelper.Models;
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
            ItemInfoListView.ItemsSource = ListViewElements;
            TitleItem.Text = Item.Name;
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
                double mainItemAmountOfMachines;
                if (Item.IsAssemblingMachine==1)
                    mainItemAmountOfMachines = targetAmountPerSec / Item.AmountPerSec/craftingMultiplier;
                else
                    mainItemAmountOfMachines = targetAmountPerSec / Item.AmountPerSec;

                double allMachinesCombined = mainItemAmountOfMachines;
                ItemInfoPageListViewController.FlattenIngredients(Item, ListViewElements, mainItemAmountOfMachines);

                foreach (var element in ListViewElements)
                {
                    if(element.Item.IsAssemblingMachine==1)
                    {
                        element.MachinesNeeded = Math.Round(element.AmountNeededCombinedPerSec / element.Item.AmountPerSec / craftingMultiplier, 2, MidpointRounding.AwayFromZero);
                        allMachinesCombined += element.AmountNeededCombinedPerSec / element.Item.AmountPerSec / craftingMultiplier;
                    }
                    else
                    {
                        element.MachinesNeeded = Math.Round(element.AmountNeededCombinedPerSec / element.Item.AmountPerSec, 2, MidpointRounding.AwayFromZero);
                        allMachinesCombined += element.AmountNeededCombinedPerSec / element.Item.AmountPerSec;
                    }
                }

                allMachinesCombined = Math.Round(allMachinesCombined, 2, MidpointRounding.AwayFromZero);
                AllMachines.Text = allMachinesCombined.ToString();
                mainItemsMachinesBox.Text = Math.Round(mainItemAmountOfMachines,2,MidpointRounding.AwayFromZero).ToString();
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
