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
    public partial class HomePageView : ContentPage
    {
        public HomePageView()
        {
            InitializeComponent();
            AddEvent();
            //Application.Current.MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.White);
            //Window.SetStatusBarColor(Android.Graphics.Color.White);
        }

        public void AddEvent()
        {
            var ShowCartGestureRecognizer = new TapGestureRecognizer();
            ShowCartGestureRecognizer.Tapped += (s, e) => {
                var masterPage = this.Parent as TabbedPage;
                masterPage.CurrentPage = masterPage.Children[2];
            };
            CartLabel.GestureRecognizers.Add(ShowCartGestureRecognizer);
            CartField.GestureRecognizers.Add(ShowCartGestureRecognizer);

            var ShowOrderGestureRecognizer = new TapGestureRecognizer();
            ShowOrderGestureRecognizer.Tapped += (s, e) => {
                var masterPage = this.Parent as TabbedPage;
                masterPage.CurrentPage = masterPage.Children[3];
            };
            OrderField.GestureRecognizers.Add(ShowOrderGestureRecognizer);

            var ShowStoresGestureRecognizer = new TapGestureRecognizer();
            ShowStoresGestureRecognizer.Tapped += (s, e) => {
                var masterPage = this.Parent as TabbedPage;
                masterPage.CurrentPage = masterPage.Children[1];
            };
            ShowAllStoreLabel.GestureRecognizers.Add(ShowStoresGestureRecognizer);
        }
    }
}