﻿using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioHelper.Helpers
{
    public static class FrameExtensions
    {
        public static object? GetPageViewModel(this Frame frame)=> frame?.Content?.GetType().GetProperty("ViewModel")?.GetValue(frame.Content, null);
    }
}
