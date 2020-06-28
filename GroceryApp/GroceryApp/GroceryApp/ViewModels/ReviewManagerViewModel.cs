using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Views.Popups;
using GroceryApp.Views.TabBars;
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
        public ICommand GobackCommand { get; set; }
        public ReviewManagerViewModel()
        {
            LoadData();
            ReplyCommand = new Command<ReviewItem>(ShowReviewInfor);
            ShowDetailOrderCommand = new Command<ReviewItem>(ShowDetailOrder);
            GobackCommand = new Command(Goback);
        }
        public void Goback()
        {
            var Tabbar = TabbarStoreManager.GetInstance();
            Tabbar.CurrentPage = Tabbar.Children[0];
        }

        public async void ShowReviewInfor(ReviewItem reviewItem)
        {
            
            var replyPopup = new ReplyPopupView();
            reviewItem.SetStar();
            replyPopup.BindingContext = reviewItem;
            replyPopup.InitStars();
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
            OrderBillItem orderBillItem = new OrderBillItem
            {
                Order = dataProvider.GetOrderBillByIDOrderBill(reviewItem.IDOrderBill),
                AddedProducts = dataProvider.GetProductsInBillByIDBill(reviewItem.IDOrderBill)
            };
            DetalPage.BindingContext = orderBillItem;
            await PopupNavigation.Instance.PushAsync(DetalPage);
        }


        public void LoadData()
        {
            Reviews = new ObservableCollection<ReviewItem>(dataProvider.GetReviewOfMyStore());
        }
    }
}
