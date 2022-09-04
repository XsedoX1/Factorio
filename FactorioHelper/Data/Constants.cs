using System;
using System.IO;


using Windows.Storage;
namespace FactorioHelper.Data;

public static class Constants
{
    public static string PROJECT_DIR
    {

        get
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            return localFolder.Path + "/";
        }
    }

}