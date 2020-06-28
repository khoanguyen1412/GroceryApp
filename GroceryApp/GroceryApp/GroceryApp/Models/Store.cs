using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryApp.Models
{
    public class Store
    {
        public string IDStore { get; set; }
        public string StoreName { get; set; }
        public string ImageURL { get; set; }
        public string StoreDescription { get; set; }
        public string StoreAddress { get; set; }
        public double RatingStore { get; set; }
        public int IsActive { get; set; }
    }
   
}
