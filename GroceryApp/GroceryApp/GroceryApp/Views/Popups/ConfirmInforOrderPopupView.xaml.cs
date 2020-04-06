using Rg.Plugins.Popup.Pages;
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
    public partial class ConfirmInforOrderPopupView : PopupPage
    {
        public ConfirmInforOrderPopupView()
        {
            InitializeComponent();
            ConfirmOrder.HeightRequest = 400;
            
        }
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            var maxHeight = 600;
            var maxWidth = Application.Current.MainPage.Width-20;
            base.LayoutChildren(x + (width - maxWidth) / 2, y + (height - maxHeight) / 2, maxWidth, maxHeight);
        }
    }
}