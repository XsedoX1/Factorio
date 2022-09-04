using System;
using System.IO;

namespace FactorioHelper.Data;

public static class Constants
{
    public static string PROJECT_DIR =>
        Path.GetFullPath(Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "../../../../../../"));
}