using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryApp.Models
{
    public class ProductItem
    {
        public Product Product { get; set; }
        public bool isHidden { get; set; }
    }
}
