using GroceryApp.DataProviders;
using GroceryApp.Models;
using GroceryApp.Views.Screens;
using Plugin.SharedTransitions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public interface IListStoresViewModel
    {

    }
    public class ListStoresViewModel : BaseViewModel, IListStoresViewModel
    {
        private ObservableCollection<Store> _stores;

        public ObservableCollection<Store> Stores
        {
            get { return _stores; }
            set { _stores = value; OnPropertyChanged(nameof(Stores)); }
        }

        private ObservableCollection<ProductType> _productTypes;

        public ObservableCollection<ProductType> ProductTypes
        {
            get { return _productTypes; }
            set { _productTypes = value; OnPropertyChanged(nameof(ProductTypes)); }
        }

        public ICommand ShowStoreCommand { get; set; }
        public ListStoresViewModel()
        {
            LoadData();
            ShowStoreCommand = new Command(ShowStore);
        }

        public async void ShowStore()
        {
            await App.Current.MainPage.Navigation.PushAsync(new ShowStoreView(), true);
        }

        public void LoadData()
        {
            
            _productTypes= new ObservableCollection<ProductType>(DataProvider.ListProductTypesYel);
            _stores = new ObservableCollection<Store>(DataProvider.ListStores);
        }
    }
}
