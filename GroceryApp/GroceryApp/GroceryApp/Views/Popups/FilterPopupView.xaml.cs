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
    public partial class FilterPopupView : PopupPage
    {
        private static FilterPopupView _instance;

        public static FilterPopupView GetInstance()
        {
            if (_instance == null)
            {
                _instance = new FilterPopupView();
            }
            return _instance;
        }
        private FilterPopupView()
        {
            InitializeComponent();
        }
    }
}