using Acr.UserDialogs;
using Com.OneSignal;
using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Views.Drawer;
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
    public partial class MiddleView : ContentPage
    {
        DataProvider dataProvider = DataProvider.GetInstance();
        string Username;
        string Password;
        public MiddleView()
        {
            InitializeComponent();
        }

        public MiddleView(string Username,string Password)
        {
            InitializeComponent();
            this.Username = Username;
            this.Password = Password;

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!dataProvider.CheckUserExist(this.Username))
            {
                await DisplayAlert("Account not exist", "Wrong username or password, please try again", "OK");
                await App.Current.MainPage.Navigation.PopAsync();
                return;
            };
            User user = dataProvider.GetUserByIDUser(this.Username);
            user.IsLogined = 1;

            OneSignal.Current.SetExternalUserId(user.IDUser);
            OneSignal.Current.SendTag("IsLogined", "1");
            var httpClient = new HttpClient();
            await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "user/update", user);

            Infor.IDUser = this.Username;
            Infor.IDStore = dataProvider.GetIDStoreByIDUser(this.Username);
            
            Infor.IDCart = this.Username;
            App.Current.MainPage = AppDrawer.GetInstance();

            
        }
    }
}