using GroceryApp.Models;
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

        public ICommand Hide1Command { get; set; }
        public ICommand Hide2Command { get; set; }
        public ICommand Hide3Command { get; set; }

        public ChangePasswordViewModel()
        {
            SetupPassword("");

            Hide1Command = new Command(ChangeHide1);
            Hide2Command = new Command(ChangeHide2);
            Hide3Command = new Command(ChangeHide3);
        }

        public ChangePasswordViewModel(string password)
        {
            
            SetupPassword(password);

            Hide1Command = new Command(ChangeHide1);
            Hide2Command = new Command(ChangeHide2);
            Hide3Command = new Command(ChangeHide3);
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
