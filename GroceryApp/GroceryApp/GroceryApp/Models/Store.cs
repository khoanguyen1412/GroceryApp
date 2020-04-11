using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryApp.Models
{
    public class Store
    {
        public int ID { get; set; }
        public string StoreName { get; set; }
        public string ImageURL { get; set; }
        public string StoreDescription { get; set; }
        public string StoreAddress { get; set; }
        public List<Product> Products { get; set; }
        public List<OrderBill> OrderBills { get; set; }
    }
   
}
