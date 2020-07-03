using GroceryApp.Data;
using GroceryApp.Models;
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
    public partial class TestParallax : ContentPage
    {
        
        public TestParallax()
        {
            InitializeComponent();
            this.BindingContext = new User
            {
                ImageURL = "https://bom.to/2ggnay",
                Email = "khoanguyen1412v@gmail.com",
                UserName = "Khoa athony",
                IDStore = "defaultstore"
            };
        }

    }
}