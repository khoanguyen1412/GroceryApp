using Acr.UserDialogs;
using GroceryApp.Models;
using GroceryApp.Services;
using GroceryApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroceryApp.Views.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CodeVerifyView : ContentPage
    {

        User user;
        EmailVerifyItem emailItem;
        public CodeVerifyView()
        {
            InitializeComponent();
        }

        public void SetUserData(User user, EmailVerifyItem emailItem)
        {
            this.user = user;
            this.emailItem = emailItem;
        }

        private async void ConfirmBtn_Clicked(object sender, EventArgs e)
        {
            if (!CheckValidCode())
            {
                await DisplayAlert("Error", "Your code is wrong, check again!", "OK");
                return;
            }

            if (!emailItem.CheckTime())
            {
                bool result= await DisplayAlert("Error", "Your code is expired!", "Resend","Cancel");
                if (result)
                {
                    using (UserDialogs.Instance.Loading("Sending code.."))
                    {
                        this.emailItem = EmailService.SendCodeToEmail(this.user.Email);
                    }
                        
                }
                return;
            }

            //CHECK XONG, CHUYỂN QUA MÀN HÌNH RESET PASSWORD
            var resetPage = new ResetPasswordView();
            var ResetVM = new ResetPasswordViewModel(this.user);
            resetPage.BindingContext = ResetVM;
            await App.Current.MainPage.Navigation.PushAsync(resetPage, true);
        }

        private bool CheckValidCode()
        {
            if (string.IsNullOrEmpty(CodeEntry.Text)) return false;
            if (CodeEntry.Text == this.emailItem.Code) return true;
            return false;
        }
    }
}