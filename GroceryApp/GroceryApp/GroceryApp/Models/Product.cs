using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryApp.Models
{
    public class Product
    {
        public int ID { get; set; }
        public int IDType { get; set; }
        public int IDStore { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Unit { get; set; }
        public int QuantityInventory { get; set; }
        public int QuantityOrder { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
    }
}
