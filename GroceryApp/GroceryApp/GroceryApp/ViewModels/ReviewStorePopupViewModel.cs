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
    public interface IReviewStorePopupViewModel
    {

    }
    public class ReviewStorePopupViewModel:BaseViewModel,IReviewStorePopupViewModel
    {

        OrderBill order;
        private string _review;
        public string Review
        {
            get { return _review; }
            set { _review = value; OnPropertyChanged(nameof(Review)); }
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
        #region stars
        
        private bool _check1;
        public bool Check1
        {
            get { return _check1; }
            set { _check1 = value; CanNext = true;
                OnPropertyChanged(nameof(Check1)); 
            }
        }
        private bool _check2;
        public bool Check2
        {
            get { return _check2; }
            set { _check2 = value; CanNext = true; OnPropertyChanged(nameof(Check2)); }
        }
        private bool _check3;
        public bool Check3
        {
            get { return _check3; }
            set { _check3 = value; CanNext = true; OnPropertyChanged(nameof(Check3)); }
        }
        private bool _check4;
        public bool Check4
        {
            get { return _check4; }
            set { _check4 = value; CanNext = true; OnPropertyChanged(nameof(Check4)); }
        }
        private bool _check5;
        public bool Check5
        {
            get { return _check5; }
            set { _check5 = value; CanNext = true; OnPropertyChanged(nameof(Check5)); }
        }
        #endregion
        public ICommand SendReviewCommand { get; set; }

        public ReviewStorePopupViewModel()
        {
            
        }

        public ReviewStorePopupViewModel(OrderBill order)
        {
            this.order = order;
            Check1 = false;
            Check2 = false;
            Check3 = false;
            Check4 = false;
            Check5 = false;
            CanNext = false;
            SendReviewCommand = new Command(SendReview);
        }

        public async void SendReview()
        {
            //TEST INTERNET CONNECTTION 
            var httpClient = new HttpClient();
            string x = "";
            try
            {
                var testInternet = await httpClient.GetStringAsync("https://newappgroc.azurewebsites.net/store/getstorebyid/test");
                x = testInternet;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Action fail, check your internet connection and try again!", "OK");
                return;
            }

            using (UserDialogs.Instance.Loading("Receiving order"))
            {
                order.Rating = GetRating();
                order.Review = Review;
                DataUpdater.ReceiveOder(order);
                order.State = OrderState.Received;
                //call api update orderbill
                await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "orderbill/update", order);

                //reload list orders view
                (TabBarCustomer.GetInstance().Children.ElementAt(3).BindingContext as ListOrdersViewModel).LoadData();

                await PopupNavigation.Instance.PopAllAsync();
            }
                
            MessageService.Show("Received successfully", 0);
            //PUSH NOTI
            string datas = PushNotificationService.ConvertDataReceiveOrder(order);
            PushNotificationService.Push(NotiNumber.ReceiveOrderForStore,datas, false);
            PushNotificationService.Push(NotiNumber.ReceiveOrderForOther, datas, true);
        }

        public int GetRating()
        {
            int result=1;
            if (Check5) result= 5;
            if (Check4) result = 4;
            if (Check3) result = 3;
            if (Check2) result = 2;
            return result ;

        }

    }
}
