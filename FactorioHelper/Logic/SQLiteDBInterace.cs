using FactorioHelper.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioHelper.Logic
{
    public static class SQLiteDBInterace
    {
        private static ObservableCollection<Item> _itemsList = new();
        public static ObservableCollection<Item> ItemsList
        {
            get
            {
                UpdateLoadItemsList();
                return _itemsList;
            }
        }

        public static void UpdateLoadItemsList()
        {
            using(var db = new SQLiteContext())
            {
                _itemsList = new ObservableCollection<Item>(db.Items.Include(it=>it.Ingredients));
            }
          
        }


        public static void SaveItem(Item item)
        {
            
            using (var db = new SQLiteContext())
            {
                if (item.Ingredients.Count > 0)
                {
                    foreach (Ingredient ingredient in item.Ingredients)
                    {
                        db.Items.Attach(ingredient.Item);
                    }
                }
                db.Items.Add(item);
                db.SaveChanges();
                _itemsList.Add(item);
            }

        }

        public static void RemoveItem(Item item)
        {
            using(var db = new SQLiteContext())
            {
                var itemsToRemove =
                    from ig in db.Ingredients
                    where ig.Item != null && ig.ItemId == item.ItemId
                    join it in db.Items on ig.MainItemId equals it.ItemId
                    select it;


                foreach (var itemToRemove in itemsToRemove)
                {
                    RemoveItem(itemToRemove);
                }

                db.Remove(item);
                _itemsList.Remove(item);
                db.SaveChanges();

            }
        }

    }
}
