using GroceryApp.Models;
using GroceryApp.Views.Screens;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{

    public interface IShowStoreViewModel
    {

    }

    public class FeedBack
    {
        public string CustomerReview { get; set; }
        public string StoreAnswer { get; set; }
    }

    public class ShowStoreViewModel : BaseTransitionViewModel, IShowStoreViewModel
    {

        private ObservableCollection<Product> _products;

        private ObservableCollection<OrderBill> _orderedBills;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged(nameof(Products)); }
        }

        public ObservableCollection<OrderBill> OrderBills
        {
            get { return _orderedBills; }
            set { _orderedBills = value; OnPropertyChanged(nameof(OrderBills)); }
        }

        
        public ObservableCollection<FeedBack> FeedBacks
        {
            get {
                ObservableCollection<FeedBack> feedBack = new ObservableCollection<FeedBack>();
                foreach(OrderBill order in _orderedBills)
                {
                    FeedBack newFeedBack = new FeedBack();
                    newFeedBack.CustomerReview = order.Review;
                    newFeedBack.StoreAnswer = order.StoreAnswer;
                    feedBack.Add(newFeedBack);
                }
                return feedBack; 
            }
        }

        private int _selectedProductId;
        public int SelectedProductId
        {
            get => _selectedProductId;
            set => SetProperty(ref _selectedProductId, value);
        }


        public string StoreName
        {
            get { return "Fruits Kingdom"; }
        }
        public string ImageURL
        {
            get { return "https://st3.depositphotos.com/1346781/17800/i/1600/depositphotos_178006858-stock-photo-a-wide-variety-of-fresh.jpg"; }
        }
        public string StoreDescription
        {
            get { return "All kind of fruits"; }
        }
        public string StoreAddress
        {
            get { return "192 Trần Quốc Toản, Quận 1, TPHCM"; }
        }

        public DelegateCommand<Product> ShowDetailProductCommand { get; set; }
        //public ICommand ShowDetailProductCommand { get; set; }
        public ShowStoreViewModel()
        {
            LoadData();
            ShowDetailProductCommand = new DelegateCommand<Product>(ShowDetailProduct);
        }

        public async void ShowDetailProduct(Product product)
        {
            SelectedProductId = product.ID;
            var detailPage = new DetailProductView();
            detailPage.BindingContext = product;
            await App.Current.MainPage.Navigation.PushAsync(detailPage, true);
        }

        public void LoadData()
        {
            loadProducts();
            loadOrderedBills();
        }

        public void loadProducts()
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
                    ImageURL="https://cdn.huongnghiepaau.com/wp-content/uploads/2019/01/hoc-lam-banh-cupcake.jpg",
                    },
            };


            _products = new ObservableCollection<Product>(products);
        }

        public void loadOrderedBills()
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

            List<OrderBill> orderedBills = new List<OrderBill>
            {
                new OrderBill
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
                        State = "DONE",
                        Review = "Giao hàng khá nhanh, đồ ăn rất tươi và sạch!",
                        StoreAnswer = "Cảm ơn bạn đã ủng hộ nhé",
                        Rating = -1,
                        OrderedProducts = products
                    },
                new OrderBill
                    {
                        ID = 1,
                        IDUser = 1,
                        IDStore = 1,
                        Date = new DateTime(2020, 4, 3),
                        SubTotalPrice = 120000,
                        DeliveryPrice=10000,
                        TotalPrice=130000,
                        CustomerAddress = "25 Trần Duy Hưng, Hà Nội",
                        Note = "Giao hàng từ 6->8h sáng",
                        State = "DONE",
                        Review = "Shop bán khá đắt, nhưng bù lại ngon",
                        StoreAnswer = "Tiền nào của đó bạn ê, cảm ơn bạn đã ủng hộ",
                        Rating = -1,
                        OrderedProducts = products
                    },
                new OrderBill
                    {
                        ID = 2,
                        IDUser = 1,
                        IDStore = 1,
                        Date = new DateTime(2020, 4, 3),
                        SubTotalPrice = 120000,
                        DeliveryPrice=10000,
                        TotalPrice=130000,
                        CustomerAddress = "25 Trần Duy Hưng, Hà Nội",
                        Note = "Giao hàng từ 6->8h sáng",
                        State = "DONE",
                        Review = "Mong shop sẽ có thêm nhiều sản phẩm mới",
                        StoreAnswer = "",
                        Rating = -1,
                        OrderedProducts = products
                    },

            };

            _orderedBills = new ObservableCollection<OrderBill>(orderedBills);
        }
    }
}
