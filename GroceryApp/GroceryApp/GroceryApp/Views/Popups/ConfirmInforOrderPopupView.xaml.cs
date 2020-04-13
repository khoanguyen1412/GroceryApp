using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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

            var tapGestureRecognizer1 = new TapGestureRecognizer();//background
            tapGestureRecognizer1.Tapped += (s, e) => {
                ClosePopup();
            };
            tapGestureRecognizer1.NumberOfTapsRequired = 1;
            background.GestureRecognizers.Add(tapGestureRecognizer1);

            var tapGestureRecognizer2 = new TapGestureRecognizer();//popup
            tapGestureRecognizer2.Tapped += (s, e) => {
                //ClosePopup();
            };
            tapGestureRecognizer2.NumberOfTapsRequired = 1;
            card.GestureRecognizers.Add(tapGestureRecognizer2);
        }

        public async void ClosePopup()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        /*
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            var maxHeight = 600;
            var maxWidth = Application.Current.MainPage.Width-20;
            base.LayoutChildren(x + (width - maxWidth) / 2, y + (height - maxHeight) / 2, maxWidth, maxHeight);
        }
        */
        protected override void OnAppearing()
        {
            base.OnAppearing();
            address.Focused += InputFocused;
            note.Focused += InputFocused;
            address.Unfocused += InputUnfocused;
            note.Unfocused += InputUnfocused;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            address.Focused -= InputFocused;
            note.Focused -= InputFocused;
            address.Unfocused -= InputUnfocused;
            note.Unfocused -= InputUnfocused;
        }
        void InputFocused(object sender, EventArgs args)
        {
            Content.LayoutTo(new Rectangle(0, -360, Content.Bounds.Width, Content.Bounds.Height));
        }

        void InputUnfocused(object sender, EventArgs args)
        {
            Content.LayoutTo(new Rectangle(0, 0, Content.Bounds.Width, Content.Bounds.Height));
        }
    }
}