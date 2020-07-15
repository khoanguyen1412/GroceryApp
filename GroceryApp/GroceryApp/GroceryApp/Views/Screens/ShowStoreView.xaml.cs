using Acr.UserDialogs;
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
    public partial class ShowStoreView : ContentPage
    {
        const uint ExpandAnimationSpeed = 350;
        const uint CollapseAnimationSpeed = 250;
        double pageHeight = 0;

        private static ShowStoreView _instance;

        public static ShowStoreView GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ShowStoreView();
            }
            return _instance;
        }
        public static void Destroy()
        {
            _instance = null;
        }
        private ShowStoreView()
        {
            InitializeComponent();
            TimeSpan interval = new TimeSpan(0, 0, 2);
            Device.StartTimer(interval, () => {
                UserDialogs.Instance.HideLoading();
                return true;
            });
        }

        protected override void OnAppearing()
        {
            CartPopup.OnExpandTapped += OnExpand;
            CartPopup.OnCollapseTapped += OnCollapse;
            base.OnAppearing();
            UserDialogs.Instance.ShowLoading("Loading...");
        }

        
        protected override void OnSizeAllocated(double width, double height)
        {
            pageHeight = height;
            CartPopup.TranslationY = pageHeight - CartPopup.HeaderHeight;
            base.OnSizeAllocated(width, height);
        }

        protected override void OnDisappearing()
        {
            CartPopup.OnExpandTapped -= OnExpand;
            CartPopup.OnCollapseTapped -= OnCollapse;
            base.OnDisappearing();
        }

        private void OnExpand()
        {
            CartPopupFade.IsVisible = true;
            CartPopupFade.FadeTo(1, ExpandAnimationSpeed, Easing.SinInOut);

            var height = pageHeight - CartPopup.HeaderHeight;
            CartPopup.TranslateTo(0, Height - height, ExpandAnimationSpeed, Easing.SinInOut);
        }

        private void OnCollapse()
        {


            CartPopup.TranslateTo(0, pageHeight - CartPopup.HeaderHeight, CollapseAnimationSpeed, Easing.SinInOut);
            CartPopupFade.FadeTo(0, CollapseAnimationSpeed, Easing.SinInOut);
            CartPopupFade.IsVisible = false;
        }
    }
}