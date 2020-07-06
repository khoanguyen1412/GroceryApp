using Acr.UserDialogs;
using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Services;
using GroceryApp.Views.TabBars;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public interface IWriteReviewPopupViewModel
    {

    }
    public class WriteReviewPopupViewModel : BaseViewModel, IWriteReviewPopupViewModel
    {
        OrderBill order;
        private string _review;
        public string Review
        {
            get { return _review; }
            set { _review = value;
                if(!string.IsNullOrEmpty(_review)) CanNext = true; 
                else CanNext = false;
                OnPropertyChanged(nameof(Review)); }
        }
        public Color _nextColor;
        public Color NextColor
        {
            get { return _nextColor; }
            set
            {
                _nextColor = value;
                OnPropertyChanged(nameof(NextColor));
            }
        }
        private bool _canNext;
        public bool CanNext
        {
            get
            {
                return _canNext;

            }
            set
            {
                _canNext = value;
                if (_canNext) NextColor = Color.FromHex("#00cc00");
                else NextColor = Color.LightGray;
                OnPropertyChanged(nameof(CanNext));
            }
        }

        public ICommand SendReviewCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public WriteReviewPopupViewModel()
        {

        }

        public WriteReviewPopupViewModel(OrderBill order)
        {
            this.order = order;
            Review = order.Review;
            SendReviewCommand = new Command(SendReview);
            CloseCommand = new Command(Close);
        }

        public async void Close()
        {
            await PopupNavigation.PopAllAsync();
        }

        public async void SendReview()
        {
            //TEST INTERNET CONNECTTION 
            var httpClient = new HttpClient();
            try
            {
                var testInternet = await httpClient.GetStringAsync("https://newappgroc.azurewebsites.net/store/getstorebyid/test");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Action fail, check your internet connection and try again!", "OK");
                return;
            }

            using (UserDialogs.Instance.Loading("Sending.."))
            {
                order.Review = Review;
                DataUpdater.ReceiveOder(order);
                //call api update orderbill
                await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "orderbill/update", order);

                //reload list orders view
                (TabBarCustomer.GetInstance().Children.ElementAt(3).BindingContext as ListOrdersViewModel).LoadData();

                await PopupNavigation.Instance.PopAllAsync();
            }

            MessageService.Show("Received successfully", 0);
            //PUSH NOTI
            string datas = PushNotificationService.ConvertDataWrireReview(order);
            PushNotificationService.Push(NotiNumber.SendReview, datas, false);
        }
    }
}
