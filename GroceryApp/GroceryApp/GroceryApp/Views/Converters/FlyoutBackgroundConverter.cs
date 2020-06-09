using GroceryApp.Views.Drawer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace GroceryApp.Views.Converters
{
    public class FlyoutBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            try
            {
                string title = (string)value;
                if (title == "Logout") return "";
                return "goarrow";
            }
            catch(Exception e)
            {

            }
            //string itemName = ((ShellContent)value).Title;
            //string currentFlytoutName = AppDrawer.GetInstance().CurrentItem.Title;
            //if(itemName == currentFlytoutName) return Color.FromHex("#73D8D4");
            Color background = (Color)value;
            Color colorGreen = Color.FromHex("#73D8D4");
            if (colorGreen == background) return false;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
