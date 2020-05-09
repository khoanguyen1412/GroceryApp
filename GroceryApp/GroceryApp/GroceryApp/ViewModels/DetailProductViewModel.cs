using GroceryApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryApp.ViewModels
{
    public interface IDetailProductViewModel
    {

    }
    public class DetailProductViewModel: BaseViewModel, IDetailProductViewModel
    {
        private Product _product;
        public Product Product
        {
            get { return _product; }
        }

        public DetailProductViewModel()
        {
            LoadData();
        }

        void LoadData()
        {

            //DATA KHÔNG CÒN SỬ DỤNG, DO PRODUCT ĐÃ ĐƯỢC GỬI TỪ BINDING CONTEXT Ở SCREEN TRƯỚC
            Product product = new Product
            {
                IDProduct = "0",
                IDType = "0",
                IDStore = "0",
                ProductName = "Carrot",
                ProductDescription = "Fresh and Big Carrots from US Farms",
                Unit = "one",
                QuantityInventory = 15,
                QuantityOrder = 4,
                Price = 2000,
                ImageURL = "https://www.jessicagavin.com/wp-content/uploads/2019/02/carrots-7-1200.jpg",
            };
            
            //this._product = product;

        }
    }
}
