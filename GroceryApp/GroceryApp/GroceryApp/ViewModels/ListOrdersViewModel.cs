using Acr.UserDialogs;
using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Services;
using GroceryApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
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
        DataProvider dataProvider = DataProvider.GetInstance();
        private ObservableCollection<OrderBill> _orders;

        public ObservableCollection<OrderBill> Orders
        {
            get { return _orders; }
            set { _orders = value; OnPropertyChanged(nameof(Orders)); }
        }

        private ObservableCollection<OrderBill> _waitingOrders;
        public ObservableCollection<OrderBill> WaitingOrders
        {
            get {
                return _waitingOrders;
            }
            set { _waitingOrders = value; OnPropertyChanged(nameof(WaitingOrders)); }
        }

        private ObservableCollection<OrderBill> _deliveringOrders;
        public ObservableCollection<OrderBill> DeliveringOrders
        {
            get
            {
                return _deliveringOrders;
            }
            set { _deliveringOrders = value; OnPropertyChanged(nameof(DeliveringOrders)); }
        }

        private ObservableCollection<OrderBill> _receivedOrders;
        public ObservableCollection<OrderBill> ReceivedOrders
        {
            get
            {
                return _receivedOrders;
            }
            set { _receivedOrders = value; OnPropertyChanged(nameof(ReceivedOrders)); }
        }

        public void LoadKindsOfOrders()
        {
            ObservableCollection<OrderBill> AllOrders = Orders;
            ObservableCollection<OrderBill> waitingOrders = new ObservableCollection<OrderBill>();
            foreach (OrderBill order in AllOrders)
                if (order.State == OrderState.Waiting)
                {
                    order.OrderNumber = waitingOrders.Count + 1;
                    waitingOrders.Add(order);
                }
            WaitingOrders = new ObservableCollection<OrderBill>(waitingOrders);

            ObservableCollection<OrderBill> deliveringOrders = new ObservableCollection<OrderBill>();
            foreach (OrderBill order in AllOrders)
                if (order.State == OrderState.Delivering)
                {
                    order.OrderNumber = deliveringOrders.Count + 1;
                    deliveringOrders.Add(order);
                }
            DeliveringOrders = new ObservableCollection<OrderBill>(deliveringOrders);

            ObservableCollection<OrderBill> receivedOrders = new ObservableCollection<OrderBill>();
            foreach (OrderBill order in AllOrders)
                if (order.State == OrderState.Received)
                {
                    order.OrderNumber = receivedOrders.Count + 1;
                    receivedOrders.Add(order);
                }
            ReceivedOrders = new ObservableCollection<OrderBill>(receivedOrders);
        }

        public ICommand ShowDetailOrderCommand { get; set; }
        public ICommand ShowReviewPopupCommand { get; set; }
        public ICommand DeleteOrderCommand { get; set; }

        public ListOrdersViewModel()
        {
            LoadData();
            ShowDetailOrderCommand = new Command<OrderBill>(ShowDetailOrder);
            ShowReviewPopupCommand = new Command<OrderBill>(ShowReviewPopup);
            DeleteOrderCommand = new Command<OrderBill>(DeleteOrder);

        }

        public async void DeleteOrder(OrderBill orderBill)
        {
            using (UserDialogs.Instance.Loading("Canceling order"))
            {
                var httpClient = new HttpClient();
                List<Product> productInOrder = dataProvider.GetProductsInBillByIDBill(orderBill.IDOrderBill);
                //update quantityinventory ở local
                List<Product> sourceProducts = DataUpdater.ReturnListProductToSource(productInOrder);
                //api update quantityinventory ở server
                foreach (Product product in sourceProducts)
                {
                    await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "product/update", product);
                }

                //api delete products trong order bị xóa (ở server)
                await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "product/deletebyidorderbill/" + orderBill.IDOrderBill, new { });

                //delete product ở local
                DataUpdater.DeleteProducts(productInOrder);

                //delete orderbill server
                await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "orderbill/deleteorderbillbyid/" + orderBill.IDOrderBill, new { });
                //delete orderbill local
                DataUpdater.DeleteOrderBillByID(orderBill.IDOrderBill);


                //reload ListOrdersView
                LoadData();
            }
                
            MessageService.Show("Cancel order successfully", 0);

            //PUSH NOTI
            string datas = PushNotificationService.ConvertDataCancelOrder(orderBill);
            PushNotificationService.Push(NotiNumber.CancelOrderForStore,datas, false); 
            PushNotificationService.Push(NotiNumber.CancelOrderForOther,datas, true); 
        
        }

        public async void ShowReviewPopup(OrderBill order)
        {
            var ReviewPopup = new ReviewStorePopupView();
            ReviewPopup.BindingContext = new ReviewStorePopupViewModel(order);
            await PopupNavigation.Instance.PushAsync(ReviewPopup);
        }

        public async void ShowDetailOrder(OrderBill order)
        {
            var DetalPage = new OrderDetailPopupView();
            order.Init();
            OrderBillItem orderItem = new OrderBillItem
            {
                Order = order,
                AddedProducts = dataProvider.GetProductsInBillByIDBill(order.IDOrderBill)
            };
            DetalPage.BindingContext = orderItem;
            await PopupNavigation.Instance.PushAsync(DetalPage);
        }

        public void LoadData()
        {
            var dataProvider = DataProvider.GetInstance();
            Orders = new ObservableCollection<OrderBill>(dataProvider.GetMyOrderBills());
            foreach (OrderBill order in Orders)
                order.Init();
            LoadKindsOfOrders();
        }
    }
}
