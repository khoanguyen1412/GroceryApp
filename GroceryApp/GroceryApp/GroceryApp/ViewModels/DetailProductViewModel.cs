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
            set { _product = value; OnPropertyChanged(nameof(Product)); }
        }

        private int _quantityInventory;
        public int QuantityInventory
        {
            get { return Product.QuantityInventory; }
            set { _quantityInventory = value; OnPropertyChanged(nameof(QuantityInventory)); }
        }

        public DetailProductViewModel(Product product)
        {
            Product = product;
            LoadData();
        }

        void LoadData()
        {


        }
    }
}
