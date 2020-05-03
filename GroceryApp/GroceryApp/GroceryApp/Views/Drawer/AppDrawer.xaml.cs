using GroceryApp.Views.Screens;
using GroceryApp.Views.TabBars;
using Plugin.SharedTransitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroceryApp.Views.Drawer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppDrawer : SharedTransitionShell
    {
        FlyoutItem flyoutShopping = new FlyoutItem
        {
            Title = "Shopping",
            Icon = "heartcart",
            FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems
        };
        Tab tabShopping = new Tab
        {
            Title = "Shopping",
            Icon = "heartcart",
            FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems
        };
        FlyoutItem flyoutStore = new FlyoutItem
        {
            Title = "Your Store",
            Icon = "store",
            FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems
        };
        Tab tabStore = new Tab
        {
            Title = "Your Store",
            Icon = "store",
            FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems
        };


        private static AppDrawer _instance;

        public static AppDrawer GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AppDrawer();
            }
            return _instance;
        }
        private AppDrawer()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
            Shell.SetNavBarIsVisible(this, false);
            //Item CUSTOMER
            var shoppingTabs = new ShellContent()
            {
                Content = TabBarCustomer.GetInstance(),
                Title = "Shopping",
                Icon = "heartcart"
            };
            tabShopping.Items.Add(shoppingTabs);
            flyoutShopping.Items.Add(tabShopping);
            appShell.Items.Add(flyoutShopping);

            //Item STORE
            
            var storeTabs = new ShellContent()
            {
                Content = TabbarStoreManager.GetInstance(),
                Title = "Your store",
                Icon = "store"
            };
            tabStore.Items.Add(storeTabs);
            flyoutStore.Items.Add(tabStore);
            appShell.Items.Add(flyoutStore);
            
        }

        protected override void OnTabIndexPropertyChanged(int oldValue, int newValue)
        {
            base.OnTabIndexPropertyChanged(oldValue, newValue);
        }
        protected override bool OnBackButtonPressed()
        {
            
            int x = App.Current.MainPage.Navigation.NavigationStack.Count;
            if (x == 1)
            {
                if (appShell.CurrentItem == flyoutShopping)
                {
                    var tabbar = TabBarCustomer.GetInstance();
                    tabbar.GoHome();
                }
                else
                {
                    var tabbar = TabbarStoreManager.GetInstance();
                    tabbar.GoHome();
                }
            }
            else
            {
                base.OnBackButtonPressed();
            }

            return true;
        }
    }
}