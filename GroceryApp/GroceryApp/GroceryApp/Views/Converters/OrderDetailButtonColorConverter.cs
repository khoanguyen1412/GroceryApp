using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace GroceryApp.Views.Converters
{
    public class OrderDetailButtonColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string orderState = (string)value;
            Color color = Color.Black;
            switch (orderState)
            {
                case "WAITING":
                    color = Color.Red;
                    break;
                case "DELIVERING":
                    color = Color.FromHex("#00cc00");
                    break;
                default:
                    color = Color.Black;
                    break;
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
