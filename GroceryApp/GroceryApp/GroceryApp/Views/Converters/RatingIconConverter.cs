using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace GroceryApp.Views.Converters
{
    public class RatingIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double rate = (double)value;
            if (rate >= 4.5f) return "happy";
            if (rate >= 2.5f) return "normal";
            return "sad";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
