﻿using GroceryApp.Models;
using GroceryApp.ViewModels;
using GroceryApp.Views.TabBars;
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
    public partial class StoreAddressView : ContentPage
    {
        public string parentName="StoreSetting";
        public StoreAddressView()
        {
            InitializeComponent();
        }

        public void SetForUser()
        {
            titleLabel.Text = "Your Address";
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            string message = CheckInfor();
            if (message != "")
            {
                DisplayAlert("Error", message, "OK");
                return;
            }
            AddressItem newAddress = new AddressItem
            {
                Country = CountryText.Text,
                City = CityText.Text,
                HouseNumber = HouseNumberText.Text,
                District=DistrictText.Text
            };
            
            //(App.Current.MainPage.Navigation.NavigationStack.ElementAt(0).BindingContext as StoreSetttingViewModel).ChangeAddress(newAddress);
            if (parentName == "StoreSetting")
                (TabbarStoreManager.GetInstance().Children.ElementAt(4).BindingContext as StoreSetttingViewModel).ChangeAddress(newAddress);
            else
                (UserSettingView.GetInstance().BindingContext as UserSettingViewModel).ChangeAddress(newAddress); 
            App.Current.MainPage.Navigation.PopAsync();
        }

        private string CheckInfor()
        {
            if (string.IsNullOrEmpty(CountryText.Text)
                && string.IsNullOrEmpty(CityText.Text)
                && string.IsNullOrEmpty(HouseNumberText.Text)
                && string.IsNullOrEmpty(DistrictText.Text)) return "At least one field has to have information";
            return "";
        }
    }
}