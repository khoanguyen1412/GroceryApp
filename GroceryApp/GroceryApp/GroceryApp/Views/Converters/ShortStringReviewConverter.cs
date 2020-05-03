using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace GroceryApp.Views.Converters
{
    public class ShortStringReviewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int leng = 27;
            String Content = (String)value;

            String shortStr = "";
            if (Content.Length <= leng) shortStr = Content;
            else
            {
                shortStr = Content.Substring(0, leng);
                shortStr += "..";
            }
            return shortStr;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
