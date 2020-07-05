using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public interface IRegisterViewModel
    {

    }
    public class RegisterViewModel : BaseViewModel, IRegisterViewModel
    {
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        private string _newPassword;
        public string NewPassword
        {
            get { return _newPassword; }
            set { _newPassword = value; OnPropertyChanged(nameof(NewPassword)); }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; OnPropertyChanged(nameof(ConfirmPassword)); }
        }

        private bool _hide1;
        public bool Hide1
        {
            get { return _hide1; }
            set { _hide1 = value; OnPropertyChanged(nameof(Hide1)); }
        }

        private bool _hide2;
        public bool Hide2
        {
            get { return _hide2; }
            set { _hide2 = value; OnPropertyChanged(nameof(Hide2)); }
        }


        private bool _showError;
        public bool ShowError
        {
            get { return _showError; }
            set { _showError = value; OnPropertyChanged(nameof(ShowError)); }
        }

        private string _errorStr;
        public string ErrorStr
        {
            get { return _errorStr; }
            set { _errorStr = value; OnPropertyChanged(nameof(ErrorStr)); }
        }

        public ICommand Hide1Command { get; set; }
        public ICommand Hide2Command { get; set; }
        public ICommand SignUpCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public RegisterViewModel()
        {
            SetUpData();
            ShowError = false;
            ErrorStr = "";
            Hide1Command = new Command(ChangeHide1);
            Hide2Command = new Command(ChangeHide2);
            SignUpCommand = new Command(SignUp);
            GoBackCommand = new Command(GoBack);
        }

        public async void GoBack()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        public async void SignUp()
        {
            if (!CheckValidNewPassword())
            {
                return;
            }
            if (CheckExistUsername())
            {
                return;
            }

            ShowError = false;

            //TẠO STORE
            string idStore = "Store_" + DateTime.Now.ToString("HHmmss");
            Store newStore = new Store
            {
                IDStore = idStore,
                StoreName="",
                ImageURL="defaultstore",
                StoreDescription="",
                StoreAddress="###",
                RatingStore=0,
                IsActive=0,
                
            };
            //encode password
            string encodePass = MD5Service.EncodeToMD5(NewPassword);
            //TẠO USER TƯƠNG ỨNG
            User newUser = new User
            {
                IDUser= Username,
                Password= encodePass,
                IDStore=idStore,
                PhoneNumber="",
                Address="###",
                Email="",
                ImageURL="defaultuser",
                BirthDate=DateTime.Today,
                UserName="",
                IsLogined=0
            };

            //insert STORE và USER lên database local
            DataUpdater.AddStore(newStore);
            DataUpdater.AddUser(newUser);
            //insert STORE và USER lên database online
            var httpClient = new HttpClient();
            await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "store/insert", newStore);
            await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "user/insert", newUser);

            await App.Current.MainPage.Navigation.PopAsync();
        }

        public bool CheckValidNewPassword()
        {
            bool valid = true;
            if (NewPassword == null || NewPassword == "")
            {
                ShowError = true;
                ErrorStr = "New password must not be empty";
                return false;
            }

            if (ConfirmPassword == null || ConfirmPassword == "")
            {
                ShowError = true;
                ErrorStr = "Confirm password must not be empty";
                return false;
            }
            if (NewPassword != ConfirmPassword)
            {
                ShowError = true;
                ErrorStr = "Password and confirm password do not match";
                return false;
            }

            return valid;
        }

        public bool CheckExistUsername()
        {
            foreach(User user in Database.Users)
                if (user.IDUser == Username)
                {
                    ShowError = true;
                    ErrorStr = "This username is already in use";
                    return true;
                }

            return false;
        }

        public void SetUpData()
        {
            Hide1 = true;
            Hide2 = true;
            Username = "";
            NewPassword = "";
            ConfirmPassword = "";
        }

        public void ChangeHide1()
        {
            Hide1 = !Hide1;
        }
        public void ChangeHide2()
        {
            Hide2 = !Hide2;
        }

    }
}
