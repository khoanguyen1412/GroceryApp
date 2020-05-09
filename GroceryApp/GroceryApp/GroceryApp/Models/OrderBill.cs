using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryApp.Models
{
    public class OrderBill
    {

        public string IDOrderBill { get; set; }
        public string IDUser { get; set; }
        public string IDStore { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public string CustomerAddress { get; set; }
        public string State { get; set; }
        public string Review { get; set; }
        public string StoreAnswer { get; set; }
        public int Rating { get; set; }
        public double SubTotalPrice { get; set; }
        public double DeliveryPrice { get; set; }
        public double TotalPrice { get; set; }
        public List<Product> OrderedProducts { get; set; }
        public int OrderNumber { get; set; }
    }

   











}
