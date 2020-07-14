using Com.OneSignal;
using Com.OneSignal.Abstractions;
using GroceryApp.Data;
using GroceryApp.Services;
using GroceryApp.ViewModels;
using GroceryApp.Views.Drawer;
using GroceryApp.Views.Popups;
using GroceryApp.Views.Screens;
using GroceryApp.Views.TabBars;
using Plugin.SharedTransitions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroceryApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //LoadServerDataAsync();

            //MainPage = new SharedTransitionNavigationPage(new FinalBillView());
            //MainPage = new NavigationPage(new DetailProductView());
            //MainPage = new SharedTransitionNavigationPage(new TabBarCustomer());

            //Infor.IDUser = "1";
            //Infor.IDStore = "1";
            //Infor.IDCart = "1";
            MainPage = new NavigationPage(new FilterPopupView());
            OneSignal.Current.StartInit("b5f59a9f-3873-47a9-80e5-ca37fb75610a")
                .HandleNotificationReceived(PushNotificationService.HandleNotificationReceived)
                .HandleNotificationOpened(PushNotificationService.HandleNotificationOpened)
                .InFocusDisplaying(OSInFocusDisplayOption.Notification)
                .EndInit();

        }

        
        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
