using GroceryApp.Data;
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
        public double SubTotalPrice { get; set; }
        public double DeliveryPrice { get; set; }
        public double TotalPrice { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string Note { get; set; }
        public OrderState State { get; set; }
        public string Review { get; set; }
        public string StoreAnswer { get; set; }
        public int Rating { get; set; }


        //Phụ
        public int OrderNumber { get; set; }
        public string UserName { get; set; }

        public void Update(OrderBill updatedOrder)
        {
            this.CustomerAddress = updatedOrder.CustomerAddress;
            this.CustomerPhone = updatedOrder.CustomerPhone;
            this.Note = updatedOrder.Note;
            this.State = updatedOrder.State;
            this.Review = updatedOrder.Review;
            this.StoreAnswer = updatedOrder.StoreAnswer;
            this.Rating = updatedOrder.Rating;
            this.StoreAnswer = updatedOrder.StoreAnswer;
        }

        DataProvider dataProvider = DataProvider.GetInstance();

        public void Init()
        {
            List<Product> OrderedProducts = dataProvider.GetProductsInBillByIDBill(this.IDOrderBill);
            this.DeliveryPrice = 10;
            this.SubTotalPrice = 0;
            foreach (Product product in OrderedProducts)
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
            List<Product> OrderedProducts = dataProvider.GetProductsInBillByIDBill(this.IDOrderBill);
            double total = 10;
            foreach (Product product in OrderedProducts)
                total += product.Price * product.QuantityOrder;

            return total;
        }

        
    }

   











}
