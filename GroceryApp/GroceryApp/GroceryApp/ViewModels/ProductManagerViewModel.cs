﻿using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Services;
using GroceryApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
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
        HttpClient httpClient = new HttpClient();
        public int currentType = -1;
        DataProvider dataProvider = DataProvider.GetInstance();
        private ObservableCollection<Product> _products;

        private ObservableCollection<TypeItem> _typeItems;
        public ObservableCollection<TypeItem> TypeItems
        {
            get { return _typeItems; }
            set { _typeItems = value; OnPropertyChanged(nameof(TypeItems)); }
        }

        //quản lý source product cho toàn bộ màn hình(ko thêm, bớt, chỉ chỉnh sửa)
        private ObservableCollection<ProductItem> _saveProducts;
        private ObservableCollection<ProductItem> _soldProducts;
        private ObservableCollection<ProductItem> _deletedProducts;

        public ObservableCollection<ProductItem> SoldProducts
        {
            get
            {
                return _soldProducts;
            }
            set { _soldProducts = value; OnPropertyChanged(nameof(SoldProducts)); }
        }

        public ObservableCollection<ProductItem> DeletedProducts
        {
            get {
                return _deletedProducts;
            }
            set { _deletedProducts = value; OnPropertyChanged(nameof(DeletedProducts)); }
        }

        
        public ICommand AddProductCommand { get; set; }
        public ICommand DeleteProductCommand { get; set; }
        public ICommand EditProductCommand { get; set; }
        public ICommand RestoreProductCommand { get; set; }
        public ICommand ChooseTypeCommand { get; set; }
        public ProductManagerViewModel()
        {
            LoadData();
            AddProductCommand = new Command(AddProduct);
            EditProductCommand = new Command<ProductItem>(EditProduct);
            ChooseTypeCommand = new Command<TypeItem>(ChooseType);
            DeleteProductCommand = new Command<ProductItem>(DeleteProduct);
            RestoreProductCommand = new Command<ProductItem>(RestoreProduct);
        }

        public async void RestoreProduct(ProductItem productItem)
        {
            Product restoredProduct = DataUpdater.RestoreProductInStore(productItem.Product);
            //update product ở database server
            await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "product/update", restoredProduct);

            LoadProducts();

            //PUSH NOTI
            string datas = PushNotificationService.ConvertDataUpdateProduct(restoredProduct);
            PushNotificationService.Push(NotiNumber.UpdateProduct, datas, true);
        }

        public async void DeleteProduct(ProductItem productItem)
        {

            //update product ở database local
            Product deletedProduct= DataUpdater.DeleteProductInStore(productItem.Product);
            //update product ở database server
            await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "product/update", deletedProduct);

            LoadProducts();

            //PUSH NOTI
            string datas = PushNotificationService.ConvertDataUpdateProduct(deletedProduct);
            PushNotificationService.Push(NotiNumber.UpdateProduct, datas, true);
        }

        public void ChooseType(TypeItem typeItem)
        {
            int choosingIndex = -1;
            for (int i = 0; i < _typeItems.Count; i++)
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

        public async void AddProduct()
        {
            var AddPage = new AddProductPopupView();
            AddPage.BindingContext = new AddProductPopupViewModel();
            await PopupNavigation.Instance.PushAsync(AddPage);
        }

        public async void EditProduct(ProductItem productItem)
        {
            var EditPage = new EditProductPopupView();
            EditPage.BindingContext = new EditProductPopupViewModel(productItem.Product);
            await PopupNavigation.Instance.PushAsync(EditPage);
        }

        public void LoadData()
        {
            LoadProductTypes();
            LoadProducts();
            _products = new ObservableCollection<Product>(dataProvider.GetProductOfMyStore());
        }



        public void LoadProducts()
        {
            if (_saveProducts == null)
            {
                List<ProductItem> productItems = new List<ProductItem>();
                
                List<Product> products = dataProvider.GetProductOfMyStore();
                foreach (Product product in products)
                {
                    ProductItem item = new ProductItem
                    {
                        Product = product,
                        isHidden = false
                    };

                    productItems.Add(item);
                }

                _saveProducts = new ObservableCollection<ProductItem>(productItems);
                List<ProductItem> soldItems = new List<ProductItem>();
                List<ProductItem> deletedItems = new List<ProductItem>();
                foreach (ProductItem item in _saveProducts)
                    if (item.Product.StateInStore == ProductStateInStore.Selling)
                    {
                        if (!item.isHidden)
                            soldItems.Add(item);
                    }
                    else
                    {
                        deletedItems.Add(item);
                    }

                SoldProducts = new ObservableCollection<ProductItem>(soldItems);
                DeletedProducts = new ObservableCollection<ProductItem>(deletedItems);
            }
            else
            {
                
                if (currentType == -1)
                {
                    foreach (ProductItem item in _saveProducts)
                        item.isHidden = false;
                }
                else
                {
                    foreach (ProductItem item in _saveProducts)
                    {
                        if (item.Product.IDType == (currentType + 1).ToString())
                        {
                            item.isHidden = false;
                        }
                        else
                        {
                            item.isHidden = true;
                        }
                    }
                }
                List<ProductItem> soldItems = new List<ProductItem>();
                List<ProductItem> deletedItems = new List<ProductItem>();

               
                foreach (ProductItem item in _saveProducts)
                    if (item.Product.StateInStore == ProductStateInStore.Selling)
                    {
                        if (!item.isHidden)
                            soldItems.Add(item);
                    }
                    else
                    {
                        deletedItems.Add(item);
                    }

                SoldProducts = new ObservableCollection<ProductItem>(soldItems);
                DeletedProducts = new ObservableCollection<ProductItem>(deletedItems);
                
            }
        }

        public void LoadProductTypes()
        {
            
            List<ProductType> types = dataProvider.GetProductTypes();
            List<TypeItem> typeItems = new List<TypeItem>();
            foreach (ProductType type in types)
            {
                TypeItem typeItem = new TypeItem();
                typeItem.productType = type;
                typeItem.isChosen = false;

                typeItems.Add(typeItem);
            }
            _typeItems = new ObservableCollection<TypeItem>(typeItems);
        }

        public async void UpdateProduct(Product updatedProduct)
        {
            foreach(ProductItem item in _saveProducts)
                if (item.Product.IDProduct == updatedProduct.IDProduct)
                {
                    item.Product = updatedProduct;
                    break;
                }

            //Update database local
            DataUpdater.UpdateProduct(updatedProduct);
            //Update database server
            await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "product/update", updatedProduct);
            LoadProducts();

            //PUSH NOTI
            string datas = PushNotificationService.ConvertDataUpdateProduct(updatedProduct);
            PushNotificationService.Push(NotiNumber.UpdateProduct, datas, true);
        }

        public async void AddProduct(Product newProduct)
        {
            ProductItem newItem = new ProductItem
            {
                Product = newProduct,
                isHidden = false
            };
            _saveProducts.Add(newItem);

            //Thêm product ở database local
            DataUpdater.AddProduct(newProduct);
            //Thêm product ở database server
            await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "product/insert", newProduct);

            LoadProducts();

            //PUSH NOTI
            string datas = PushNotificationService.ConvertDataAddProduct(newProduct);
            PushNotificationService.Push(NotiNumber.AddProduct, datas, true);
        }
    }
}
