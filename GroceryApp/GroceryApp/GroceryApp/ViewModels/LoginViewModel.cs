using Acr.UserDialogs;
using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Views.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public interface ILoginViewModel
    {

    }
    public class LoginViewModel : BaseViewModel, ICartViewModel
    {
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        private bool _hide;
        public bool Hide
        {
            get { return _hide; }
            set { _hide = value; OnPropertyChanged(nameof(Hide)); }
        }

        private bool _remember;
        public bool Remember
        {
            get { return _remember; }
            set { _remember = value; OnPropertyChanged(nameof(Remember)); }
        }
        public ICommand HideCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public ICommand ForgotCommand { get; set; }

        public LoginViewModel()
        {
            Username = Preferences.Get("Username",string.Empty);
            Password = Preferences.Get("Password", string.Empty); 
            Remember = Preferences.Get("Remember", false);
            HideCommand = new Command(ChangeHide);
            LoginCommand = new Command(Login);
            RegisterCommand = new Command(Register);
            ForgotCommand = new Command(ResetPassword);
        }

        public async void ResetPassword()
        {
            using (UserDialogs.Instance.Loading("Waiting.."))
            {
                await LoadServerDataAsync();
                await App.Current.MainPage.Navigation.PushAsync(new EmailVerifyView());
            }
                
        }

        public async void Register()
        {
            using (UserDialogs.Instance.Loading("Waiting.."))
            {
                await LoadServerDataAsync();
                await App.Current.MainPage.Navigation.PushAsync(new RegisterView(), true);
            }
            
        }

        public async void Login()
        {
            if (Remember)
            {
                Preferences.Set("Username", Username);
                Preferences.Set("Password", Password);
                Preferences.Set("Remember", true);
            }
            else
            {
                Preferences.Set("Username", "");
                Preferences.Set("Password", "");
                Preferences.Set("Remember", false);
            }
            using (UserDialogs.Instance.Loading("Loging.."))
            {
                await LoadServerDataAsync();
                await App.Current.MainPage.Navigation.PushAsync(new MiddleView(Username, Password), true);
            }
        }

        public async Task LoadServerDataAsync()
        {
            await ServerDatabase.LoadDataFromServer();
            Database.LoadDataToLocal();
        }

        public void ChangeHide()
        {
            Hide = !Hide;
        }

    }
}
