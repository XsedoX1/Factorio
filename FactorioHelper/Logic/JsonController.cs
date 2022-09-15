using FactorioHelper.Data;
using FactorioHelper.Items;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace FactorioHelper.Logic;

public static class JsonController
{

    public static void SerializeItem(Item item)
    {
        var readItem = JsonConvert.SerializeObject(item);
        if (!Directory.Exists(Settings.Path))
            Directory.CreateDirectory(Settings.Path);

        if (!File.Exists(Settings.Path + item.Id + ".json"))
            File.WriteAllText(Settings.Path + item.Id + ".json", readItem);
    }

    public static ObservableCollection<Item> DeserializeItems()
    {
        var listOfItems = new ObservableCollection<Item>();

        if (!Directory.Exists(Settings.Path))
            Directory.CreateDirectory(Settings.Path);

        var files = Directory.GetFiles(Settings.Path);

        foreach (var path in files)
        {
            FileInfo fi = new FileInfo(path);
            if (fi.Extension != ".json")
                continue;

            var serializedItem = File.ReadAllText(path);
            var deserializedItem = JsonConvert.DeserializeObject<Item>(serializedItem);
            if (deserializedItem is not null) listOfItems.Add(deserializedItem);
        }

        return listOfItems;
    }
}