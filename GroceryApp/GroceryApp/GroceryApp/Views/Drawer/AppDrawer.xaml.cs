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
        FlyoutItem flyout = new FlyoutItem
        {
            Title = "Shopping",
            Icon = "heartcart",
            FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems
        };
        Tab tab = new Tab
        {
            Title = "Shopping",
            Icon = "heartcart",
            FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems
        };


        public AppDrawer()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
            Shell.SetNavBarIsVisible(this, false);
            var shoppingTabs = new ShellContent() { 
                Content = TabBarCustomer.GetInstance(),
                Title= "Shopping",
                Icon="heartcart"
            };
            tab.Items.Add(shoppingTabs);
            flyout.Items.Add(tab);
            //group1.Items.Add(shoppingTabs);
            //MainFlyout.Items.Add(shell_section);
            appShell.Items.Add(flyout);

        }

        protected override bool OnBackButtonPressed()
        {
            
            //App.Current.MainPage.Navigation.PushAsync(new ShowStoreView(), true);
            int x = App.Current.MainPage.Navigation.NavigationStack.Count;
            if(x==1)
            {
                if(appShell.CurrentItem == flyout)
                {
                    var tabbar = TabBarCustomer.GetInstance();
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