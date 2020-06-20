using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Views.Drawer;
using ImTools;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public class CategoryItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Action { get; set; }
        public string Image { get; set; }
    }

    public class SimpleFeedBackItem
    {
        public string UserName { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public string UserImage { get; set; }
    }

    public interface IStoreDashBoardViewModel
    {

    }
    public class StoreDashBoardViewModel : BaseViewModel, IStoreDashBoardViewModel
    {
        public int NumberOfOrder
        {
            get
            {
                
                DataProvider dataProvider = DataProvider.GetInstance();
                return dataProvider.GetOrderBillByIDStore(Infor.IDStore).Count;

            }
        }

        public int NumberOfProduct
        {
            get
            {
                DataProvider dataProvider = DataProvider.GetInstance();
                return dataProvider.GetProductByIDStore(Infor.IDStore).Count;

            }
        }

        public string Renenue
        {
            get
            {
                DataProvider dataProvider = DataProvider.GetInstance();
                List<OrderBill> orders= dataProvider.GetOrderBillByIDStore(Infor.IDStore);
                double total = 0;
                foreach (OrderBill order in orders)
                    if(order.State==OrderState.Received)
                    {
                        List<Product> orderedProducts = dataProvider.GetProductsInBillByIDBill(order.IDOrderBill);
                        total += order.GetTotal();
                    }
                    


                return total.ToString();
            }
        }

        public List<int> Sliders
        {
            get
            {
                return new List<int>() { 0, 1};
            }
        }
        public ObservableCollection<ReviewItem> Reviews
        {
            get
            {
                ObservableCollection<ReviewItem> result = new ObservableCollection<ReviewItem>();
                DataProvider dataProvider = DataProvider.GetInstance();
                List<OrderBill> orders = dataProvider.GetMyReviewedOrder();
                orders = SortByDate(orders);

                for(int i = 0; i < orders.Count; i++)
                {
                    if (i == 5) break;
                    User user = dataProvider.GetUserByIDUser(orders[i].IDUser);
                    ReviewItem item = new ReviewItem
                    {
                        CustomerImage = user.ImageURL,
                        CustomerName = user.UserName,
                        Content = orders[i].Review,
                        Date = orders[i].Date,
                        Rating = orders[i].Rating
                    };
                    item = CreateStar(item);
                    result.Add(item);
                }

                return result;
            }
        }
        public List<CategoryItem> Categories
        {
            get
            {
                DataProvider dataProvider = DataProvider.GetInstance();

                string label1 = dataProvider.GetNumberProductOutOfStock().ToString();
                string label2 = dataProvider.GetNumberNewOrder().ToString();
                string label3 = dataProvider.GetNumberNewFeedback().ToString();
                
                List<CategoryItem> categories = new List<CategoryItem>
                {
                    new CategoryItem
                    {
                        Title="Manage Products",
                        Description=label1+" out-of-stocks",
                        Action="View products",
                        Image="colorproduct",
                    },
                    new CategoryItem
                    {
                        Title="Handle Orders",
                        Description=label2+" new orders",
                        Action="View orders",
                        Image="colororder",
                    },
                    new CategoryItem
                    {
                        Title="Feedback",
                        Description=label3+" new feedbacks",
                        Action="View feedback",
                        Image="colorfeedback",
                    },
                    new CategoryItem
                    {
                        Title="Build store",
                        Description="Improve business",
                        Action="Go to",
                        Image="colortool",
                    },
                };

                return categories;
            }
        }

        private Chart _chart;
        public Chart Chart
        {
            get
            {
                return _chart;
            }
        }

        public ICommand ShowDrawerCommand { get; set; }
        public StoreDashBoardViewModel()
        {
            CreateChartData();
            ShowDrawerCommand = new Command(ShowDrawer);
        }
        public void ShowDrawer()
        {
            var appDrawer = AppDrawer.GetInstance();
            appDrawer.FlyoutIsPresented = true;
        }


        public void CreateChartData()
        {

            List<SkiaSharp.SKColor> colors = new List<SKColor>
            {
                SKColor.Parse("#ff3333"),
                SKColor.Parse("#009933"),
                SKColor.Parse("#0000ff"),
                SKColor.Parse("#ff66cc"),
                SKColor.Parse("#ffff1a"),
                SKColor.Parse("#00ffff"),
            };
            List<string> MonthName = new List<string>
            {
                "Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"
            };

            DataProvider dataProvider = DataProvider.GetInstance();
            List<ItemChart> itemCharts = dataProvider.GetDataForChart();

            var entries = new List<Microcharts.Entry>();
            for(int i=0;i<itemCharts.Count;i++)
            {
                ItemChart item = itemCharts[i];

                Microcharts.Entry entry = new Microcharts.Entry(item.NumberOrder)
                {
                    Label = MonthName[item.Month-1],
                    ValueLabel = item.NumberOrder.ToString(),
                    Color = colors[i]
                };
                entries.Add(entry);
            }

            /*
            var entries = new[]
            {
                new Microcharts.Entry(2)
                {
                    Label = "January",
                    ValueLabel = "2",
                    Color = SKColor.Parse("#ff3333")
                },
                new Microcharts.Entry(15)
                {
                    Label = "February",
                    ValueLabel = "15",
                    Color = SKColor.Parse("#009933"),
                    
                },
                new Microcharts.Entry(10)
                {
                    Label = "March",
                    ValueLabel = "10",
                    Color = SKColor.Parse("#0000ff")
                }
            };
            */
            _chart = new LineChart() { 
                Entries = entries,
                LabelTextSize=40,
                LineSize=5,
                PointSize=20,
                LineMode=LineMode.Straight,
                Margin=20,
                PointMode=PointMode.Square,
                PointAreaAlpha=5
            };

        }

        public List<OrderBill> SortByDate(List<OrderBill> orders)
        {
            for(int i=0;i<orders.Count-1;i++)
                for(int j=i+1;j<orders.Count;j++)
                    if(orders[i].Date<orders[j].Date)
                    {
                        OrderBill temp = orders[i];
                        orders[i] = orders[j];
                        orders[j] = temp;
                    }

            return orders;
        }

        public ReviewItem CreateStar(ReviewItem item)
        {
            if (item.Rating == 0)
            {
                item.star1 = "emptystar";
                item.star2 = "emptystar";
                item.star3 = "emptystar";
                item.star4 = "emptystar";
                item.star5 = "emptystar";
            }
            if (item.Rating == 1)
            {
                item.star1 = "fullstar";
                item.star2 = "emptystar";
                item.star3 = "emptystar";
                item.star4 = "emptystar";
                item.star5 = "emptystar";
            }
            if (item.Rating == 2)
            {
                item.star1 = "fullstar";
                item.star2 = "fullstar";
                item.star3 = "emptystar";
                item.star4 = "emptystar";
                item.star5 = "emptystar";
            }
            if (item.Rating == 3)
            {
                item.star1 = "fullstar";
                item.star2 = "fullstar";
                item.star3 = "fullstar";
                item.star4 = "emptystar";
                item.star5 = "emptystar";
            }
            if (item.Rating == 4)
            {
                item.star1 = "fullstar";
                item.star2 = "fullstar";
                item.star3 = "fullstar";
                item.star4 = "fullstar";
                item.star5 = "emptystar";
            }
            if (item.Rating == 5)
            {
                item.star1 = "fullstar";
                item.star2 = "fullstar";
                item.star3 = "fullstar";
                item.star4 = "fullstar";
                item.star5 = "fullstar";
            }
            return item;
        }
    }
}
