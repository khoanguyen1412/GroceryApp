﻿using Acr.UserDialogs;
using GroceryApp.Data;
using GroceryApp.Views.Drawer;
using GroceryApp.Views.TabBars;
using ImTools;
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
    public partial class LoginTempView : ContentPage
    {
        public LoginTempView()
        {
            InitializeComponent();
            //List<string> ids = new List<string> { "1", "2" };
            //IDPicker.ItemsSource = ids;
            //IDPicker.SelectedItem = IDPicker.ItemsSource[0];
        }




    }
}