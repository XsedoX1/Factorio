using Factorio.Data;
using Factorio.Items;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.IO;

namespace Factorio.Logic;

public static class MainPageListViewController
{
    public static ObservableCollection<Item> ListOfItems { get; set; } = new ObservableCollection<Item>();


    public static void Updater()
    {
        ListOfItems.Clear();
        ListOfItems = JsonController.Deserializer();
    }

    public static void RemoveItem(Item item)
    {
        ListOfItems.Remove(item);
        if (File.Exists(Constants.PROJECT_DIR + "Items/ItemsDB/" + item.Id + ".json"))
            File.Delete(Constants.PROJECT_DIR + "Items/ItemsDB/" + item.Id + ".json");

    }

    public static void Remove_Button(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        Item item = button.DataContext as Item;
        if (item == null) return;
        RemoveItem(item);
    }

    public static void AddEdit_Whole_Item(Item item)
    {
        RemoveItem(item);
        ListOfItems.Add(item);

        JsonController.Serializer(item);
    }
}