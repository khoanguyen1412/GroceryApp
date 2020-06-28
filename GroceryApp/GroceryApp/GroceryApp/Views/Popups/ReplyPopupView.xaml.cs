using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Services;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public void InitStars()
        {
            ReviewItem review = this.BindingContext as ReviewItem;
            star1.Source = review.star1;
            star2.Source = review.star2;
            star3.Source = review.star3;
            star4.Source = review.star4;
            star5.Source = review.star5;
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
            if (string.IsNullOrEmpty(AnswerEntry.Text))
            {
                await DisplayAlert("Error", "Your answer must not be blank!", "OK");
                return;
            }
            
            //call api update orderbill
            ReviewItem reviewItem = this.BindingContext as ReviewItem;
            DataProvider dataProvider = DataProvider.GetInstance();
            OrderBill order = dataProvider.GetOrderBillByIDOrderBill(reviewItem.IDOrderBill);
            order.StoreAnswer = AnswerEntry.Text;

            //update orderbill ở database local
            DataUpdater.UpdateStoreAnswerOrderbill(order);
            //update orderbill ở database server
            var httpClient = new HttpClient();
            await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "orderbill/update", order);
            await PopupNavigation.Instance.PopAsync();

            MessageService.Show("Send answer successfully",0);
            //PUSH NOTI
            string datas = PushNotificationService.ConvertDataAnswerFeedback(order);
            PushNotificationService.Push(NotiNumber.AnswerFeedback, datas, true);
        }
    }
}