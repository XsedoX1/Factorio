using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FactorioHelper.Data
{
    public static class Settings
    {
        private static Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public static string Path
        {

            get
            {
                return (string)localSettings.Values["itemsPath"];
            }
            set
            {
                if (value == (string)localSettings.Values["itemsPath"]) return;
                localSettings.Values["itemsPath"] = value + "/";
            }
        }

    }
}
