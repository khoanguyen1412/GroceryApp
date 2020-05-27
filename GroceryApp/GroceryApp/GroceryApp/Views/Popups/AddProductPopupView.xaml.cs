using GroceryApp.Models;
using GroceryApp.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroceryApp.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProductPopupView : PopupPage
    {
        public AddProductPopupView()
        {
            InitializeComponent();
            
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            Product NewProduct = (this.BindingContext as AddProductPopupViewModel).GetNewProduct();
            if (NewProduct == null) return;
            (App.Current.MainPage.Navigation.NavigationStack.ElementAt(0).BindingContext as ProductManagerViewModel).AddProduct(NewProduct);
            await PopupNavigation.Instance.PopAsync();
        }
    }
}