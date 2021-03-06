﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroceryApp.Views.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListStoresView : ContentPage
    {
        public ListStoresView()
        {
            InitializeComponent();

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                GoHome();
            };
            tapGestureRecognizer.NumberOfTapsRequired = 1;
            backLabel.GestureRecognizers.Add(tapGestureRecognizer);
        }
        public void GoHome()
        {
            var masterPage = this.Parent as TabbedPage;
            masterPage.CurrentPage = masterPage.Children[0];
        }

        protected override bool OnBackButtonPressed()
        {
            GoHome();
            return true;
        }


    }
}