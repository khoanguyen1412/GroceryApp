using Com.OneSignal;
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
using Xamarin.Essentials;
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

            
            Preferences.Set("IDLogin", "");
            //OneSignal.Current.SendTag("IsLogined", "0");

            //TEST INTERNET CONNECTTION 
            var httpClient = new HttpClient();

            try
            {
                var testInternet = await httpClient.GetStringAsync("https://newappgroc.azurewebsites.net/store/getstorebyid/test");
                OneSignal.Current.SetExternalUserId("");
                await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "user/update", user);
            }
            catch (Exception ex)
            {

            }
            
            App.Current.MainPage = new NavigationPage( LoginView.GetInstance());

        }
    }
}