using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace GroceryApp.Views.Converters
{
    public class StoreRatingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double rate = (double)value;
            switch (rate)
            {
                case 0:
                    return "star00";
                case 0.5f:
                    return "star05";
                case 1.0f:
                    return "star10";
                case 1.5f:
                    return "star15";
                case 2.0f:
                    return "star20";
                case 2.5f:
                    return "star25";
                case 3.0f:
                    return "star30";
                case 3.5f:
                    return "star35";
                case 4.0f:
                    return "star40";
                case 4.5f:
                    return "star45";
                case 5.0f:
                    return "star50";
            }
            return "Star00";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
