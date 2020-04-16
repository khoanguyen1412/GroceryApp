using GroceryApp.Models;
using GroceryApp.ViewModels;
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
            initStuff();
        }

        public void initStuff()
        {
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

    
        private void address_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateInfor(e.NewTextValue, note.Text);
        }


        private void note_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateInfor(address.Text, e.NewTextValue);
        }

        public void UpdateInfor(string address, string note)
        {
            (this.BindingContext as OrderBill).CustomerAddress = address;
            (this.BindingContext as OrderBill).Note = note;

        }

    }
}