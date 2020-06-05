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
    public partial class StoreInformationView : ContentPage
    {

        public StoreInformationView()
        {
            InitializeComponent();
        }
        private void Save_Clicked(object sender, EventArgs e)
        {
            StoreInfor newInfor = new StoreInfor
            {
                StoreName = NameEntry.Text,
                StoreDescription = DescEntry.Text
            };
            //(App.Current.MainPage.Navigation.NavigationStack.ElementAt(0).BindingContext as StoreSetttingViewModel).ChangeInfor(newInfor);
            (TabbarStoreManager.GetInstance().Children.ElementAt(4).BindingContext as StoreSetttingViewModel).ChangeInfor(newInfor);

            App.Current.MainPage.Navigation.PopAsync();
        }
    }
}