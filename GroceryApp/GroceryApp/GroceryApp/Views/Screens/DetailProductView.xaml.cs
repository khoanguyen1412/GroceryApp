using Acr.UserDialogs;
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
    public partial class DetailProductView : ContentPage
    {
        public DetailProductView()
        {
            InitializeComponent();
            TimeSpan interval = new TimeSpan(0, 0, 1);
            Device.StartTimer(interval, () => {
                UserDialogs.Instance.HideLoading();
                return true;
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UserDialogs.Instance.ShowLoading("Loading...");
        }

        

        private void AddToChosenProducts(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
    }
}