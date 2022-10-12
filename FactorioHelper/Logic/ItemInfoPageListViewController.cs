using FactorioHelper.Models;
using System.Collections.ObjectModel;

namespace FactorioHelper.Logic
{
    public static class ItemInfoPageListViewController
    {
        public static void FlattenIngredients(Item item, ObservableCollection<SummedIngredient> _ingredients, double multiplier)
        {
            foreach (var ingredient in item.Ingredients)
            {
                bool added = false;
                SummedIngredient toRemove = null;

                if (ingredient is not null)
                {
                    foreach (var addedIngredient in _ingredients)
                    {
                        if (addedIngredient is not null)
                        {
                            if (addedIngredient.Item == ingredient.Item)
                            {
                                SummedIngredient summedIngredient = new SummedIngredient(ingredient.Item, addedIngredient.AmountNeededCombinedPerSec + ingredient.AmountNeededPerSec * multiplier);
                                _ingredients.Add(summedIngredient);
                                toRemove = addedIngredient;
                                added = true;
                                break;
                            }
                        }
                    }
                    if (!added)
                    {
                        SummedIngredient summedIngredient = new SummedIngredient(ingredient.Item, ingredient.AmountNeededPerSec * multiplier);
                        _ingredients.Add(summedIngredient);
                    }
                    else
                        _ingredients.Remove(toRemove);


                    if (ingredient.Item.Ingredients.Count > 0)
                        FlattenIngredients(ingredient.Item, _ingredients, ingredient.AmountNeededPerSec/ingredient.Item.AmountPerSec);
                }
            }

        }
    }
}
