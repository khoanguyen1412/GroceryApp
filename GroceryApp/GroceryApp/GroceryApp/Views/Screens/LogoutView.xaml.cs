﻿using Com.OneSignal;
using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Views.Drawer;
using GroceryApp.Views.TabBars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroceryApp.Views.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogoutView : ContentPage
    {

        DataProvider dataProvider = DataProvider.GetInstance();
        public LogoutView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            AppDrawer.Destroy();
            TabBarCustomer.Destroy();
            TabbarStoreManager.Destroy();
            UserSettingView.Destroy();
            User user = dataProvider.GetUserByIDUser(Infor.IDUser);
            user.IsLogined = 0;

            OneSignal.Current.SetExternalUserId(user.IDUser);
            OneSignal.Current.SendTag("IsLogined", "0");

            var httpClient = new HttpClient();
            await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "user/update", user);
            App.Current.MainPage = new NavigationPage( LoginView.GetInstance());

        }
    }
}