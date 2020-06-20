using Acr.UserDialogs;
using GroceryApp.Data;
using GroceryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace GroceryApp.Views.TabBars
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabBarCustomer : Xamarin.Forms.TabbedPage
    {
        protected override void OnAppearing()
        {
        
            base.OnAppearing();
        }
        public TabBarCustomer()
        {
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            this.On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false);
        }
        private static TabBarCustomer _instance;

        public static TabBarCustomer GetInstance()
        {
            if (_instance == null)
            {
                User user = DataProvider.GetInstance().GetUserByIDUser(Infor.IDUser);
                _instance = new TabBarCustomer();
            }
            return _instance;
        }

        public static void Destroy()
        {
            _instance = null;
        }

        public void GoHome()
        {
            var tabbar = TabBarCustomer.GetInstance();
            if(tabbar.CurrentPage == tabbar.Children[0])
            {
                //System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                //System.Environment.Exit(0);
            }
            tabbar.CurrentPage = tabbar.Children[0];
        }
    }
}