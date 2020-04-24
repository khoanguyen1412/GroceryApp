using GroceryApp.DataProviders;
using GroceryApp.Models;
using GroceryApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public interface IProductManagerViewModel
    {

    }
    public class ProductManagerViewModel: BaseViewModel, IProductManagerViewModel
    {
        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> SoldProducts
        {
            get
            {
                ObservableCollection<Product> result = new ObservableCollection<Product>();
                foreach (Product product in _products)
                    if (product.StateInStore == "SOLD")
                        result.Add(product);
                return result;
            }
        }

        public ObservableCollection<Product> HiddenProducts
        {
            get {
                ObservableCollection<Product> result = new ObservableCollection<Product>();
                foreach (Product product in _products)
                    if (product.StateInStore == "HIDDEN")
                        result.Add(product);
                return result; 
            }
        }

        public ObservableCollection<ProductType> ProductTypes
        {
            get { 

                return new ObservableCollection<ProductType>(DataProvider.ListProductTypesYel); 
            }
        }
        public ICommand AddProductCommand { get; set; }
        public ICommand EditProductCommand { get; set; }
        public ProductManagerViewModel()
        {
            LoadData();
            AddProductCommand = new Command(AddProduct);
            EditProductCommand = new Command(EditProduct);
        }

        public async void AddProduct()
        {
            var AddPage = new AddProductPopupView();
            await PopupNavigation.Instance.PushAsync(AddPage);
        }

        public async void EditProduct()
        {
            var EditPage = new EditProductPopupView();
            await PopupNavigation.Instance.PushAsync(EditPage);
        }

        public void LoadData()
        {
            _products = new ObservableCollection<Product>(DataProvider.ListProductManager);
        }
    }
}
