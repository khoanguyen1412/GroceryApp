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
        public UserSettingView()
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