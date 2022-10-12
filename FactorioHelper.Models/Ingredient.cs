using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FactorioHelper.Models;

public class Ingredient : IEquatable<Ingredient>
{
    public long MainItemId { get; set; }

    public long ItemId { get; set; }
    public Item Item { get; set; }

    public int AmountNeeded { get; set; }

    public double TimeToCraftMainItem { get; set; }

    public double AmountNeededPerSec { get; set; }

    


    public override bool Equals(object obj) => this.Equals(obj as Ingredient);

    public Ingredient() { }
    public Ingredient(int amountNeeded, Item item, double timeToCraftMainItem)
    {
        AmountNeeded = amountNeeded;
        Item = item;
        ItemId = item.ItemId;
        TimeToCraftMainItem = timeToCraftMainItem;
        AmountNeededPerSec = Math.Round(AmountNeeded / TimeToCraftMainItem, 3, MidpointRounding.AwayFromZero);
    }



    public bool Equals(Ingredient other)
    {
        if (other == null) return false;
        if (Object.ReferenceEquals(this, other)) return true;
        if (this.GetType() != other.GetType()) return false;

        return (this.AmountNeeded == other.AmountNeeded) && (this.TimeToCraftMainItem == other.TimeToCraftMainItem) && (this.Item == other.Item);
    }

    public override int GetHashCode() => (this.AmountNeeded, this.TimeToCraftMainItem, this.Item).GetHashCode();

    public static bool operator ==(Ingredient left, Ingredient right)
    {
        if (left is null)
        {
            if (right is null) return true;
            return false;

        }
        return left.Equals(right);
    }

    public static bool operator !=(Ingredient left, Ingredient right) => !(left == right);

}