using FFImageLoading.Forms.Args;
using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Views.Screens;
using GroceryApp.Views.TabBars;
using Plugin.SharedTransitions;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{

    public interface IShowStoreViewModel
    {

    }

    public class FeedBack
    {
        public string CustomerReview { get; set; }
        public string StoreAnswer { get; set; }
    }

    

    

    public class ShowStoreViewModel : BaseViewModel, IShowStoreViewModel
    {

        DataProvider dataProvider = DataProvider.GetInstance();
        public ShowStoreViewModel()
        {
            //KHÔNG ĐƯỢC DÙNG HÀM KHỞI TẠO NÀY
            //LoadData();
            //ShowDetailProductCommand = new Command<Product>(ShowDetailProduct);
        }
        public int currentType = -1;
        public string IDStore { get; set; }

        private ObservableCollection<ProductItem> _products;
        private ObservableCollection<ProductItem> _saveProducts;

        private ObservableCollection<OrderBill> _orderedBills;

        private ObservableCollection<TypeItem> _typeItems;

        public ObservableCollection<ProductItem> Products
        {
            get {
                return _products;
            }
            set { _products = value; OnPropertyChanged(nameof(Products)); }
        }

        public ObservableCollection<TypeItem> TypeItems
        {
            get { return _typeItems; }
            set { _typeItems = value; OnPropertyChanged(nameof(TypeItems)); }
        }

        public ObservableCollection<OrderBill> OrderBills
        {
            get { return _orderedBills; }
            set { _orderedBills = value; OnPropertyChanged(nameof(OrderBills)); }
        }

        public ObservableCollection<FeedBack> FeedBacks
        {
            get
            {
                ObservableCollection<FeedBack> feedBack = new ObservableCollection<FeedBack>();
                foreach (OrderBill order in _orderedBills)
                    if(order.State== OrderState.Received)
                    {
                        FeedBack newFeedBack = new FeedBack();
                        newFeedBack.CustomerReview = order.Review;
                        newFeedBack.StoreAnswer = order.StoreAnswer;
                        feedBack.Add(newFeedBack);
                    }
                return feedBack;
            }
        }

        private string _selectedProductId;
        public string SelectedProductId
        {
            get { return _selectedProductId; }
            set { _selectedProductId = value; OnPropertyChanged(nameof(SelectedProductId)); }
        }

        private ObservableCollection<Product> _selectedProducts;
        public ObservableCollection<Product> SelectedProducts
        {
            get
            {
                return _selectedProducts;
            }
            set { _selectedProducts = value; OnPropertyChanged(nameof(SelectedProducts)); }
        }
        public string StoreName
        {
            get {
                var dataProvider = DataProvider.GetInstance();
                return dataProvider.GetStoreByIDStore(IDStore).StoreName; 
            }
        }
        public string ImageURL
        {
            get
            {
                var dataProvider = DataProvider.GetInstance();
                return dataProvider.GetStoreByIDStore(IDStore).ImageURL;
            }
        }
        public string StoreDescription
        {
            get
            {
                var dataProvider = DataProvider.GetInstance();
                return dataProvider.GetStoreByIDStore(IDStore).StoreDescription;
            }
        }
        public string StoreAddress
        {
            get
            {
                var dataProvider = DataProvider.GetInstance();
                return dataProvider.GetStoreByIDStore(IDStore).StoreAddress;
            }
        }

        public ICommand ShowDetailProductCommand { get; set; }
        public ICommand ChooseCommand { get; set; }
        
        public void updateSelectedProduct()
        {
            List<Product> selectedProducts = new List<Product>();
            foreach (ProductItem item in _saveProducts)
                if (item.Product.QuantityOrder > 0)
                    selectedProducts.Add(item.Product);
            SelectedProducts = new ObservableCollection<Product>(selectedProducts);
        }

        


        public ShowStoreViewModel(string IDStore)
        {
            this.IDStore = IDStore;
            LoadData();
            ShowDetailProductCommand = new Command<ProductItem>(ShowDetailProduct);
            ChooseCommand = new Command<TypeItem>(Choose);
        }

        public async void AddToCart()
        {
            List<Product> changedProducts = new List<Product>();
            //clone +insert
            List<Product> cloneProducts = new List<Product>();
            for (int i = 0; i < _saveProducts.Count; i++)
                if(_saveProducts[i].Product.QuantityOrder > 0)
                {
                    _saveProducts[i].Product.SetQuantityInventory(_saveProducts[i].Product.QuantityInventory - _saveProducts[i].Product.QuantityOrder);
                    Product cloneProduct = new Product(_saveProducts[i].Product);
                    _saveProducts[i].Product.SetQuantityOrder(0);
                    changedProducts.Add(_saveProducts[i].Product);
                    cloneProduct.IDSourceProduct = _saveProducts[i].Product.IDProduct;
                    cloneProduct.IDCart = Infor.IDUser;
                    cloneProduct.State = ProductState.InCart;
                    cloneProducts.Add(cloneProduct);
                }

            //genid for cloneproducts
            DateTime date = DateTime.Now;
            for (int i = 0; i < cloneProducts.Count; i++)
            {
                string id = "Product_";
                id+=date.ToString("HHmmss");
                id += i.ToString();
                cloneProducts[i].IDProduct = id;
            }
            //update data product 
            DataUpdater.UpdateProduct(changedProducts);
            List<Product> ExistProducts = new List<Product>();
            List<Product> NewProducts = new List<Product>();
            
            foreach(Product product in cloneProducts)
            {
                if (dataProvider.CheckExistInMyCart(product))
                    ExistProducts.Add(product);
                else NewProducts.Add(product);
            }
            DataUpdater.UpdateExistProductIncart(ExistProducts);
            DataUpdater.InsertProduct(NewProducts);

            ExistProducts = dataProvider.GetExistProducInCarttByListProduct(ExistProducts);

            //call api update server database
            foreach (Product product in changedProducts)
            {
                var httpClient = new HttpClient();
                await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "product/update", product);
            }

            foreach (Product product in ExistProducts)
            {
                var httpClient = new HttpClient();
                await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "product/update", product);
            }

            //call api insert server database
            foreach (Product product in NewProducts)
            {
                var httpClient = new HttpClient();
                await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "product/insert", product);
            }

            //update UI CART
            var cartVM = TabBarCustomer.GetInstance().Children[2].BindingContext as CartViewModel;
            cartVM.InitCart();
        }

        
        public void Choose(TypeItem typeItem)
        {
            int choosingIndex = -1;
            for(int i=0;i< _typeItems.Count; i++)
            {
                if (_typeItems[i].productType.IDType == typeItem.productType.IDType)
                {
                    choosingIndex = i;
                    _typeItems[i].isChosen = !_typeItems[i].isChosen;
                }
                else
                {
                    _typeItems[i].isChosen = false;
                }

            }
            if (choosingIndex != currentType)
            {
                currentType = choosingIndex;
                LoadProducts();
            }
            if (_typeItems[choosingIndex].isChosen == false)
            {
                currentType = -1;
                LoadProducts();
            }

            TypeItems = new ObservableCollection<TypeItem>(_typeItems);

        }

        public async void ShowDetailProduct(ProductItem item)
        {
            SelectedProductId = item.Product.IDProduct;
            var detailPage = new DetailProductView();
            detailPage.BindingContext = item.Product;
            await App.Current.MainPage.Navigation.PushAsync(detailPage, true);
        }

        public void LoadData()
        {
            LoadProducts();
            LoadOrderedBills();
            LoadProductTypes();
        }

        public void LoadProducts()
        {
            if(_saveProducts == null)
            {
                List<ProductItem> productItems = new List<ProductItem>();
                var dataProvider = DataProvider.GetInstance();
                List<Product> products = dataProvider.GetProductByIDStore(IDStore);
                foreach (Product product in products)
                {
                    product.QuantityOrder = 0;
                    ProductItem item = new ProductItem
                    {
                        Product = product,
                        isHidden = false
                    };
                    productItems.Add(item);
                }

                _saveProducts= new ObservableCollection<ProductItem>(productItems);
                List<ProductItem> items = new List<ProductItem>();
                foreach (ProductItem item in _saveProducts)
                    if (!item.isHidden)
                        items.Add(item);
                Products = new ObservableCollection<ProductItem>(items);
            }
            else
            {
                List<ProductItem> filteredProducts = new List<ProductItem>();
                if (currentType == -1)
                {
                    foreach (ProductItem item in _saveProducts)
                    {
                        item.isHidden = false;
                        filteredProducts.Add(item);
                    }
                }
                else
                {
                    foreach(ProductItem item in _saveProducts)
                    {
                        if (item.Product.IDType == (currentType + 1).ToString())
                        {
                            item.isHidden = false;
                        }
                        else
                        {
                            item.isHidden = true;
                        }
                        filteredProducts.Add(item);
                    }
                }

                List<ProductItem> items = new List<ProductItem>();
                foreach (ProductItem item in _saveProducts)
                    if (!item.isHidden)
                        items.Add(item);

                Products = new ObservableCollection<ProductItem>(items);
            }
        }

        public void LoadOrderedBills()
        {
            var dataProvider = DataProvider.GetInstance();
            _orderedBills = new ObservableCollection<OrderBill>(dataProvider.GetOrderBillByIDStore(IDStore));
        }


        public void LoadProductTypes()
        {
            var dataProvider = DataProvider.GetInstance();
            List<ProductType> types = dataProvider.GetProductTypes();
            List<TypeItem> typeItems = new List<TypeItem>();
            foreach(ProductType type in types)
            {
                TypeItem typeItem = new TypeItem();
                typeItem.productType = type;
                typeItem.isChosen = false;

                typeItems.Add(typeItem);
            }
            _typeItems = new ObservableCollection<TypeItem>(typeItems);
        }
    }
}
