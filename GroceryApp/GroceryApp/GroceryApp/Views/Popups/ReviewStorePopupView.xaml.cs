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
    public partial class ReviewStorePopupView : PopupPage
    {
        public ReviewStorePopupView()
        {
            InitializeComponent();
            initStuff();
        }

        public void initStuff()
        {
            ReviewPopup.HeightRequest = 400;

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

        private void TapGestureRecognizer_Tapped1(object sender, EventArgs e)
        {
            star1.Checked = true;
            star1.animate();
            star2.Checked = false;
            star3.Checked = false;
            star4.Checked = false;
            star5.Checked = false;
        }
        private void TapGestureRecognizer_Tapped2(object sender, EventArgs e)
        {
            star1.Checked = true;
            star1.animate();
            star2.Checked = true;
            star2.animate();
            star3.Checked = false;
            star4.Checked = false;
            star5.Checked = false;
        }
        private void TapGestureRecognizer_Tapped3(object sender, EventArgs e)
        {
            star1.Checked = true;
            star1.animate();
            star2.Checked = true;
            star2.animate();
            star3.Checked = true;
            star3.animate();
            star4.Checked = false;
            star5.Checked = false;
        }
        private void TapGestureRecognizer_Tapped4(object sender, EventArgs e)
        {
            star1.Checked = true;
            star1.animate();
            star2.Checked = true;
            star2.animate();
            star3.Checked = true;
            star3.animate();
            star4.Checked = true;
            star4.animate();
            star5.Checked = false;
        }
        private void TapGestureRecognizer_Tapped5(object sender, EventArgs e)
        {
            star1.Checked = true;
            star1.animate();
            star2.Checked = true;
            star2.animate();
            star3.Checked = true;
            star3.animate();
            star4.Checked = true;
            star4.animate();
            star5.Checked = true;
            star5.animate();
        }
    }
}