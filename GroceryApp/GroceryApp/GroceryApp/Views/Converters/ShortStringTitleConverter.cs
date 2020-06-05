using GroceryApp.Data;
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
            if (value == null) return null;
            bool isList = true;
            int leng = 20;
            List<Product> products=null;
            String address = "";
            try
            {
                OrderBill order = (OrderBill)value;
                DataProvider dataProvider = DataProvider.GetInstance();
                products = dataProvider.GetProductsInBillByIDBill(order.IDOrderBill);
            }
            catch (Exception e)
            {
                isList = false;
                leng = 22;
                address = (String)value;
                address = AddressFormatConverter(address);
            }
            
           

            String fullName = "";
            if (isList)
            {
                //foreach (Product product in products)
                    //fullName += product.ProductName + " + ";
                for(int i = 0; i < products.Count - 1; i++)
                {
                    fullName += products[i].ProductName + " + ";
                }
                fullName += products[products.Count - 1].ProductName;
                //fullName.Substring(0, fullName.Length - 2);
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

        public string AddressFormatConverter(string address)
        {
            
            string[] items = address.Split('#');
            string result = "";
            if (checkEmpty(items)) return "";

            bool first = true; //check xem phần tử đầu tiên khác rỗng, để tránh kí tự cách đầu chuỗi
            for (int i = 0; i < items.Length; i++)
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

            foreach (string item in items)
                if (item != "")
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
