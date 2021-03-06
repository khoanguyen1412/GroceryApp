﻿using Acr.UserDialogs;
using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Services;
using GroceryApp.Views.Popups;
using GroceryApp.Views.TabBars;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public interface IFinalBillViewModel
    {

    }
    public class FinalBillViewModel:BaseViewModel,IFinalBillViewModel
    {
        public bool hasSent = false;
        public OrderBillItem OrderItem { get; set; }
        //public OrderBill Order { get; set; }
        public ICommand SendOrderCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        public string StoreName
        {
            get
            {
                DataProvider dataProvider = DataProvider.GetInstance();
                return dataProvider.GetStoreByIDStore(OrderItem.Order.IDStore).StoreName;
            }
        }

        public string StoreAddress
        {
            get
            {
                DataProvider dataProvider = DataProvider.GetInstance();
                return dataProvider.GetStoreByIDStore(this.OrderItem.Order.IDStore).StoreAddress;
            }
        }

        public ObservableCollection<Product> OrderedProducts
        {
            get
            {
                //DataProvider dataProvider = DataProvider.GetInstance();
                return new ObservableCollection<Product>(this.OrderItem.AddedProducts);
            }
        }

        public FinalBillViewModel(OrderBillItem orderItem)
        {
            this.OrderItem = orderItem;
            //this.Order = orderItem.Order;
            SendOrderCommand = new Command(SendOrder);
        }

        public async void GoBack()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
        public async void SendOrder()
        {

            //TEST INTERNET CONNECTTION 
            var httpClient = new HttpClient();
            string x = "";
            try
            {
                var testInternet = await httpClient.GetStringAsync(ServerDatabase.localhost+"store/getstorebyid/test");
                x = testInternet;
            }
            catch (Exception ex)
            {
                var stack = App.Current.MainPage.Navigation.NavigationStack;
                var page = stack[stack.Count - 1];
                await page.DisplayAlert("Error", "Action fail, check your internet connection and try again!", "OK");
                return;
            }

            using (UserDialogs.Instance.Loading("wait.."))
            {
                var result= await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "orderbill/insert", OrderItem.Order);
                var a = await result.Content.ReadAsStringAsync();
                JObject rss = JObject.Parse(a);

                var resultAPI = rss["result"];
                if (resultAPI.ToString() == "fail")
                {
                    return;
                }
                

                foreach (Product product in OrderItem.AddedProducts)
                {
                    product.State = ProductState.InBill;
                    product.IDOrderBill = OrderItem.Order.IDOrderBill;
                    await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "product/update", product);
                }
                    
            }

            //update orderbill trong data local
            DataUpdater.InsertOrderBill(OrderItem.Order);
            //update products trong bill ở data local
            DataUpdater.UpdateProduct(OrderItem.AddedProducts);

            //update UI CART
            var cartVM = TabBarCustomer.GetInstance().Children[2].BindingContext as CartViewModel;
            cartVM.LoadData();
            //update UI Orders
            var ordersVM = TabBarCustomer.GetInstance().Children[3].BindingContext as ListOrdersViewModel;
            ordersVM.LoadData();

            //PUSH NOTI
            string datas = PushNotificationService.ConvertDataInsertOrderBill(OrderItem.Order);
            PushNotificationService.Push(NotiNumber.MakeBillForStore, datas, false);
            PushNotificationService.Push(NotiNumber.MakeBillForOther, datas, true);

            hasSent = true;

            //await PopupNavigation.Instance.PushAsync(successPopup);
            await App.Current.MainPage.Navigation.PopAsync();
            
            
        }

        public FinalBillViewModel()
        {
            //NO USE
            //SendOrderCommand = new Command<object>(SendOrder);
        }

       

    }
}
