using GroceryApp.Views.Drawer;
using GroceryApp.Views.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GroceryApp.Data
{
    public class HandleException
    {
        public static async void Onboarding()
        {
            App.Current.MainPage.DisplayAlert("Error", "Load data fail, check your internet connection and try again!", "OK");
        }
        public static async void BeforeLogin()
        {
            var loginPage = LoginView.GetInstance();
            await loginPage.DisplayAlert("Error","Load data fail, check your internet connection and try again!","OK");
        }
    }
}
