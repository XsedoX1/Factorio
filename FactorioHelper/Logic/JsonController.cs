﻿using FactorioHelper.Items;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using Constants = FactorioHelper.Data.Constants;

namespace FactorioHelper.Logic;

public static class JsonController
{

    public static void Serializer(Item item)
    {
        var readItem = JsonConvert.SerializeObject(item);

        if (!File.Exists(Constants.PROJECT_DIR + "Data/ItemsDB/" + item.Id + ".json"))
            File.WriteAllText(Constants.PROJECT_DIR + "Data/ItemsDB/" + item.Id + ".json", readItem);
    }

    public static ObservableCollection<Item> Deserializer()
    {
        var listOfItems = new ObservableCollection<Item>();
        var files = Directory.GetFiles(Constants.PROJECT_DIR + "Data/ItemsDB/");
        foreach (var path in files)
        {
            var serializedItem = File.ReadAllText(path);
            var deserializedItem = JsonConvert.DeserializeObject<Item>(serializedItem);
            if (deserializedItem != null) listOfItems.Add(deserializedItem);
        }

        return listOfItems;
    }
}