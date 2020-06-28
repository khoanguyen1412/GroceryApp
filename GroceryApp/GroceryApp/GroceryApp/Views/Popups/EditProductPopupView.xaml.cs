using GroceryApp.Models;
using GroceryApp.Services;
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

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

    }
}