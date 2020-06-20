using GroceryApp.Data;
using GroceryApp.ViewModels;
using GroceryApp.Views.Drawer;
using GroceryApp.Views.Screens;
using GroceryApp.Views.TabBars;
using Plugin.SharedTransitions;
using System;
using System.Threading;
using System.Threading.Tasks;
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

            ///MainPage = new SharedTransitionNavigationPage(new FinalBillView());
            //MainPage = new NavigationPage(new DetailProductView());
            //MainPage = new SharedTransitionNavigationPage(new TabBarCustomer());
            ///MainPage = new SharedTransitionNavigationPage(new TabbarStoreManager());


            //MainPage = new NavigationPage(new StoreSettingView());


            //MainPage = new NavigationPage(UserSettingView.GetInstance());
            Infor.IDUser = "1";
            Infor.IDStore = "1";
            Infor.IDCart = "1";
            MainPage = new NavigationPage(new LoginView());

        }

        public async Task LoadServerDataAsync()
        {
            Infor.IDUser = "1";
            Infor.IDStore = "1";
            Infor.IDCart = "1";
            await Task.Run(async () =>
            {
                await ServerDatabase.LoadDataFromServer();
            });

            Database.LoadDataToLocal();
            
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
