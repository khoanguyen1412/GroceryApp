using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Views.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public interface IUserSettingViewModel
    {

    }
    public class UserSettingViewModel: BaseViewModel, IUserSettingViewModel
    {
        
        DataProvider dataProvider = DataProvider.GetInstance();

        private User _currentUser;

        public User CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; OnPropertyChanged(nameof(CurrentUser)); }
        }

        private DateTime _birthDate;

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; OnPropertyChanged(nameof(BirthDate)); }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(nameof(Address)); }
        }

        public ICommand ChangePasswordCommand { get; set; }
        public ICommand ChangeAddressCommand { get; set; }

        public UserSettingViewModel()
        {
            LoadData();
            ChangePasswordCommand = new Command(ChangePassword);
            ChangeAddressCommand = new Command(ChangeAddress);
        }

        public async void ChangeAddress()
        {

            var addressPage = new StoreAddressView();
            addressPage.parentName = "UserSetting";
            string[] items = CurrentUser.Address.Split('#');
            addressPage.BindingContext = new AddressItem
            {
                HouseNumber = items[0],
                District = items[1],
                City = items[2],
                Country = items[3]
            };
            await App.Current.MainPage.Navigation.PushAsync(addressPage, true);
        }

        public async void ChangePassword()
        {
            var changePasswordPage = new ChangePasswordView();
            ChangePasswordViewModel vm = new ChangePasswordViewModel(CurrentUser.Password);
            changePasswordPage.BindingContext = vm;
            await App.Current.MainPage.Navigation.PushAsync(changePasswordPage, true);
        }

        public void LoadData()
        {
            CurrentUser = dataProvider.GetUserByIDUser(Infor.IDUser);
            BirthDate = CurrentUser.BirthDate;
            Address = CurrentUser.Address;
        }

        public void ChangeAddress(AddressItem newAddress)
        {
            string address = newAddress.HouseNumber + "#" + newAddress.District + "#" + newAddress.City + "#" + newAddress.Country;
            Address = address;
            CurrentUser.Address = address;
        }

    }
}
