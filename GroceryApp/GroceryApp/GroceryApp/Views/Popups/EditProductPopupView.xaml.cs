using GroceryApp.Models;
using GroceryApp.ViewModels;
using GroceryApp.Views.TabBars;
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
    public partial class EditProductPopupView : PopupPage
    {
        public EditProductPopupView()
        {
            InitializeComponent();
        }

        private async void Save_Click(object sender, EventArgs e)
        {
            Product UpdatedProduct = (this.BindingContext as EditProductPopupViewModel).GetUpdatedProduct();
            if (UpdatedProduct == null) return;
            //(App.Current.MainPage.Navigation.NavigationStack.ElementAt(0).BindingContext as ProductManagerViewModel).UpdateProduct(UpdatedProduct);
            (TabbarStoreManager.GetInstance().Children.ElementAt(1).BindingContext as ProductManagerViewModel).UpdateProduct(UpdatedProduct);

            //TabbarStoreManager.GetInstance().Children.ElementAt(1)
            await PopupNavigation.Instance.PopAsync();
        }
    }
}