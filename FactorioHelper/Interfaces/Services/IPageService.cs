using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioHelper.Interfaces.Services
{
    public interface IPageService
    {
        Type GetPageType(string key);
    }
}
