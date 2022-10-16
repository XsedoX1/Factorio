using System.Collections.ObjectModel;

namespace FactorioHelper.Models
{
    public class Item : IEquatable<Item>
    {
        public long ItemId { get; set; }

        public double TimeToCraft { get; set; }

        public override bool Equals(object? obj) => Equals(obj as Item);

        public override int GetHashCode() => (ItemId, TimeToCraft, AmountCrafted).GetHashCode();


        public string? Name { get; set; }


        public int AmountCrafted { get; set; }

        public int IsAssemblingMachine { get; set; }

        
        public ObservableCollection<Ingredient>? Ingredients { get; set; } = new();


        public double AmountPerSec { get; set; }

        
        public Item() { }
        public Item(string name, double timeToCraft, int amountCrafted, int isAssemblingMachine, ObservableCollection<Ingredient>? ingredients = null)
        {
            TimeToCraft = timeToCraft;
            AmountCrafted = amountCrafted;
            Ingredients = ingredients;
            Name = name;
            AmountPerSec = Math.Round(amountCrafted / timeToCraft, 3, MidpointRounding.AwayFromZero);
            IsAssemblingMachine = isAssemblingMachine;
        }

        public bool Equals(Item? other)
        {
            if (other == null) return false;
            if (Object.ReferenceEquals(this, other)) return true;
            if (this.GetType() != other.GetType()) return false;

            return (ItemId == other.ItemId) && (AmountCrafted == other.AmountCrafted) && (TimeToCraft == other.TimeToCraft);
        }

        public static bool operator ==(Item? left, Item? right)
        {
            if (left is null)
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