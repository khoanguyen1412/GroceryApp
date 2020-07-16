using Acr.UserDialogs;
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
    public interface IResetPasswordViewModel
    {

    }
    public class ResetPasswordViewModel : BaseViewModel, IResetPasswordViewModel
    {
        public User user;
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
        public ICommand ResetCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ResetPasswordViewModel(User user)
        {
            this.user = user;
            SetUpData();
            ShowError = false;
            ErrorStr = "";
            Hide1Command = new Command(ChangeHide1);
            Hide2Command = new Command(ChangeHide2);
            ResetCommand = new Command(Reset);
            GoBackCommand = new Command(GoBack);
        }

        public async void GoBack()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        public async void Reset()
        {
            if (!CheckValidNewPassword())
            {

                return;
            }
            ShowError = false;
            
            user.Password = MD5Service.EncodeToMD5(NewPassword);

            try
            {
                using (UserDialogs.Instance.Loading("Reseting.."))
                {
                    //update user ở database server
                    var httpClient = new HttpClient();
                    await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "user/update", user);
                    //update user ở database local
                    DataUpdater.UpdateUser(user);
                }
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error", "Action fail, check your internet connection and try again!", "OK");
                return;
            }

            

            MessageService.Show("Update infor successfully", 0);

            //PUSH NOTI
            string datas = PushNotificationService.ConvertDataUpdateUser(user);
            PushNotificationService.Push(NotiNumber.UpdateUser, datas, true);

            await App.Current.MainPage.Navigation.PopToRootAsync();
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

        public void SetUpData()
        {
            Hide1 = true;
            Hide2 = true;
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

        public ResetPasswordViewModel()
        {
            //NO USE

        }
    }
}
