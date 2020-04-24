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
        public TabBarCustomer()
        {
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
        private static TabBarCustomer _instance;

        public static TabBarCustomer GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TabBarCustomer();
            }
            return _instance;
        }

        public void GoHome()
        {
            var tabbar = TabBarCustomer.GetInstance();
            if(tabbar.CurrentPage == tabbar.Children[0])
            {
                //System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                System.Environment.Exit(0);
            }
            tabbar.CurrentPage = tabbar.Children[0];
        }
    }
}