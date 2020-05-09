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
    public interface IListOrdersViewModel
    {

    }
    public class ListOrdersViewModel: BaseViewModel,IListOrdersViewModel
    {
        private ObservableCollection<OrderBill> _orders;

        public ObservableCollection<OrderBill> Orders
        {
            get { return _orders; }
            set { _orders = value; OnPropertyChanged(nameof(Orders)); }
        }

        public ObservableCollection<OrderBill> WaitingOrders
        {
            get {
                ObservableCollection<OrderBill> AllOrders=_orders;
                ObservableCollection<OrderBill> waitingOrders= new ObservableCollection<OrderBill>();
                foreach (OrderBill order in AllOrders)
                    if (order.State == "WAITING")
                    {
                        order.OrderNumber = waitingOrders.Count + 1;
                        waitingOrders.Add(order);
                    }
                        
                return waitingOrders;
            }
            set {  }
        }
        public ObservableCollection<OrderBill> DeliveringOrders
        {
            get
            {
                ObservableCollection<OrderBill> AllOrders = _orders;
                ObservableCollection<OrderBill> deliveringOrders = new ObservableCollection<OrderBill>();
                foreach (OrderBill order in AllOrders)
                    if (order.State == "DELIVERING")
                    {
                        order.OrderNumber = deliveringOrders.Count + 1;
                        deliveringOrders.Add(order);
                    }
                return deliveringOrders;
            }
            set { }
        }
        public ObservableCollection<OrderBill> ReceivedOrders
        {
            get
            {
                ObservableCollection<OrderBill> AllOrders = _orders;
                ObservableCollection<OrderBill> receivedOrders = new ObservableCollection<OrderBill>();
                foreach (OrderBill order in AllOrders)
                    if (order.State == "RECEIVED")
                    {
                        order.OrderNumber = receivedOrders.Count + 1;
                        receivedOrders.Add(order);
                    }
                return receivedOrders;
            }
            set { }
        }


        public ICommand ShowDetailOrderCommand { get; set; }
        public ICommand ShowReviewPopupCommand { get; set; }
        public ListOrdersViewModel()
        {
            LoadData();
            ShowDetailOrderCommand = new Command<OrderBill>(ShowDetailOrder);
            ShowReviewPopupCommand = new Command(ShowReviewPopup);
        }

        public async void ShowReviewPopup()
        {
            var ReviewPopup = new ReviewStorePopupView();
            await PopupNavigation.Instance.PushAsync(ReviewPopup);
        }

        public async void ShowDetailOrder(OrderBill order)
        {
            var DetalPage = new OrderDetailPopupView();
            DetalPage.BindingContext = order;
            await PopupNavigation.Instance.PushAsync(DetalPage);
        }

        public void LoadData()
        {
            var dataProvider = DataProvider.GetInstance();
            _orders = new ObservableCollection<OrderBill>(dataProvider.GetOrderBills());
        }
    }
}
