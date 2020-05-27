using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace GroceryApp.Views.Converters
{
    public class TextColorEnableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool enable = (Boolean)value;
            if (enable) return Color.FromHex("#000000"); ;
            return Color.FromHex("#d9d9d9"); ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
