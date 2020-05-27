using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace GroceryApp.Views.Converters
{
    public class AddressFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string address = (string)value;
            string[] items = address.Split('#');
            string result = "";
            if (checkEmpty(items)) return "";

            bool first = true; //check xem phần tử đầu tiên khác rỗng, để tránh kí tự cách đầu chuỗi
            for(int i = 0; i < items.Length; i++)
            {
                if (items[i] != "")
                {
                    if (first)
                    {
                        first = false;
                        result += items[i] + ",";
                        continue;
                    }
                    result += " " + items[i] + ",";
                }
                    
            }

            return result.Substring(0, result.Length - 1);

        }

        public bool checkEmpty(string[] items)
        {
            
            foreach(string item in items)
                if(item!="")
                {
                    return false;
                }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
