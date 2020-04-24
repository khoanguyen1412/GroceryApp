using GroceryApp.DataProviders;
using GroceryApp.Models;
using GroceryApp.Views.Screens;
using Plugin.SharedTransitions;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public class ShowStoreViewModel : BaseTransitionViewModel, IShowStoreViewModel
    {

        private ObservableCollection<Product> _products;

        private ObservableCollection<OrderBill> _orderedBills;

        private ObservableCollection<ProductType> _productTypes;

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged(nameof(Products)); }
        }

        public ObservableCollection<ProductType> ProductTypes
        {
            get { return _productTypes; }
            set { _productTypes = value; OnPropertyChanged(nameof(ProductTypes)); }
        }

        public ObservableCollection<OrderBill> OrderBills
        {
            get { return _orderedBills; }
            set { _orderedBills = value; OnPropertyChanged(nameof(OrderBills)); }
        }

        
        public ObservableCollection<FeedBack> FeedBacks
        {
            get {
                ObservableCollection<FeedBack> feedBack = new ObservableCollection<FeedBack>();
                foreach(OrderBill order in _orderedBills)
                {
                    FeedBack newFeedBack = new FeedBack();
                    newFeedBack.CustomerReview = order.Review;
                    newFeedBack.StoreAnswer = order.StoreAnswer;
                    feedBack.Add(newFeedBack);
                }
                return feedBack; 
            }
        }

        private int _selectedProductId;
        public int SelectedProductId
        {
            get => _selectedProductId;
            set => SetProperty(ref _selectedProductId, value);
        }


        public string StoreName
        {
            get { return "Fruits Kingdom"; }
        }
        public string ImageURL
        {
            get { return "https://st3.depositphotos.com/1346781/17800/i/1600/depositphotos_178006858-stock-photo-a-wide-variety-of-fresh.jpg"; }
        }
        public string StoreDescription
        {
            get { return "All kind of fruits"; }
        }
        public string StoreAddress
        {
            get { return "192 Trần Quốc Toản, Quận 1, TPHCM"; }
        }

        public DelegateCommand<Product> ShowDetailProductCommand { get; set; }
        //public ICommand ShowDetailProductCommand { get; set; }
        public ShowStoreViewModel()
        {
            LoadData();
            ShowDetailProductCommand = new DelegateCommand<Product>(ShowDetailProduct);
        }

        public async void ShowDetailProduct(Product product)
        {
            SelectedProductId = product.ID;
            var detailPage = new DetailProductView();
            detailPage.BindingContext = product;
            await App.Current.MainPage.Navigation.PushAsync(detailPage, true);
        }

        public void LoadData()
        {
            loadProducts();
            loadOrderedBills();
            loadProductTypes();
        }

        public void loadProducts()
        {
            _products = new ObservableCollection<Product>(DataProvider.ListProducts);
        }

        public void loadOrderedBills()
        {

            _orderedBills = new ObservableCollection<OrderBill>(DataProvider.ListOrders);
        }
        

        public void loadProductTypes()
        {

            _productTypes = new ObservableCollection<ProductType>(DataProvider.ListProductTypesYel);
        }
    }
}
