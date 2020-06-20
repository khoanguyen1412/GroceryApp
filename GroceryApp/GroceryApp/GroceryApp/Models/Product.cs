using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace GroceryApp.Models
{
    public class Product : INotifyPropertyChanged
    {

        
        public string IDProduct { get; set; }
        public string IDType { get; set; }
        public string IDStore { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Unit { get; set; }
        public int QuantityInventory { get; set; }
        public int QuantityOrder { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public ProductStateInStore StateInStore { get; set; }
        public string IDOrderBill { get; set; }
        public string IDCart { get; set; } //ID User who added this product to their cart
        public string IDSourceProduct{get;set;}
        public ProductState State { get; set; }
        public Product(Product source)
        {
            this.IDProduct = source.IDProduct;
            this.IDType = source.IDType;
            this.IDStore = source.IDStore;
            this.ProductName = source.ProductName;
            this.ProductDescription = source.ProductDescription;
            this.Unit = source.Unit;
            this.QuantityInventory = source.QuantityInventory;
            this.QuantityOrder = source.QuantityOrder;
            this.Price = source.Price;
            this.ImageURL = source.ImageURL;
            this.StateInStore = source.StateInStore;
            this.IDOrderBill = source.IDOrderBill;
            this.IDCart = source.IDCart;
            this.IDSourceProduct = source.IDSourceProduct;
            this.State = source.State;

        }

        public void Update(Product source)
        {
            this.IDProduct = source.IDProduct;
            this.IDType = source.IDType;
            this.IDStore = source.IDStore;
            this.ProductName = source.ProductName;
            this.ProductDescription = source.ProductDescription;
            this.Unit = source.Unit;
            this.QuantityInventory = source.QuantityInventory;
            this.QuantityOrder = source.QuantityOrder;
            this.Price = source.Price;
            this.ImageURL = source.ImageURL;
            this.StateInStore = source.StateInStore;
            this.IDOrderBill = source.IDOrderBill;
            this.IDCart = source.IDCart;
            this.IDSourceProduct = source.IDSourceProduct;
            this.State = source.State;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Command DecreaseCommand
        {
            get
            {
                return new Command(val =>
                {
                    QuantityOrder = (Int16.Parse(val.ToString()) - 1);
                    if (QuantityOrder < 0) QuantityOrder = 0;
                    OnPropertyChanged("QuantityOrder");


                });
            }
        }

        public Command IncreaseCommand
        {
            get
            {
                return new Command(val =>
                {
                    QuantityOrder = (Int16.Parse(val.ToString()) + 1);
                    if (QuantityOrder > QuantityInventory) QuantityOrder = QuantityInventory;
                    OnPropertyChanged("QuantityOrder");


                });
            }
        }

        public Command DecreaseInventoryCommand
        {
            get
            {
                return new Command(val =>
                {
                    QuantityInventory = (Int16.Parse(val.ToString()) - 1);
                    if (QuantityInventory < 0) QuantityInventory = 0;
                    OnPropertyChanged("QuantityInventory");
                });
            }
        }

        public Command IncreaseInventoryCommand
        {
            get
            {
                return new Command(val =>
                {
                    QuantityInventory = (Int16.Parse(val.ToString()) + 1);
                    OnPropertyChanged("QuantityInventory");
                });
            }
        }

        public void SetQuantityInventory(int newAmount)
        {
            QuantityInventory = newAmount;
            OnPropertyChanged("QuantityInventory");
        }

        public void SetQuantityOrder(int newAmount)
        {
            QuantityOrder = newAmount;
            OnPropertyChanged("QuantityOrder");
        }

        public Product() { }
        
    }
}
