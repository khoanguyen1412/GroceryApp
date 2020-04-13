using GroceryApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryApp.DataProviders
{
    public class DataProvider
    {
        private static DataProvider _instance;
        public static DataProvider GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataProvider();
            }
            return _instance;
        }

        public static List<Store> ListStores = new List<Store>
            {
                new Store{ID=0,StoreName="Groceriers",ImageURL="https://www.iowapublicradio.org/sites/ipr/files/201904/grocery-store-2119702_1920.jpg", StoreAddress="",StoreDescription="A lot of fresh vegetables"},
                new Store{ID=1,StoreName="Fruits Kingdom",ImageURL="https://st3.depositphotos.com/1346781/17800/i/1600/depositphotos_178006858-stock-photo-a-wide-variety-of-fresh.jpg", StoreAddress="",StoreDescription="All kind of fruits"},
                new Store{ID=2,StoreName="Candy World",ImageURL="https://bluebiz-media.azureedge.net/4a3743/contentassets/24911eb5727c4be7a1b354db9b16e9dd/bluebook_sweets_624x380.jpg", StoreAddress="",StoreDescription="Candy, fruits and more"},
                new Store{ID=3,StoreName="Funny Bakers",ImageURL="https://saohanoi.vn/wp-content/uploads/2018/10/bang-hieu-banh-kem-8.jpeg", StoreAddress="",StoreDescription="Serve Bakes and Sweet food"},
                new Store{ID=4,StoreName="Coronas",ImageURL="https://api.time.com/wp-content/uploads/2020/02/corona-beer-virus-.jpg", StoreAddress="",StoreDescription="Get viruss away"},

            };

        public static List<Product> ListProducts = new List<Product>
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
                    ImageURL="https://media.cooky.vn/images/blog-2016/cach-phan-biet-2-loai-banh-cupcake-voi-muffin-2.jpg",
                    },
            };

        public static List<OrderBill> ListOrders = new List<OrderBill>
            {
                new OrderBill{
                    ID=0,
                    IDUser=1,
                    IDStore=1,
                    Date=new DateTime(2020, 4, 3),
                    SubTotalPrice=120000,
                    DeliveryPrice=10000,
                    TotalPrice=130000,
                    CustomerAddress="25 Trần Duy Hưng, Hà Nội",
                    Note="Giao hàng từ 6->8h sáng",
                    State="WAITING",
                    Review="",
                    StoreAnswer="",
                    Rating=-1,
                    OrderedProducts=ListProducts
                },
                new OrderBill{
                    ID=1,
                    IDUser=1,
                    IDStore=1,
                    Date=new DateTime(2020, 4, 1),
                    SubTotalPrice=500000,
                    DeliveryPrice=10000,
                    TotalPrice=510000,
                    CustomerAddress="25 Trần Duy Hưng, Hà Nội",
                    Note="Giao hàng từ 6->8h sáng",
                    State="WAITING",
                    Review="",
                    StoreAnswer="",
                    Rating=-1,
                    OrderedProducts=ListProducts
                },
                new OrderBill{
                    ID=2,
                    IDUser=1,
                    IDStore=1,
                    Date=new DateTime(2020, 3, 26),
                    SubTotalPrice=48000,
                    DeliveryPrice=10000,
                    TotalPrice=58000,
                    CustomerAddress="25 Trần Duy Hưng, Hà Nội",
                    Note="Giao hàng từ 6->8h sáng",
                    State="DELIVERING",
                    Review="",
                    StoreAnswer="",
                    Rating=-1,
                    OrderedProducts=ListProducts
                },
                new OrderBill{
                    ID=3,
                    IDUser=1,
                    IDStore=1,
                    Date=new DateTime(2020, 3, 26),
                    SubTotalPrice=77000,
                    DeliveryPrice=10000,
                    TotalPrice=87000,
                    CustomerAddress="25 Trần Duy Hưng, Hà Nội",
                    Note="Giao hàng từ 6->8h sáng",
                    State="DELIVERING",
                    Review="",
                    StoreAnswer="",
                    Rating=-1,
                    OrderedProducts=ListProducts
                },
                new OrderBill{
                    ID=4,
                    IDUser=1,
                    IDStore=1,
                    Date=new DateTime(2020, 2, 24),
                    SubTotalPrice=25000,
                    DeliveryPrice=10000,
                    TotalPrice=35000,
                    CustomerAddress="25 Trần Duy Hưng, Hà Nội",
                    Note="Giao hàng từ 6->8h sáng",
                    State="DELIVERING",
                    Review="",
                    StoreAnswer="",
                    Rating=-1,
                    OrderedProducts=ListProducts
                },
                new OrderBill{
                    ID=5,
                    IDUser=1,
                    IDStore=1,
                    Date=new DateTime(2020, 1, 12),
                    SubTotalPrice=1000000,
                    DeliveryPrice=10000,
                    TotalPrice=1010000,
                    CustomerAddress="25 Trần Duy Hưng, Hà Nội",
                    Note="Giao hàng từ 6->8h sáng",
                    State="RECEIVED",
                    Review="Đồ ăn không được tươi lắm, nhưng lại khá rẻ",
                    StoreAnswer="Tiền nào của nấy thôi bạn, sorry bạn nha!",
                    Rating=2,
                    OrderedProducts=ListProducts
                },
                new OrderBill{
                    ID=6,
                    IDUser=1,
                    IDStore=1,
                    Date=new DateTime(2019, 12, 11),
                    SubTotalPrice=120000,
                    DeliveryPrice=10000,
                    TotalPrice=130000,
                    CustomerAddress="25 Trần Duy Hưng, Hà Nội",
                    Note="Giao hàng từ 6->8h sáng",
                    State="RECEIVED",
                    Review="Shop giao hàng nhanh, rất nhiệt tình tư vấn",
                    StoreAnswer="Cảm ơn bạn, chúc bạn một ngày vui vẻ",
                    Rating=4.5,
                    OrderedProducts=ListProducts
                },

            };

        public static List<ProductType> ListProductTypes = new List<ProductType>
        {
            new ProductType
            {
                ID=0,
                TypeName="Fruits",
                TypeDescription="All kind of fruits, except for cooking!",
                ImageURL="colorfruit"
            },
            new ProductType
            {
                ID=1,
                TypeName="Vegetables",
                TypeDescription="All kind of vegetables, include fruits for cooking!",
                ImageURL="greenveget"
            },
            new ProductType
            {
                ID=2,
                TypeName="Drinks",
                TypeDescription="All kind of beverages, juice, carbonated water, etc..!",
                ImageURL="colordrink"
            },
            new ProductType
            {
                ID=3,
                TypeName="Candies",
                TypeDescription="All kind of candies, lolipop, Chewing gums,..!",
                ImageURL="colorcandy"
            },
            new ProductType
            {
                ID=4,
                TypeName="Cakes",
                TypeDescription="All kind of cakes, sweety or saltine crackers,..!",
                ImageURL="colorcake"
            },
        };

        public static List<ProductType> ListProductTypesYel = new List<ProductType>
        {
            new ProductType
            {
                ID=0,
                TypeName="Fruits",
                TypeDescription="All kind of fruits, except for cooking!",
                ImageURL="yelfruit"
            },
            new ProductType
            {
                ID=1,
                TypeName="Vegets",
                TypeDescription="All kind of vegetables, include fruits for cooking!",
                ImageURL="yelveget"
            },
            new ProductType
            {
                ID=2,
                TypeName="Drinks",
                TypeDescription="All kind of beverages, juice, carbonated water, etc..!",
                ImageURL="yeldrink"
            },
            new ProductType
            {
                ID=3,
                TypeName="Candies",
                TypeDescription="All kind of candies, lolipop, Chewing gums,..!",
                ImageURL="yelcandy"
            },
            new ProductType
            {
                ID=4,
                TypeName="Cakes",
                TypeDescription="All kind of cakes, sweety or saltine crackers,..!",
                ImageURL="yelcake"
            },
        };

        public DataProvider()
        {

        }
    }
}
