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
using System.Net.Http;
using GroceryApp.Services;
using GroceryApp.Views.TabBars;

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
        DataProvider dataProvider = DataProvider.GetInstance();
        bool loadDone = false;
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
            set { _products = value;
                OnPropertyChanged(nameof(Products));
                //if (loadDone) UpdatePayment();
            }
        }

        private ObservableCollection<StoreItem> _storeItems=new ObservableCollection<StoreItem>();

        public ObservableCollection<StoreItem> StoreItems
        {
            get { return _storeItems; }
            set { 

                _storeItems = value;
                OnPropertyChanged(nameof(StoreItems)); 
            }
        }

        public ICommand ShowConfirmInforCommand { get; set; }
        public ICommand ChooseStoreCommand { get; set; }
        public ICommand ReturnProductCommand { get; set; }
        public ICommand GoHomeCommand { get; set; }

        public CartViewModel()
        {
            LoadData();
            
            
        }

        public void LoadData()
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
            ReturnProductCommand = new Command<ProductItemCart>(ReturnProduct);
            GoHomeCommand = new Command(GoHome);
            loadDone = true;
        }

        public void GoHome()
        {
            var tabbar = TabBarCustomer.GetInstance();
            tabbar.CurrentPage = tabbar.Children[0];
        }

        public async void ReturnProduct(ProductItemCart productItem)
        {
            Product product = productItem.Product;
            var httpClient = new HttpClient();
            Product deletedProduct = dataProvider.GetProductInCartByIDSourceProduct(product.IDSourceProduct);
            DataUpdater.ReturnProductToSourceProduct(deletedProduct);
            

            Product changedProduct = dataProvider.GetProductByID(product.IDSourceProduct);
            //Update lại product cho server database
            await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "product/update", changedProduct);
            //xóa product bị hủy trong cart 
            await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "product/deleteproductbyid/"+ deletedProduct.IDProduct,new { });


            //load lại data product cho store được trả về VÀ TRONG CART
            DataUpdater.DeletedProductInCart(deletedProduct);
            LoadData();


            //PUSH NOTI
            List<Product> productForPushNoti = new List<Product>();
            productForPushNoti.Add(changedProduct);
            productForPushNoti.Add(deletedProduct);
            string datas = PushNotificationService.ConvertDataReturnProductCart(productForPushNoti);
            PushNotificationService.Push(NotiNumber.ReturnProductCart, datas, true);
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
            string id="OrderBill_"+ DateTime.Now.ToString("HHmmss");
            User customer = DataProvider.GetInstance().GetUserByIDUser(Infor.IDUser);
            OrderBill order = new OrderBill()
            {
                IDOrderBill = id,
                IDUser = customer.IDUser,
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
                CustomerPhone= customer.PhoneNumber,
                UserName=customer.UserName
                
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
            if (StoreItems == null || StoreItems.Count == 0)
            {
                Products=new ObservableCollection<ProductItemCart>();
                return;
            }
            var dataProvider = DataProvider.GetInstance();
            List<Product> products = dataProvider.GetProductInCartByIDStore(StoreItems[currentStore].IDStore);
            List<ProductItemCart> items = new List<ProductItemCart>();
            foreach(Product product in products)
            {
                bool isExisted = false;
                foreach (ProductItemCart itemCart in items)
                    if (product.IDSourceProduct == itemCart.Product.IDSourceProduct)
                    {
                        isExisted = true;
                        itemCart.Product.QuantityOrder += product.QuantityOrder;
                        break;
                    }
                if (isExisted == false)
                {
                    ProductItemCart item = new ProductItemCart
                    {
                        Product = product,
                        isChosen = false
                    };
                    items.Add(item);
                }
                
            }

            Products = new ObservableCollection<ProductItemCart>(items);
            
        }

        public void LoadStoreItems()
        {
            var dataProvider = DataProvider.GetInstance();
            List<string> ids = dataProvider.GetIDStoreFromAddedProduct();
            if (ids.Count == 0)
            {
                StoreItems=new ObservableCollection<StoreItem>();
                return;
            }
            List<StoreItem> storeItems = new List<StoreItem>();
            

            foreach (string id in ids)
            {
                string name = dataProvider.GetStoreNameFromIDStore(id);
                StoreItem storeItem = new StoreItem {IDStore= id,Name = name, isChosen = false };
                storeItems.Add(storeItem);
            }
            storeItems[0].isChosen = true;

            StoreItems = new ObservableCollection<StoreItem>(storeItems);
            
            
        }
    }
}
