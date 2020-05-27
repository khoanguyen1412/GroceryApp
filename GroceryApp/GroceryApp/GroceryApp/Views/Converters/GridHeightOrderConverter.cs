using GroceryApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace GroceryApp.Views.Converters
{
    public class GridHeightOrderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<OrderBill> orders;
            ObservableCollection<ReviewItem> reviews;

            try
            {
                orders = (ObservableCollection<OrderBill>)value;
                return orders.Count * 100;
            }
            catch(Exception e)
            {
                reviews = (ObservableCollection<ReviewItem>)value;
                return reviews.Count * 110;
            }

            
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
