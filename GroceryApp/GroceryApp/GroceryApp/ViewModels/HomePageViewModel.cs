using GroceryApp.DataProviders;
using GroceryApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GroceryApp.ViewModels
{
    public interface IHomePageViewModel
    {

    }
    public class HomePageViewModel: BaseViewModel, IHomePageViewModel
    {
        private ObservableCollection<Store> _stores;

        public ObservableCollection<Store> Stores
        {
            get { return _stores; }
            set { _stores = value; OnPropertyChanged(nameof(Stores)); }
        }
        public HomePageViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
         
            _stores = new ObservableCollection<Store>(DataProvider.ListStores);
        }
    }
}
