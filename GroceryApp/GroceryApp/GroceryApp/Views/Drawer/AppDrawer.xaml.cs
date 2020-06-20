using Acr.UserDialogs;
using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Views.Screens;
using GroceryApp.Views.TabBars;
using Plugin.SharedTransitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
        FlyoutItem flyoutUserSetting = new FlyoutItem
        {
            Title = "Setting",
            Icon = "setting",
            FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems
        };
        Tab tabSetting = new Tab
        {
            Title = "Setting",
            Icon = "setting",
            FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems
        };
        FlyoutItem flyoutLogout = new FlyoutItem
        {
            Title = "Logout",
            Icon = "colorlogout",
            FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems
        };
        Tab tabLogout = new Tab
        {
            Title = "Logout",
            Icon = "colorlogout",
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

        public static void Destroy()
        {
            _instance = null;
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
                Icon = "drawercart"
            };
            tabShopping.Items.Add(shoppingTabs);
            flyoutShopping.Items.Add(tabShopping);
            appShell.Items.Add(flyoutShopping);

            //Item STORE

            var storeTabs = new ShellContent()
            {
                Content = TabbarStoreManager.GetInstance(),
                Title = "Your store",
                Icon = "drawerstore"
            };
            tabStore.Items.Add(storeTabs);
            flyoutStore.Items.Add(tabStore);
            appShell.Items.Add(flyoutStore);

            //Item SETTING

            var settingTab = new ShellContent()
            {
                Content = UserSettingView.GetInstance(),
                Title = "Setting",
                Icon = "drawersetting"
            };
            tabSetting.Items.Add(settingTab);
            flyoutUserSetting.Items.Add(tabSetting);
            appShell.Items.Add(flyoutUserSetting);

            //Item LOGOUT

            var logoutTab = new ShellContent()
            {
                Content = new LogoutView(),
                Title = "Logout",
                Icon = "flatlogout"
            };
            tabLogout.Items.Add(logoutTab);
            flyoutLogout.Items.Add(tabLogout);
            
            appShell.Items.Add(flyoutLogout);

            //appShell.CurrentItem.PropertyChanged += CurrentItem_PropertyChanged;
            flyoutShopping.PropertyChanged += FlyoutShopping_PropertyChanged;
            flyoutStore.PropertyChanged += FlyoutStore_PropertyChanged;
            flyoutUserSetting.PropertyChanged += FlyoutUserSetting_PropertyChanged;

            DataProvider dataProvider = DataProvider.GetInstance();
            User user = dataProvider.GetUserByIDUser(Infor.IDUser);
            this.BindingContext = user;
        }

        

        private async void FlyoutUserSetting_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            using (UserDialogs.Instance.Loading("wait.."))
            {
                await Task.Delay(500);
            }
        }

        private async void FlyoutStore_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            using (UserDialogs.Instance.Loading("wait.."))
            {
                await Task.Delay(500);
            }
        }

        private async void FlyoutShopping_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            using (UserDialogs.Instance.Loading("wait.."))
            {
                await Task.Delay(500);
            }
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
                    if (appShell.CurrentItem == flyoutStore)
                    {
                        var tabbar = TabbarStoreManager.GetInstance();
                        tabbar.GoHome();
                    }
                    else
                    {
                        return true;
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