using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryApp.Models
{
    public class OrderBillItem
    {
        public OrderBill Order { get; set; }
        public List<Product> AddedProducts { get; set; }
    }
}
