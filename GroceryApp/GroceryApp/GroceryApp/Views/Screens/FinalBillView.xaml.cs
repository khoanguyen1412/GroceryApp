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
            base.OnDisappearing();
            MessageService.Show("Order successfully", 0);
        }
    }
}