using GroceryApp.Models;
using GroceryApp.Views.Drawer;
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
    public interface IStoreDashBoardViewModel
    {

    }
    public class StoreDashBoardViewModel : BaseViewModel, IStoreDashBoardViewModel
    {

        public List<int> Sliders
        {
            get
            {
                return new List<int>() { 0, 1 };
            }
        }
        public ObservableCollection<ReviewItem> Reviews
        {
            get
            {
                ObservableCollection<ReviewItem> result = new ObservableCollection<ReviewItem>
                {
                    new ReviewItem
                    {
                        CustomerImage="https://znews-photo.zadn.vn/w660/Uploaded/oqivovbt/2019_01_05/jisoo_3_1.jpg",
                        CustomerName="Kim jisoo BlackPink",
                        Date="12/02/2020",
                        Content="The food is really fresh and healthy, we will order this store again, for sure!"
                    },
                    new ReviewItem
                    {
                        CustomerImage="https://i.pinimg.com/originals/7e/55/84/7e558432c10a4c64fac6b09deda5a981.jpg",
                        CustomerName="Deadpool",
                        Date="24/01/2020",
                        Content="Amazing! I can't help enjoying your kindness of serving your customers"
                    },
                    new ReviewItem
                    {
                        CustomerImage="https://gizmostory.com/wp-content/uploads/2020/01/money-heist-e1578302641839-696x391.jpg",
                        CustomerName="Good heists",
                        Date="12/12/2019",
                        Content="This store was very helpfull when we were in jail, pray for you!"
                    },
                    new ReviewItem
                    {
                        CustomerImage="https://image.nghenhinvietnam.vn/Uploaded/trunghieu/2020_01_25/photo1557109384216_1557109384435_crop_1557109391251576728816_MNWX.jpg",
                        CustomerName="Cap",
                        Date="12/02/2000",
                        Content="Between a lot of bad fast food store, I've finally found myself a good choice, thank you, hope you will get better and better everyday!"
                    },
                    new ReviewItem
                    {
                        CustomerImage="https://cdn.fado.vn/images/I/51%2BVBd0YqNL._SR600,600_.jpg",
                        CustomerName="I am Iron Man",
                        Date="12/12/2019",
                        Content="Please, save my world!"
                    },
                };

                return result;
            }
        }
        public List<CategoryItem> Categories
        {
            get
            {
                List<CategoryItem> categories = new List<CategoryItem>
                {
                    new CategoryItem
                    {
                        Title="Manage Products",
                        Description="22 out-of-stocks",
                        Action="View products",
                        Image="colorproduct",
                    },
                    new CategoryItem
                    {
                        Title="Handle Orders",
                        Description="10 new orders",
                        Action="View orders",
                        Image="colororder",
                    },
                    new CategoryItem
                    {
                        Title="Feedback",
                        Description="12 new feedbacks",
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
    }
}
