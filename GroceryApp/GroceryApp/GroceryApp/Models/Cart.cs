using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryApp.Models
{
    public class Cart
    {
        public string IDCart { get; set; }
        public List<Product> AddedProducts { get; set; }

    }
}
