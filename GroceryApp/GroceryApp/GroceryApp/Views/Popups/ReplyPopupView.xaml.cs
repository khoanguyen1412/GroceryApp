﻿using Rg.Plugins.Popup.Pages;
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
    public partial class ReplyPopupView : PopupPage
    {
        int countChange = 0;
        string sourceText;
        public ReplyPopupView()
        {
            InitializeComponent();
            countChange = 0;
            initStuff();
        }

        public void initStuff()
        {

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

        private async void CancelClick(object sender, EventArgs e)
        {
            if(countChange>1) 
                AnswerEntry.Text = sourceText;
            await PopupNavigation.Instance.PopAsync();
        }

        private void CustomEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            countChange++;
            if (countChange==2)
            {
                sourceText = e.OldTextValue;
            }

        }

        private async void SendClick(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}