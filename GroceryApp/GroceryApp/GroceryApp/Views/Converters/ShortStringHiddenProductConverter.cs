using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace GroceryApp.Views.Converters
{
    public class ShortStringHiddenProductConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string InputString = "";
            int leng = 12;

            try
            {
                InputString= (String)value;
            }
            catch (Exception e)
            {
                double temp1 = (Double)value;
                InputString = temp1.ToString();
            }
            String shortName = "";
            if (InputString.Length <= leng) shortName = InputString;
            else
            {
                shortName = InputString.Substring(0, leng);
                shortName += "..";
            }
            return shortName;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
