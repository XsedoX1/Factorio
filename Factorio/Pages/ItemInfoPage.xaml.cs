using Factorio.Items;
using Factorio.Logic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;



namespace Factorio.Pages
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
        }

        private void Go_Back(object sender, RoutedEventArgs e) 
        {
            this.Frame.GoBack();
        }

        private void CalculateListView()
        {
            if (string.IsNullOrEmpty(TargetAmountPerSecond.Text)) return;
            foreach (var element in ListViewElements)
            {
                element.MachinesNeeded = Math.Round(element.AmountNeededCombined / element.Item.AmountPerSec / craftingMultiplier * Convert.ToDouble(TargetAmountPerSecond.Text), 2, MidpointRounding.AwayFromZero);
                element.ItemNeededPerSec = Math.Round(element.AmountNeededCombined/Item.TimeToCraft * Convert.ToDouble(TargetAmountPerSecond.Text), 2, MidpointRounding.AwayFromZero);
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            CalculateListView();
            ItemInfoListView.ItemsSource = ListViewElements;
        }

        private void ChangeMachine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(sender is RadioButtons rb)
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
