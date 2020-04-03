using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryApp.Models
{
    public class OrderBill
    {
        public int ID { get; set; }
        public int IDUser { get; set; }
        public int IDStore { get; set; }
        public string ProductName { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public string CustomerAddress { get; set; }
        public string State { get; set; }
        public string Review { get; set; }
        public string StoreAnswer { get; set; }
        public double Rating { get; set; }
        public double TotalPrice { get; set; }
        public List<Product> OrderedProducts { get; set; }
    }

   











}
