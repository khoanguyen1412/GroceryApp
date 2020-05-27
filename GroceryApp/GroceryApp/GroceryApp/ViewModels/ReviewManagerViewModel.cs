using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public interface IReviewManagerViewModel
    {

    }
    
    public class ReviewManagerViewModel: BaseViewModel,IReviewManagerViewModel
    {
        DataProvider dataProvider = DataProvider.GetInstance();
        public ICommand ReplyCommand { get; set; }
        public ICommand ShowDetailOrderCommand { get; set; }
        public ReviewManagerViewModel()
        {
            LoadData();
            ReplyCommand = new Command<ReviewItem>(ShowConfirmInfor);
            ShowDetailOrderCommand = new Command<ReviewItem>(ShowDetailOrder);
        }

        public async void ShowConfirmInfor(ReviewItem reviewItem)
        {
            
            var replyPopup = new ReplyPopupView();
            replyPopup.BindingContext = reviewItem;
            await PopupNavigation.Instance.PushAsync(replyPopup);
        }

        public ObservableCollection<ReviewItem> _reviews;
        public ObservableCollection<ReviewItem> Reviews
        {
            get { return _reviews; }
            set { _reviews = value; OnPropertyChanged(nameof(Reviews)); }
        }

        public async void ShowDetailOrder(ReviewItem reviewItem)
        {
            var DetalPage = new OrderDetailPopupView();
            DetalPage.BindingContext = dataProvider.GetOrderBillByIDOrderBill(reviewItem.IDOrderBill);
            await PopupNavigation.Instance.PushAsync(DetalPage);
        }


        public void LoadData()
        {
            Reviews = new ObservableCollection<ReviewItem>(dataProvider.GetReviewOfMyStore());
        }
    }
}
