using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Factorio.Items;
using Newtonsoft.Json;
using Constants = Factorio.Data.Constants;

namespace Factorio.Logic;

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