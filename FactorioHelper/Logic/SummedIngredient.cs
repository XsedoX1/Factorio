using FactorioHelper.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioHelper.Logic
{
    public class SummedIngredient : IEquatable<SummedIngredient>, INotifyPropertyChanged
    {
        public Item Item { get; set; }
        private double machinesNeeded;
        private double itemNeededPerSec;
        public double AmountNeededCombined { set; get; }
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

        public double ItemNeededPerSec
        {
            get { return itemNeededPerSec; }
            set
            {
                if (itemNeededPerSec == value) return;
                itemNeededPerSec = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SummedIngredient(Item item, double amountNeededCombined)
        {
            Item = item;
            AmountNeededCombined = amountNeededCombined;
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

            return (Item == other.Item) && (AmountNeededCombined == other.AmountNeededCombined);
        }

        public override int GetHashCode()
        {
            return (Item, AmountNeededCombined).GetHashCode();
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

        private void NotifyPropertyChanged(String propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        

        public static bool operator !=(SummedIngredient left, SummedIngredient right) => !(left == right);
    }
}
