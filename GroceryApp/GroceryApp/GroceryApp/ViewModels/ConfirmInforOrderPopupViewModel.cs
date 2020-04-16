using GroceryApp.Models;
using GroceryApp.Views.Screens;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public interface IConfirmInforOrderPopupViewModel
    {

    }
    public class ConfirmInforOrderPopupViewModel:BaseViewModel, IConfirmInforOrderPopupViewModel
    {
        OrderBill Order = null;
        public ICommand ShowBillCommand { get; set; }
        public ConfirmInforOrderPopupViewModel()
        {
            
            ShowBillCommand = new Command<object>(ShowBill);
        }

        public ConfirmInforOrderPopupViewModel(object order)
        {
            this.Order = order as OrderBill;
            ShowBillCommand = new Command<object>(ShowBill);
        }

        public async void ShowBill(object bindingContext)
        {

            var BillView = new FinalBillView();
            OrderBill order = bindingContext as OrderBill;
            BillView.BindingContext = order;
            await PopupNavigation.Instance.PopAsync();
            await App.Current.MainPage.Navigation.PushAsync(BillView, true);
        }
    }
}
