using GroceryApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace GroceryApp.Views.Converters
{
    public class ShortStringTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isList = true;
            int leng = 20;
            List<Product> products=null;
            String address = "";
            try
            {
                products = (List<Product>)value;
            }
            catch (Exception e)
            {
                isList = false;
                leng = 22;
                address = (String)value;
            }
            
           

            String fullName = "";
            if (isList)
            {
                foreach (Product product in products)
                    fullName += product.ProductName + " + ";
            }
            else fullName = address;
            

            String shortName = "";
            if (fullName.Length <= leng) shortName = fullName;
            else
            {
                shortName = fullName.Substring(0, leng);
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
