using FactorioHelper.EFCore.DataAccess;
using FactorioHelper.Interfaces.Services;
using FactorioHelper.Models;
using FactorioHelper.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FactorioHelper.Services
{
    public class DBService : IDBService
    {
        private SQLiteContext _dbContext;
        
        public ObservableCollection<Item> ItemsList
        {
            get
            {
                using (_dbContext)
                {
                    return new ObservableCollection<Item>(_dbContext.Items);
                }
            }
        }

        public DBService()
        {
            _dbContext = App.GetService<SQLiteContext>();
        }


        public void SaveItem(Item item)
        {

            using (_dbContext)
            {
                if (item.Ingredients.Count > 0)
                {
                    foreach (Ingredient ingredient in item.Ingredients)
                    {
                        _dbContext.Items.Attach(ingredient.Item);
                    }
                }
                _dbContext.Items.Add(item);
                _dbContext.SaveChanges();
            }

        }

        public void RemoveItem(Item item)
        {
            using (_dbContext)
            {
                var itemsToRemove =
                    from ig in _dbContext.Ingredients
                    where ig.Item != null && ig.ItemId == item.ItemId
                    join it in _dbContext.Items on ig.MainItemId equals it.ItemId
                    select it;


                foreach (var itemToRemove in itemsToRemove)
                {
                    RemoveItem(itemToRemove);
                }

                _dbContext.Remove(item);
                _dbContext.SaveChanges();

            }
        }

    }
}
