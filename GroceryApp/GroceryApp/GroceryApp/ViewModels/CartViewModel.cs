using GroceryApp.Models;
using GroceryApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using GroceryApp.DataProviders;

namespace GroceryApp.ViewModels
{
    
    public interface ICartViewModel
    {

    }
    public class CartViewModel : BaseViewModel, ICartViewModel
    {
        
        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged(nameof(Products)); }
        }

        public ICommand ShowConfirmInforCommand { get; set; }
        public CartViewModel()
        {
            LoadData();
            ShowConfirmInforCommand = new Command(ShowConfirmInfor);
        }
        
        public async void ShowConfirmInfor()
        {
            await PopupNavigation.Instance.PushAsync(new ConfirmInforOrderPopupView());
        }

        public void LoadData()
        {
            
            _products = new ObservableCollection<Product>(DataProvider.ListProducts);
        }
    }
}
