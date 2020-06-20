using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Views.TabBars;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public interface IOrderDetailManagerPopupViewModel
    {

    }
    public class OrderDetailManagerPopupViewModel : BaseViewModel, IOrderDetailManagerPopupViewModel
    {
        DataProvider dataProvider = DataProvider.GetInstance();

        public OrderBill Order
        {
            get;
            set;
        }

        public List<Product> Products
        {
            get;
            set;
        }

        //public double SubTotalPrice
        //{
        //    get { return _order.SubTotalPrice; }
        //}
        //public double DeliveryPrice
        //{
        //    get { return _order.DeliveryPrice; }
        //}
        //public double TotalPrice
        //{
        //    get { return _order.TotalPrice; }
        //}
        //public OrderState State
        //{
        //    get { return _order.State; }
        //}

        //public string Address
        //{
        //    get { return _order.CustomerAddress; }
        //}

        public ICommand DeliverCommand { get; set; }
        public ICommand CancelCommand { get; set; }


        public OrderDetailManagerPopupViewModel(OrderBillItem orderItem)
        {
            Order = orderItem.Order;
            Products = orderItem.AddedProducts;
            DeliverCommand = new Command<OrderBill>(DeliverOder);
            CancelCommand = new Command<OrderBill>(CancelOrder);

        }
        public async void DeliverOder(OrderBill order)
        {
            (TabbarStoreManager.GetInstance().Children.ElementAt(2).BindingContext as OrderManagerViewModel).DeliverOrder(order);

            await PopupNavigation.Instance.PopAsync();
        }

        public async void CancelOrder(OrderBill order)
        {
            (TabbarStoreManager.GetInstance().Children.ElementAt(2).BindingContext as OrderManagerViewModel).CancelOrder(order);
            await PopupNavigation.Instance.PopAsync();
        }

        public OrderDetailManagerPopupViewModel()
        {
            //NO USE
        }
    }
}
