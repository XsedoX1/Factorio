using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace FactorioHelper.Items
{
    public class Item : IEquatable<Item>
    {
        public double TimeToCraft { get; private set; }

        public override bool Equals(object obj) => Equals(obj as Item);
        public override int GetHashCode() => (Id, TimeToCraft, AmountCrafted).GetHashCode();

        public string Name { get; }

        public int AmountCrafted { get; }

        public ObservableCollection<Ingredient> Ingredients { get; }

        public double AmountPerSec { get; }

        public string Id { get; private set; }

        public Item(string name, double timeToCraft, int amountCrafted, ObservableCollection<Ingredient> ingredients = null)
        {
            TimeToCraft = timeToCraft;
            AmountCrafted = amountCrafted;
            Ingredients = ingredients;
            Name = name;
            AmountPerSec = Math.Round(amountCrafted / timeToCraft, 3, MidpointRounding.AwayFromZero);
            Id = Regex.Replace(name, @"\s+", "_");
        }

        public bool Equals(Item other)
        {
            if (other == null) return false;
            if(Object.ReferenceEquals(this, other)) return true;
            if(this.GetType() != other.GetType()) return false;

            return (Id == other.Id) && (AmountCrafted == other.AmountCrafted) && (TimeToCraft==other.TimeToCraft);
        }

        public static bool operator ==(Item left, Item right)
        {
            if(left is null)
            {
                if (right is null)
                    return true;

                return false;
            }
            return left.Equals(right);

        }

        public static bool operator !=(Item left, Item right) => !(left == right);
        
    }
}