﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryApp.Models
{
  
    public class ProductType
    {
        public int ID { get; set; }
        public string TypeName { get; set; }
        public string ImageURL { get; set; }
        public string TypeDescription { get; set; }
    }
}
