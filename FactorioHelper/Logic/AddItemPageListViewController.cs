using FactorioHelper.Models.Models;
using System.Collections.ObjectModel;

namespace FactorioHelper.Logic
{

    public static class AddItemPageListViewController
    {
        public static ObservableCollection<Item> ListOfAvailableItems { set; get; } = new ObservableCollection<Item>();
        public static ObservableCollection<Ingredient> ListOfIngredients { set; get; } = new ObservableCollection<Ingredient>();


        public static void Add_Button(Item item, int amountNeeded, double timeNeededForMainItem)
        {
           
            var ingredient = new Ingredient(amountNeeded, item, timeNeededForMainItem);
            if (ingredient != null)
            {
                ListOfIngredients.Add(ingredient);
                ListOfAvailableItems.Remove(ingredient.Item);
            }


        }

        public static void Remove_Ingredient_Button(Ingredient ingredient)
        {
            if (ListOfIngredients.Contains(ingredient))
            {
                ListOfIngredients.Remove(ingredient);
                ListOfAvailableItems.Add(ingredient.Item);
            }

        }

    }
}
