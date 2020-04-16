using GroceryApp.DataProviders;
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

    public interface IOrderDetailPopupViewModel
    {

    }
    public class OrderDetailPopupViewModel : BaseViewModel, IOrderDetailPopupViewModel
    {

        private OrderBill _order;

        public OrderBill Order
        {
            get { return _order; }
            set { _order = value; OnPropertyChanged(nameof(Order)); }
        }

        public List<Product> Products
        {
            get { return _order.OrderedProducts; }
        }

        public double SubTotalPrice
        {
            get { return _order.SubTotalPrice; }
        }
        public double DeliveryPrice
        {
            get { return _order.DeliveryPrice; }
        }
        public double TotalPrice
        {
            get { return _order.TotalPrice; }
        }
        public string State
        {
            get { return _order.State; }
        }
        public string State1
        {
            get { return "WAITING"; }
        }
        public string State2
        {
            get { return "DELIVERING"; }
        }
        public string State3
        {
            get { return "RECEIVED"; }
        }

        public ICommand ShowReviewPopupCommand { get; set; }
        public OrderDetailPopupViewModel()
        {
            LoadData();
            ShowReviewPopupCommand = new Command(ShowReviewPopup);
        }
        public async void ShowReviewPopup()
        {
            
            var ReviewPopup = new ReviewStorePopupView();
            await PopupNavigation.Instance.PushAsync(ReviewPopup);
        }


        public void LoadData()
        {
            List<Product> products = new List<Product>
            {
                new Product{
                    ID=0,
                    IDType=0,
                    IDStore=0,
                    ProductName="Carrot",
                    ProductDescription="Fresh and Big Carrots from US Farms",
                    Unit="one",
                    QuantityInventory=15,
                    QuantityOrder=4,
                    Price=2000,
                    ImageURL="https://www.jessicagavin.com/wp-content/uploads/2019/02/carrots-7-1200.jpg",
                    },
                new Product{
                    ID=1,
                    IDType=0,
                    IDStore=0,
                    ProductName="Potato",
                    ProductDescription="Juicy potatos from West US",
                    Unit="one",
                    QuantityInventory=23,
                    QuantityOrder=3,
                    Price=7000,
                    ImageURL="https://www.asianscientist.com/wp-content/uploads/bfi_thumb/20180719-potatoes-vegetables-pexels-36ls0syth5iutrozsmneo0.jpeg",
                    },
                new Product{
                    ID=2,
                    IDType=1,
                    IDStore=1,
                    ProductName="Orange",
                    ProductDescription="Small juicy Oranges with no seed",
                    Unit="one",
                    QuantityInventory=45,
                    QuantityOrder=10,
                    Price=5000,
                    ImageURL="https://www.irishtimes.com/polopoly_fs/1.3923226.1560339148!/image/image.jpg_gen/derivatives/ratio_1x1_w1200/image.jpg",
                    },
                new Product{
                    ID=3,
                    IDType=2,
                    IDStore=2,
                    ProductName="Lolly Pop",
                    ProductDescription="No harmful toxic and 100% from milk",
                    Unit="one",
                    QuantityInventory=100,
                    QuantityOrder=15,
                    Price=500,
                    ImageURL="https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcTnSWbkRZa817h-8I2OfbmyS3AeStVjy2dhf_j5F9xae5tdan9-&usqp=CAU",
                    },
                new Product{
                    ID=4,
                    IDType=3,
                    IDStore=3,
                    ProductName="Lime water",
                    ProductDescription="Natural lemons and no fat sugar",
                    Unit="one",
                    QuantityInventory=50,
                    QuantityOrder=6,
                    Price=15000,
                    ImageURL="https://www.7sky.life/sys/wp-content/uploads/lemon-water.jpg",
                    },
                new Product{
                    ID=5,
                    IDType=4,
                    IDStore=4,
                    ProductName="Cup cake",
                    ProductDescription="White scream, suit for dieting",
                    Unit="one",
                    QuantityInventory=15,
                    QuantityOrder=7,
                    Price=7000,
                    ImageURL="https://lh3.googleusercontent.com/proxy/oJusGH8ZmImrmCKbDfygtl6zm-gTMrOCivuqCgQDeH5du0ZvJ_ViKQmpCWL8-0OL8G6Ttgzrwfib3ZHpupj2rTU8XWTFpC2knVIkwn8ug_3n2ZY1b5RF27sA8JuXFQebbAwf6Q",
                    },
            };

            OrderBill order = new OrderBill
            {
                ID = 0,
                IDUser = 1,
                IDStore = 1,
                Date = new DateTime(2020, 4, 3),
                SubTotalPrice = 120000,
                DeliveryPrice=10000,
                TotalPrice=130000,
                CustomerAddress = "25 Trần Duy Hưng, Hà Nội",
                Note = "Giao hàng từ 6->8h sáng",
                State = "WAITING",
                Review = "",
                StoreAnswer = "",
                Rating = -1,
                OrderedProducts = products
            };

            _order = DataProvider.ListOrders[0];
        }
    }
}
