using Acr.UserDialogs;
using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Services;
using GroceryApp.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroceryApp.Views.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmailVerifyView : ContentPage
    {

        public List<User> Users = new List<User>();
        public EmailVerifyView()
        {
            InitializeComponent();

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                App.Current.MainPage.Navigation.PopAsync();
            };
            backLabel.GestureRecognizers.Add(tapGestureRecognizer);

        }


        private async void Send_Clicked(object sender, EventArgs e)
        {
            
            if (CheckValidInfor() == false)
            {
                await DisplayAlert("Error", "Your infor is wrong, check again!", "OK");
                return;
            }
            Users = Database.Users;
            
            User user = CheckExistInfor();
            if (user == null)
            {
                await DisplayAlert("Error", "Username or email is wrong, check again!", "OK");
                return;
            }

            EmailVerifyItem emailItem = null;
            try
            {
                //GỬI CODE QUA MAIL
                using (UserDialogs.Instance.Loading("Sending code.."))
                {
                    emailItem = EmailService.SendCodeToEmail(EmailEntry.Text);

                }
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error", "Action fail, check your internet connection and try again!", "OK");
                return;
            }
            

            //QUA PAGE NHẬP CODE
            using (UserDialogs.Instance.Loading("Waiting.."))
            {
                var CodeVerifyPage = new CodeVerifyView();
                CodeVerifyPage.SetUserData(user, emailItem);
                await App.Current.MainPage.Navigation.PushAsync(CodeVerifyPage, true);
            }
            

        }

        

        

        private User CheckExistInfor()
        {
            foreach(User user in Users)
                if (user.IDUser == UsernameEntry.Text)
                {
                    if (user.Email == EmailEntry.Text) return user;
                    return null;
                }
            return null;
        }

        

        private bool CheckValidInfor()
        {
            if (string.IsNullOrEmpty(UsernameEntry.Text) || string.IsNullOrEmpty(EmailEntry.Text)) return false;
            if (EmailService.CheckValidEmail(EmailEntry.Text) == false) return false;
            return true;
        }
    }
}