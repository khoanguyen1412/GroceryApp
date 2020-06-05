using GroceryApp.Models;
using GroceryApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using GroceryApp.Data;
using ImTools;

namespace GroceryApp.ViewModels
{
    public class StoreItem
    {
        public string IDStore { get; set; }
        public string Name { get; set; }
        public bool isChosen { get; set; }
    }

    public class ProductItemCart
    {
        public Product Product { get; set; }
        public bool isChosen { get; set; }
    }
    public interface ICartViewModel
    {

    }
    public class CartViewModel : BaseViewModel, ICartViewModel
    {


        private int currentStore;

        private bool _canOrder;
        public bool CanOrder
        {
            get { return _canOrder; }
            set { _canOrder = value; OnPropertyChanged(nameof(CanOrder)); }
        }

        private double _subtotal;
        public double Subtotal {
            get { return _subtotal; }
            set { _subtotal = value; OnPropertyChanged(nameof(Subtotal)); }
        }
        private double _delivery;
        public double Delivery {
            get { return _delivery; }
            set { _delivery = value; OnPropertyChanged(nameof(Delivery)); }
        }
        private double _total;

        public double Total {
            get { return _total; }
            set { _total = value; OnPropertyChanged(nameof(Total)); }
        }

        private ObservableCollection<ProductItemCart> _products;

        public ObservableCollection<ProductItemCart> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged(nameof(Products)); }
        }

        private ObservableCollection<StoreItem> _storeItems=new ObservableCollection<StoreItem>();

        public ObservableCollection<StoreItem> StoreItems
        {
            get { return _storeItems; }
            set { _storeItems = value; OnPropertyChanged(nameof(StoreItems)); }
        }

        public ICommand ShowConfirmInforCommand { get; set; }
        public ICommand ChooseStoreCommand { get; set; }

        public CartViewModel()
        {
            currentStore = 0;
            Subtotal = 0;
            Delivery = 0;
            Total = 0;
            
            LoadStoreItems();
            LoadProducts();
            CanOrder = false;
            ShowConfirmInforCommand = new Command(ShowConfirmInfor);
            ChooseStoreCommand = new Command<string>(ChooseStore);
            
        }



        public void UpdatePayment()
        {
            double subtotal = 0;
            double total = 0;
            double delivery = 0;
            foreach(ProductItemCart item in _products)
                if (item.isChosen)
                {
                    subtotal += item.Product.QuantityOrder * item.Product.Price;
                }

            if (subtotal == 0) delivery = 0;
            else delivery = 10;
            total = delivery + subtotal;

            Total = total;
            Subtotal = subtotal;
            Delivery = delivery;
            if (Total == 0) CanOrder = false;
            else CanOrder = true;
        }

        public void ChooseStore(string storeName)
        {
            var list = _storeItems;
            int choosingIndex = -1;
            for(int i = 0; i < list.Count; i++)
            {
                if (list[i].Name == storeName)
                {
                    choosingIndex = i;
                    list[i].isChosen = true;
                }
                else
                {
                    list[i].isChosen = false;
                }
            }

            if (choosingIndex != currentStore)
            {
                currentStore = choosingIndex;
                LoadProducts();
            }

            StoreItems = new ObservableCollection<StoreItem>(list);
        }


        public async void ShowConfirmInfor()
        {
            List<Product> ListProducts = new List<Product>();
            foreach (ProductItemCart item in _products)
                if (item.isChosen)
                    ListProducts.Add(item.Product);

            OrderBill order = new OrderBill()
            {
                IDOrderBill = "9999",
                IDUser = Infor.IDUser,
                IDStore = StoreItems[currentStore].IDStore,
                Date = DateTime.Today,
                SubTotalPrice = Subtotal,
                DeliveryPrice = Delivery,
                TotalPrice = Total,
                CustomerAddress = "",
                Note = "",
                State = OrderState.Waiting,
                Review = "",
                StoreAnswer = "",
                Rating = -1,
                //OrderedProducts = ListProducts
            };

            OrderBillItem orderBillItem = new OrderBillItem
            {
                Order = order,
                AddedProducts = ListProducts
            };

            var addressPopup = new ConfirmInforOrderPopupView();
            var PopupVM = new ConfirmInforOrderPopupViewModel(orderBillItem);
            addressPopup.BindingContext = PopupVM;
            await PopupNavigation.Instance.PushAsync(addressPopup);
        }

        public void LoadProducts()
        {
            var dataProvider = DataProvider.GetInstance();
            List<Product> products = dataProvider.GetProductInCartByIDStore(StoreItems[currentStore].IDStore);
            List<ProductItemCart> items = new List<ProductItemCart>();
            foreach(Product product in products)
            {
                ProductItemCart item = new ProductItemCart
                {
                    Product = product,
                    isChosen = false
                };
                items.Add(item);
            }
            
            Products = new ObservableCollection<ProductItemCart>(items);
            
        }

        public void LoadStoreItems()
        {
            var dataProvider = DataProvider.GetInstance();
            List<string> ids = dataProvider.GetIDStoreFromAddedProduct();
            List<StoreItem> storeItems = new List<StoreItem>();

            foreach (string id in ids)
            {
                string name = dataProvider.GetStoreNameFromIDStore(id);
                StoreItem storeItem = new StoreItem {IDStore= id,Name = name, isChosen = false };
                storeItems.Add(storeItem);
            }
            storeItems[0].isChosen = true;

            ObservableCollection<StoreItem> result = new ObservableCollection<StoreItem>(storeItems);
            _storeItems = result;
            
        }
    }
}
