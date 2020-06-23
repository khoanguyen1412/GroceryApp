using GroceryApp.Models;
using GroceryApp.Views.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{

    public interface IChangePasswordViewModel
    {

    }
    public class ChangePasswordViewModel : BaseViewModel, IChangePasswordViewModel
    {
        private string _currentPassword;
        public string CurrentPassword
        {
            get { return _currentPassword; }
            set { _currentPassword = value; OnPropertyChanged(nameof(CurrentPassword)); }
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

        private bool _hide3;
        public bool Hide3
        {
            get { return _hide3; }
            set { _hide3 = value; OnPropertyChanged(nameof(Hide3)); }
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
        public ICommand Hide3Command { get; set; }

        public ICommand SaveChangeCommand { get; set; }

        public ChangePasswordViewModel()
        {
            //NO USE
        }

        public ChangePasswordViewModel(string password)
        {
            
            SetupPassword(password);

            ShowError = false;
            ErrorStr = "";
            Hide1Command = new Command(ChangeHide1);
            Hide2Command = new Command(ChangeHide2);
            Hide3Command = new Command(ChangeHide3);
            SaveChangeCommand = new Command(SaveChange);
        }

        public async void SaveChange()
        {
            if (!CheckValidNewPassword())
            {

                return;
            }
            ShowError = false;
            (UserSettingView.GetInstance().BindingContext as UserSettingViewModel).ChangePassword(NewPassword);
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

        public void SetupPassword(string password)
        {
            Hide1 = true;
            Hide2 = true;
            Hide3 = true;
            CurrentPassword = password;
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
        public void ChangeHide3()
        {
            Hide3 = !Hide3;
        }

        
    }
}
