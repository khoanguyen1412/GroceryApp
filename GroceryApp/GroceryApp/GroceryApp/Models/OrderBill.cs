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
        public string CustomerPhone { get; set; }
        public string State { get; set; }
        public string Review { get; set; }
        public string StoreAnswer { get; set; }
        public int Rating { get; set; }
        public double SubTotalPrice { get; set; }
        public double DeliveryPrice { get; set; }
        public double TotalPrice { get; set; }
        public List<Product> OrderedProducts { get; set; }
        public int OrderNumber { get; set; }

        //Phụ
        public string UserName { get; set; }

        public void Init()
        {
            this.DeliveryPrice = 10;
            this.SubTotalPrice = 0;
            foreach (Product product in this.OrderedProducts)
                SubTotalPrice += product.QuantityOrder * product.Price;
            this.TotalPrice = SubTotalPrice + DeliveryPrice;

            foreach (User user in GroceryApp.Data.Database.Users)
                if (user.IDUser == this.IDUser)
                {
                    this.UserName = user.UserName;
                    break;
                }
        }
        public double GetTotal()
        {
            double total = 10;
            foreach (Product product in this.OrderedProducts)
                total += product.Price * product.QuantityOrder;

            return total;
        }

        
    }

   











}
