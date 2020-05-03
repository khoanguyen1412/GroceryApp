using GroceryApp.DataProviders;
using GroceryApp.Models;
using GroceryApp.Views.Drawer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

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
        public ICommand ShowDrawerCommand { get; set; }
        public HomePageViewModel()
        {
            LoadData();
            ShowDrawerCommand = new Command(ShowDrawer);
        }

        public void ShowDrawer()
        {
            var appDrawer = AppDrawer.GetInstance();
            appDrawer.FlyoutIsPresented = true;
        }

        public void LoadData()
        {
         
            _stores = new ObservableCollection<Store>(DataProvider.ListStores);
        }
    }
}
