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
    public partial class SuccessNotiPopupView : PopupPage
    {
        public SuccessNotiPopupView()
        {
            InitializeComponent();
            initStuff();
            initLoader();
        }

        public async void initLoader()
        {
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            load.Source = "success";
            await Task.Delay(1700);
            await PopupNavigation.Instance.PopAsync();
            await App.Current.MainPage.Navigation.PopAsync();
        }

        public void initStuff()
        {
            NotiPopup.HeightRequest = 400;

            var tapGestureRecognizer1 = new TapGestureRecognizer();//background
            tapGestureRecognizer1.Tapped += (s, e) => {
                ClosePopup();
            };
            tapGestureRecognizer1.NumberOfTapsRequired = 1;
            //background.GestureRecognizers.Add(tapGestureRecognizer1);

            var tapGestureRecognizer2 = new TapGestureRecognizer();//popup
            tapGestureRecognizer2.Tapped += (s, e) => {
                //ClosePopup();
            };
            tapGestureRecognizer2.NumberOfTapsRequired = 1;
            //card.GestureRecognizers.Add(tapGestureRecognizer2);
        }

        public async void ClosePopup()
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}