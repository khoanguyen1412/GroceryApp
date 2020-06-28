using GroceryApp.Data;
using GroceryApp.Views.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GroceryApp.Views
{
    public class SplashPage: ContentPage
    {
        Image splashImage;
        public SplashPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            var sub = new AbsoluteLayout();
            splashImage = new Image()
            {
                Source = "checkout_100px.png",
                WidthRequest = 100,
                HeightRequest = 100
            };
            AbsoluteLayout.SetLayoutFlags(splashImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            sub.Children.Add(splashImage);
            this.BackgroundColor = Color.Magenta;
            this.Content = sub;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await splashImage.ScaleTo(1, 2000);
            await splashImage.ScaleTo(0.9, 1500, Easing.Linear);
            await splashImage.ScaleTo(150, 1200, Easing.Linear);
            await ServerDatabase.LoadDataFromServer();
            Database.LoadDataToLocal();
            Application.Current.MainPage=new LoginTempView();

        }
    }
}
