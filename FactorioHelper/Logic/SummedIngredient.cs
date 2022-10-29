using FactorioHelper.Models.Models;
using System;
using System.ComponentModel;

namespace FactorioHelper.Logic
{
    public class SummedIngredient : IEquatable<SummedIngredient>, INotifyPropertyChanged
    {
        public Item Item { get; set; }
        private double machinesNeeded;
        private double amountNeededCombinedPerSec;
        public double MachinesNeeded
        {
            get { return machinesNeeded; }
            set
            {
                if (machinesNeeded == value) return;
                machinesNeeded = value;
                NotifyPropertyChanged();
            }
        }

        public double AmountNeededCombinedPerSec
        {
            get { return amountNeededCombinedPerSec; }
            set
            {
                if (amountNeededCombinedPerSec == value) return;
                amountNeededCombinedPerSec = Math.Round(value, 2, MidpointRounding.AwayFromZero);
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SummedIngredient(Item item, double amountNeededCombined)
        {
            Item = item;
            AmountNeededCombinedPerSec = amountNeededCombined;
        }
        public SummedIngredient() { }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as SummedIngredient);
        }

        public bool Equals(SummedIngredient other)
        {
            if (other is null) return false;
            if (Object.ReferenceEquals(this, other)) return true;
            if (this.GetType != other.GetType) return false;

            return (Item == other.Item) && (AmountNeededCombinedPerSec == other.AmountNeededCombinedPerSec);
        }

        public override int GetHashCode()
        {
            return (Item, AmountNeededCombinedPerSec).GetHashCode();
        }

        public static bool operator ==(SummedIngredient left, SummedIngredient right)
        {
            if (left is null)
            {
                if (right is null) return true;
                return false;
            }
            return left.Equals(right);

        }

        private void NotifyPropertyChanged(String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public static bool operator !=(SummedIngredient left, SummedIngredient right) => !(left == right);
    }
}
