using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace GroceryApp.Views.Converters
{
    public class OrderDetailButtonTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string orderState = (string)value;
            string text = "";
            switch (orderState)
            {
                case "WAITING":
                    text = "CANCEL ORDER";
                    break;
                case "DELIVERING":
                    text = "RECEIVE ORDER";
                    break;
                default:
                    text = "";
                    break;
            }

            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
