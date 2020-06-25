using Acr.UserDialogs;
using GroceryApp.Data;
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
    public partial class LoginView : ContentPage
    {
        private static LoginView _instance;

        public static LoginView GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LoginView();
            }
            return _instance;
        }
        private LoginView()
        {
            InitializeComponent();
        }

       
    }
}