using GroceryApp.Models;
using GroceryApp.Services;
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
    public partial class FinalBillView : ContentPage
    {
        public FinalBillView()
        {
            InitializeComponent();
            
        }

        protected override void OnDisappearing()
        {
            bool hasSent = (this.BindingContext as GroceryApp.ViewModels.FinalBillViewModel).hasSent;
            base.OnDisappearing();
            if(hasSent) MessageService.Show("Order successfully", 0);
        }
    }
}