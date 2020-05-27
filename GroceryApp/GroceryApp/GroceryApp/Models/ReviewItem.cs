using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryApp.Models
{
    public class ReviewItem
    {
        public string IDOrderBill { get; set; }
        public string CustomerImage { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public string StoreAnswer { get; set; }
        public int Rating { get; set; }
        public string star1 { get; set; }
        public string star2 { get; set; }
        public string star3 { get; set; }
        public string star4 { get; set; }
        public string star5 { get; set; }

        public void SetStar()
        {
            switch (this.Rating)
            {
                case 1:
                    {
                        this.star1 = "fullstar";
                        this.star2 = "emptystar";
                        this.star3 = "emptystar";
                        this.star4 = "emptystar";
                        this.star5 = "emptystar";
                        break;
                    }
                case 2:
                    {
                        this.star1 = "fullstar";
                        this.star2 = "fullstar";
                        this.star3 = "emptystar";
                        this.star4 = "emptystar";
                        this.star5 = "emptystar";
                        break;
                    }
                case 3:
                    {
                        this.star1 = "fullstar";
                        this.star2 = "fullstar";
                        this.star3 = "fullstar";
                        this.star4 = "emptystar";
                        this.star5 = "emptystar";
                        break;
                    }
                case 4:
                    {
                        this.star1 = "fullstar";
                        this.star2 = "fullstar";
                        this.star3 = "fullstar";
                        this.star4 = "fullstar";
                        this.star5 = "emptystar";
                        break;
                    }
                case 5:
                    {
                        this.star1 = "fullstar";
                        this.star2 = "fullstar";
                        this.star3 = "fullstar";
                        this.star4 = "fullstar";
                        this.star5 = "fullstar";
                        break;
                    }
                default:
                    {
                        this.star1 = "emptystar";
                        this.star2 = "emptystar";
                        this.star3 = "emptystar";
                        this.star4 = "emptystar";
                        this.star5 = "emptystar";
                        break;
                    }
            }
        }
    }
}
