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
        }

        private void AddToChosenProducts(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
    }
}