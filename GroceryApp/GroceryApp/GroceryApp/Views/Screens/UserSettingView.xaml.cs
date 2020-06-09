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
    public partial class UserSettingView : ContentPage
    {
        private static UserSettingView _instance;

        public static UserSettingView GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UserSettingView();
            }
            return _instance;
        }
        private UserSettingView()
        {
            InitializeComponent();
            
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                datePicker.Focus();
            };
            tapGestureRecognizer.NumberOfTapsRequired = 1;
            dateCard.GestureRecognizers.Add(tapGestureRecognizer);
            
        }
    }
}