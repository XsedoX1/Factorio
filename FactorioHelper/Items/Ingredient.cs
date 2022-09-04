using Newtonsoft.Json;
using System;

namespace FactorioHelper.Items;

public class Ingredient : IEquatable<Ingredient>
{
    public int AmountNeeded { get; }

    public double TimeToCraftMainItem { get; private set; }

    public double AmountNeededPerSecond { get; }

    public Item Item { get; }


    public override bool Equals(object obj) => this.Equals(obj as Ingredient);

    [JsonConstructor]
    public Ingredient(int amountNeeded, Item item, double timeToCraftMainItem)
    {
        AmountNeeded = amountNeeded;
        Item = item;
        TimeToCraftMainItem = timeToCraftMainItem;
        AmountNeededPerSecond = Math.Round(AmountNeeded / TimeToCraftMainItem, 3, MidpointRounding.AwayFromZero);
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