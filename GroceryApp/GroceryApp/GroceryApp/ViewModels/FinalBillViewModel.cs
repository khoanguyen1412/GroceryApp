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
    public interface IFinalBillViewModel
    {

    }
    public class FinalBillViewModel:BaseViewModel,IFinalBillViewModel
    {
        public OrderBill Order { get; set; }
        public ICommand SendOrderCommand { get; set; }
        
        public string StoreName
        {
            get
            {
                DataProvider dataProvider = DataProvider.GetInstance();
                return dataProvider.GetStoreByIDStore(Order.IDStore).StoreName;
            }
        }

        public string StoreAddress
        {
            get
            {
                DataProvider dataProvider = DataProvider.GetInstance();
                return dataProvider.GetStoreByIDStore(Order.IDStore).StoreAddress;
            }
        }

        public ObservableCollection<Product> OrderedProducts
        {
            get
            {
                return new ObservableCollection<Product>(this.Order.OrderedProducts);
            }
        }

        public FinalBillViewModel(OrderBill order)
        {
            this.Order = order;
            SendOrderCommand = new Command<object>(SendOrder);
        }
        public async void SendOrder(object bindingContext)
        {
            
            var successPopup = new SuccessNotiPopupView();
            successPopup.BindingContext = bindingContext;
            await PopupNavigation.Instance.PushAsync(successPopup);
        }

        public FinalBillViewModel()
        {
            //NO USE
            //SendOrderCommand = new Command<object>(SendOrder);
        }

    }
}
