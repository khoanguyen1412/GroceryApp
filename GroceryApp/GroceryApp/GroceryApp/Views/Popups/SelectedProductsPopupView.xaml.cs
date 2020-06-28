using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Services;
using GroceryApp.ViewModels;
using GroceryApp.Views.Drawer;
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
    public partial class SelectedProductsPopupView : ContentView
    {
        public delegate void TapDelegate();
        public SelectedProductsPopupView()
        {
            InitializeComponent();
            var AddToCartGestureRecognizer = new TapGestureRecognizer();
            AddToCartGestureRecognizer.Tapped += AddToCart;
            btnAdd.GestureRecognizers.Add(AddToCartGestureRecognizer);

        }

        public void AddToCart(object sender, EventArgs e)
        {
            var viewmodel = this.BindingContext as ShowStoreViewModel;
            if (viewmodel.CheckNoSelectedProduct())
            {
                MessageService.Show("No product is chosen", 0);
                return;
            }
            User user = DataProvider.GetInstance().GetUserByIDUser(Infor.IDUser);
            if(!user.CheckValidInfor())
            {
                var app = AppDrawer.GetInstance();
                app.DisplayAlert("Error","Your information is not enough, please update your infor and try again!", "OK");
                return;
            }
            viewmodel.AddToCart();
            viewmodel.updateSelectedProduct();
            CollapseTapped(sender,e);
            MessageService.Show("Add to cart success", 0);
        }

        public int HeaderHeight { get; private set; } = 70;

        public TapDelegate OnExpandTapped { get; set; }
        public TapDelegate OnCollapseTapped { get; set; }


        private void GoToState(string visualState)
        {
            VisualStateManager.GoToState(BasketSummary, visualState);
            VisualStateManager.GoToState(CollapseButton, visualState);
            VisualStateManager.GoToState(ExpandButton, visualState);
        }

        private void CollapseTapped(object sender, EventArgs e)
        {
            GoToState("Collapsed");
            OnCollapseTapped?.Invoke();
        }
        private void ExpandTapped(object sender, EventArgs e)
        {
            
            GoToState("Expanded");
            OnExpandTapped?.Invoke();
            var viewmodel = this.BindingContext as ShowStoreViewModel;
            viewmodel.updateSelectedProduct();

        }
    }
}