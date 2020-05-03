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
        public ICommand ReplyCommand { get; set; }
        public ReviewManagerViewModel()
        {
            LoadData();
            ReplyCommand = new Command<ReviewItem>(ShowConfirmInfor);
        }

        public async void ShowConfirmInfor(ReviewItem reviewItem)
        {
            
            var replyPopup = new ReplyPopupView();
            replyPopup.BindingContext = reviewItem;
            // addressPopup.Setup();
            await PopupNavigation.Instance.PushAsync(replyPopup);
        }

        private ObservableCollection<OrderBill> _orders;

        public ObservableCollection<ReviewItem> Reviews
        {
            get {
                ObservableCollection <ReviewItem> result =new ObservableCollection<ReviewItem>
                {
                    new ReviewItem
                    {
                        CustomerImage="https://znews-photo.zadn.vn/w660/Uploaded/oqivovbt/2019_01_05/jisoo_3_1.jpg",
                        CustomerName="Kim jisoo BlackPink",
                        Date="12/02/2020",
                        Content="The food is really fresh and healthy, we will order this store again, for sure!"
                    },
                    new ReviewItem
                    {
                        CustomerImage="https://i.pinimg.com/originals/7e/55/84/7e558432c10a4c64fac6b09deda5a981.jpg",
                        CustomerName="Deadpool",
                        Date="24/01/2020",
                        Content="Amazing! I can't help enjoying your kindness of serving your customers"
                    },
                    new ReviewItem
                    {
                        CustomerImage="https://gizmostory.com/wp-content/uploads/2020/01/money-heist-e1578302641839-696x391.jpg",
                        CustomerName="Good heists",
                        Date="12/12/2019",
                        Content="This store was very helpfull when we were in jail, pray for you!"
                    },
                    new ReviewItem
                    {
                        CustomerImage="https://image.nghenhinvietnam.vn/Uploaded/trunghieu/2020_01_25/photo1557109384216_1557109384435_crop_1557109391251576728816_MNWX.jpg",
                        CustomerName="Cap",
                        Date="12/02/2000",
                        Content="Between a lot of bad fast food store, I've finally found myself a good choice, thank you, hope you will get better and better everyday!"
                    },
                    new ReviewItem
                    {
                        CustomerImage="https://cdn.fado.vn/images/I/51%2BVBd0YqNL._SR600,600_.jpg",
                        CustomerName="I am Iron Man",
                        Date="12/12/2019",
                        Content="Please, save my world!"
                    },
                };
                
                return result; 
            }
        }

        
        public ObservableCollection<OrderBill> GetCompletedOrders()
        {
            ObservableCollection<OrderBill> result = new ObservableCollection<OrderBill>();
            foreach (OrderBill order in _orders)
                if (order.State == "RECEIVED")
                    result.Add(order);
            return result;
            
        }

        public void LoadData()
        {
            
        }
    }
}
