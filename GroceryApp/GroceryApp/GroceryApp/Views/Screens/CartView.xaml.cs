using GroceryApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroceryApp.Views.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartView : ContentPage
    {
        public CartView()
        {
            InitializeComponent();
           
        }



        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var viewmodel = this.BindingContext as CartViewModel;
            viewmodel.UpdatePayment();
        }
    }
}