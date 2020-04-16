using GroceryApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public interface IFinalBillViewModel
    {

    }
    public class FinalBillViewModel:BaseViewModel,IFinalBillViewModel
    {
        public ICommand SendOrderCommand { get; set; }
        public FinalBillViewModel()
        {
            SendOrderCommand = new Command<object>(SendOrder);
        }
        public async void SendOrder(object bindingContext)
        {
            
            var successPopup = new SuccessNotiPopupView();
            successPopup.BindingContext = bindingContext;
            await PopupNavigation.Instance.PushAsync(successPopup);
        }

    }
}
