using GroceryApp.ViewModels;
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
        }

        private void SearchBar_Focused(object sender, FocusEventArgs e)
        {
            listsearch.IsVisible = true;
        }

        private void SearchBar_Unfocused(object sender, FocusEventArgs e)
        {
            if((this.BindingContext as HomePageViewModel).isChoosing)
            {
                (this.BindingContext as HomePageViewModel).isChoosing = false;
                return;
            }
            listsearch.IsVisible = false;
        }

       
    }
}