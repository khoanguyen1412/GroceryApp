using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
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
        public ICommand SendReviewCommand { get; set; }

        public ReviewStorePopupViewModel()
        {
            SendReviewCommand = new Command(SendReview);
        }

        public async void SendReview()
        {
           
            await PopupNavigation.Instance.PopAllAsync();
        }

    }
}
