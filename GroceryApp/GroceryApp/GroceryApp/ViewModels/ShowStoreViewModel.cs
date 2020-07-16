using Acr.UserDialogs;
using FFImageLoading.Forms.Args;
using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Services;
using GroceryApp.Views.Popups;
using GroceryApp.Views.Screens;
using GroceryApp.Views.TabBars;
using Plugin.SharedTransitions;
using Prism.Commands;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public class NameComparer : IComparer<ProductItem>
    {
        public int Compare(ProductItem x, ProductItem y)
        {
            return String.Compare(x.Product.ProductName, y.Product.ProductName);
        }
    }

    public class PriceComparer : IComparer<ProductItem>
    {
        public int Compare(ProductItem x, ProductItem y)
        {
            if (x.Product.Price > y.Product.Price) return 1;
            if (x.Product.Price == y.Product.Price) return 0;
            return -1;
        }
    }

    public interface IShowStoreViewModel
    {

    }

    public class FeedBack
    {
        public string CustomerName { get; set; }
        public string CustomerReview { get; set; }
        public string StoreAnswer { get; set; }
    }

    

    

    public class ShowStoreViewModel : BaseViewModel, IShowStoreViewModel
    {
        //thuộc tính filter
        public string SortType;
        public double FilterMin;
        public double FilterMax;
        public bool isFilter;

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


        private ObservableCollection<FeedBack> _feedBacks;
        public ObservableCollection<FeedBack> FeedBacks
        {
            get { return _feedBacks; }
            set { _feedBacks = value; OnPropertyChanged(nameof(FeedBacks)); }
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

        public double Rating { 
            get
            {
                var dataProvider = DataProvider.GetInstance();
                return dataProvider.GetStoreByIDStore(IDStore).RatingStore;
            }
        }
        public ICommand ShowDetailProductCommand { get; set; }
        public ICommand ChooseCommand { get; set; }
        public ICommand FilterCommand { get; set; }

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
            LoadData(false);
            LoadReviews();
            ShowDetailProductCommand = new Command<ProductItem>(ShowDetailProduct);
            ChooseCommand = new Command<TypeItem>(Choose);
            FilterCommand = new Command(OpenFilter);
        }



        public async void OpenFilter()
        {
            using (UserDialogs.Instance.Loading("Loading.."))
            {
                await PopupNavigation.Instance.PushAsync(FilterPopupView.GetInstance());

            }
        }

        public void LoadReviews()
        {
            List<FeedBack> feedBack = new List<FeedBack>();
            foreach (OrderBill order in _orderedBills)
                if (order.State == OrderState.Received && !string.IsNullOrEmpty(order.Review))
                {
                    FeedBack newFeedBack = new FeedBack();
                    newFeedBack.CustomerName = order.UserName;
                    newFeedBack.CustomerReview = order.Review;
                    newFeedBack.StoreAnswer = order.StoreAnswer;
                    feedBack.Add(newFeedBack);
                }
            FeedBacks=new ObservableCollection<FeedBack>(feedBack);
        }

        public void SetIDStore(string IDStore)
        {
            this.IDStore = IDStore;
            LoadData(false);
        }

        public bool CheckNoSelectedProduct()
        {
            for (int i = 0; i < _saveProducts.Count; i++)
                if (_saveProducts[i].Product.QuantityOrder > 0)
                {
                    return false;
                }
            return true;
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
            cartVM.LoadData();

            //PUSH NOTI
            string datas= PushNotificationService.ConvertDataAddToCart(changedProducts);
            PushNotificationService.Push(NotiNumber.AddToCart, datas,true);
        }

        public void ApplyFilter(bool isFilter, string sortType, double filterMin,double filterMax)
        {
            this.isFilter = isFilter;
            if (!isFilter) return;
            this.SortType = sortType;
            this.FilterMin = filterMin;
            this.FilterMax = filterMax;
            LoadProducts(false);
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
                LoadProducts(false);
            }
            if (_typeItems[choosingIndex].isChosen == false)
            {
                currentType = -1;
                LoadProducts(false);
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

        public void LoadData(bool isReload)
        {
            isFilter = false;
            LoadProducts(isReload);
            LoadOrderedBills();
            LoadProductTypes();
        }

        public void LoadProducts(bool isReload)
        {

            if(_saveProducts == null || isReload)
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
            FiltProducts();
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

        public void FiltProducts()
        {
            if (!isFilter) return;
            List<ProductItem> source = Products.ToList();
            List<ProductItem> filterList = new List<ProductItem>();
            if (FilterMin != -1)
            {
                foreach (ProductItem item in source)
                    if (item.Product.Price >= FilterMin && item.Product.Price <= FilterMax)
                        filterList.Add(item);
            }
            else
            {
                filterList = source;
            }

            if (!string.IsNullOrEmpty(SortType))
            {
                NameComparer nameCompare = new NameComparer();
                PriceComparer priceCompare = new PriceComparer();
                if(SortType== "A->Z")
                {
                    filterList.Sort(nameCompare);
                }
                if (SortType == "Z->A")
                {
                    filterList.Sort(nameCompare);
                    filterList.Reverse();
                }
                if (SortType == "Min->Max")
                {
                    filterList.Sort(priceCompare);
                }
                if (SortType == "Max->Min")
                {
                    filterList.Sort(priceCompare);
                    filterList.Reverse();
                }
            }

            Products = new ObservableCollection<ProductItem>(filterList);
        }
    }
}
