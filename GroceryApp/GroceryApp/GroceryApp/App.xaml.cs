﻿using GroceryApp.Views.Screens;
using GroceryApp.Views.TabBars;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroceryApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TabBarCustomer());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}