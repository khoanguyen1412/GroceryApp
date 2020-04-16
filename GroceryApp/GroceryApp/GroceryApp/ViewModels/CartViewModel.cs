using GroceryApp.Models;
using GroceryApp.Views.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using GroceryApp.DataProviders;

namespace GroceryApp.ViewModels
{
    
    public interface ICartViewModel
    {

    }
    public class CartViewModel : BaseViewModel, ICartViewModel
    {
        
        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged(nameof(Products)); }
        }

        public ICommand ShowConfirmInforCommand { get; set; }
        public CartViewModel()
        {
            LoadData();
            ShowConfirmInforCommand = new Command(ShowConfirmInfor);
        }
        
        public async void ShowConfirmInfor()
        {
            List<Product> ListProducts = new List<Product>
            {
               
                new Product{
                    ID=1,
                    IDType=0,
                    IDStore=0,
                    ProductName="Potatoxxxxx",
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
                    ProductName="Cup cake black for black people",
                    ProductDescription="White scream, suit for dieting",
                    Unit="one",
                    QuantityInventory=15,
                    QuantityOrder=7,
                    Price=7000,
                    ImageURL="https://media.cooky.vn/images/blog-2016/cach-phan-biet-2-loai-banh-cupcake-voi-muffin-2.jpg",
                    },
            };
            OrderBill order = new OrderBill()
            {
                ID = 0,
                IDUser = 1,
                IDStore = 1,
                Date = new DateTime(2020, 4, 3),
                SubTotalPrice = 120000,
                DeliveryPrice = 10000,
                TotalPrice = 130000,
                CustomerAddress = "25 Trần Duy Hưng, Hà Nội",
                Note = "Giao hàng từ 6->8h sáng",
                State = "WAITING",
                Review = "",
                StoreAnswer = "",
                Rating = -1,
                OrderedProducts = ListProducts
            };
            var addressPopup = new ConfirmInforOrderPopupView();
            addressPopup.BindingContext = order;
           // addressPopup.Setup();
            await PopupNavigation.Instance.PushAsync(addressPopup);
        }

        public void LoadData()
        {
            
            _products = new ObservableCollection<Product>(DataProvider.ListProducts);
        }
    }
}
