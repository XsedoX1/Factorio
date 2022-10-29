using FactorioHelper.Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioHelper.Interfaces.Services
{
    public interface IDBService
    {
        void SaveItem(Item item);

        void RemoveItem(Item item);
    }
}
