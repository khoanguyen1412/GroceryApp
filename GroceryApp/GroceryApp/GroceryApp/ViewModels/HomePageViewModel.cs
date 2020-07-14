using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Views.Drawer;
using GroceryApp.Views.Screens;
using GroceryApp.Views.TabBars;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public interface IHomePageViewModel
    {

    }
    public class HomePageViewModel: BaseViewModel, IHomePageViewModel
    {
        public bool isChoosing = false;
        DataProvider dataProvider = DataProvider.GetInstance();
        private ObservableCollection<Store> _stores;

        public ObservableCollection<Store> Stores
        {
            get { return _stores; }
            set { _stores = value; OnPropertyChanged(nameof(Stores)); }
        }

        private ObservableCollection<SearchItemHome> SourceSearchItems;
        private ObservableCollection<SearchItemHome> _searchList;

        public ObservableCollection<SearchItemHome> SearchList
        {
            get { return _searchList; }
            set { _searchList = value; OnPropertyChanged(nameof(SearchList)); }
        }

        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set { 
                _searchText = value;
                OnSearch(value);
                OnPropertyChanged(nameof(SearchText)); 
            }
        }

        private bool _showList;

        public bool ShowList
        {
            get { return _showList; }
            set { _showList = value; OnPropertyChanged(nameof(ShowList)); }
        }

        public string Title { get; set; }
        public ICommand ShowDrawerCommand { get; set; }
        public ICommand GoCartCommand { get; set; }
        public ICommand GoOrderCommand { get; set; }
        public ICommand GoStoreCommand { get; set; }
        public ICommand FruitCommand { get; set; }
        public ICommand VegetCommand { get; set; }
        public ICommand DrinkCommand { get; set; }
        public ICommand CandyCommand { get; set; }
        public ICommand CakeCommand { get; set; }
        public ICommand ShowStoreCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public HomePageViewModel()
        {
            LoadData();
            ShowDrawerCommand = new Command(ShowDrawer);
            FruitCommand = new Command(ChooseFruit);
            VegetCommand = new Command(ChooseVeget);
            DrinkCommand = new Command(ChooseDrink);
            CandyCommand = new Command(ChooseCandy);
            CakeCommand = new Command(ChooseCake);
            GoCartCommand = new Command(GoCart);
            GoOrderCommand = new Command(GoOrder);
            GoStoreCommand = new Command(GoStore);
            ShowStoreCommand = new Command<Store>(ShowStore);
            SearchCommand = new Command<SearchItemHome>(Search);
        }

        public void Search(SearchItemHome searchItem)
        {
            isChoosing = false;
            ShowList = false;
            
            switch (searchItem.Name)
            {
                case "Cart": 
                    GoCart();
                    return;
                case "Orders":
                    GoOrder();
                    return;
                case "Stores":
                    GoStore();
                    return;
                case "Fruits":
                    ChooseFruit();
                    return;
                case "Vegetables":
                    ChooseVeget();
                    return;
                case "Drinks":
                    ChooseDrink();
                    return;
                case "Candies":
                    ChooseCandy();
                    return;
                case "Cakes":
                    ChooseCake();
                    return;
                case "Your Store":
                    GoStoreManager();
                    return;
            }
        }

        public void GoStoreManager()
        {

            var tabbar = AppDrawer.GetInstance();
            tabbar.CurrentItem = tabbar.Items[1];
        }

        public async void ShowStore(Store store)
        {
            ShowStoreView.Destroy();
            var showStoreView = ShowStoreView.GetInstance();
            showStoreView.BindingContext = new ShowStoreViewModel(store.IDStore);
            await App.Current.MainPage.Navigation.PushAsync(showStoreView, true);
        }

        public void GoCart()
        {
            var Tabbar = TabBarCustomer.GetInstance();
            Tabbar.CurrentPage = Tabbar.Children[2];
        }
        public void GoOrder()
        {
            var Tabbar = TabBarCustomer.GetInstance();
            Tabbar.CurrentPage = Tabbar.Children[3];
        }
        public void GoStore()
        {
            var Tabbar = TabBarCustomer.GetInstance();
            Tabbar.CurrentPage = Tabbar.Children[1];
        }

        public void ChooseCake()
        {
            TypeItem typeItem = new TypeItem
            {
                productType = dataProvider.GetTypeByID("5"),
                isChosen = false
            };
            var Tabbar = TabBarCustomer.GetInstance();
            var ListStoreVM = (TabBarCustomer.GetInstance().Children.ElementAt(1).BindingContext as ListStoresViewModel);
            ListStoreVM.ResetChooseType();
            ListStoreVM.ChooseType(typeItem);
            Tabbar.CurrentPage = Tabbar.Children[1];
        }
        public void ChooseCandy()
        {
            TypeItem typeItem = new TypeItem
            {
                productType = dataProvider.GetTypeByID("4"),
                isChosen = false
            };
            var Tabbar = TabBarCustomer.GetInstance();
            var ListStoreVM = (TabBarCustomer.GetInstance().Children.ElementAt(1).BindingContext as ListStoresViewModel);
            ListStoreVM.ResetChooseType();
            ListStoreVM.ChooseType(typeItem);
            Tabbar.CurrentPage = Tabbar.Children[1];
        }
        public void ChooseDrink()
        {
            TypeItem typeItem = new TypeItem
            {
                productType = dataProvider.GetTypeByID("3"),
                isChosen = false
            };
            var Tabbar = TabBarCustomer.GetInstance();
            var ListStoreVM = (TabBarCustomer.GetInstance().Children.ElementAt(1).BindingContext as ListStoresViewModel);
            ListStoreVM.ResetChooseType();
            ListStoreVM.ChooseType(typeItem);
            Tabbar.CurrentPage = Tabbar.Children[1];
        }
        public void ChooseVeget()
        {
            TypeItem typeItem = new TypeItem
            {
                productType = dataProvider.GetTypeByID("2"),
                isChosen = false
            };
            var Tabbar = TabBarCustomer.GetInstance();
            var ListStoreVM = (TabBarCustomer.GetInstance().Children.ElementAt(1).BindingContext as ListStoresViewModel);
            ListStoreVM.ResetChooseType();
            ListStoreVM.ChooseType(typeItem);
            Tabbar.CurrentPage = Tabbar.Children[1];
        }
        public void ChooseFruit()
        {
            TypeItem typeItem = new TypeItem
            {
                productType = dataProvider.GetTypeByID("1"),
                isChosen = false
            };
            var Tabbar = TabBarCustomer.GetInstance();
            var ListStoreVM = (TabBarCustomer.GetInstance().Children.ElementAt(1).BindingContext as ListStoresViewModel);
            ListStoreVM.ResetChooseType();
            ListStoreVM.ChooseType(typeItem);
            Tabbar.CurrentPage = Tabbar.Children[1];
        }

        public void ShowDrawer()
        {
            var appDrawer = AppDrawer.GetInstance();
            appDrawer.FlyoutIsPresented = true;
        }

        public void LoadData()
        {
            Title = dataProvider.GetMyName();
            if (Title == "") Title = "Hi there!";
            else Title = "Hi " + Title + "!";
            _stores = new ObservableCollection<Store>(dataProvider.GetListStores());

            //Load list cho search bar
            SourceSearchItems = new ObservableCollection<SearchItemHome>
            {
                new SearchItemHome
                {
                    Name="Cart",
                    Image="heartcart"
                },
                new SearchItemHome
                {
                    Name="Orders",
                    Image="colororder"
                },
                new SearchItemHome
                {
                    Name="Stores",
                    Image="drawerstore"
                },
                new SearchItemHome
                {
                    Name="Fruits",
                    Image="colorfruit"
                },
                new SearchItemHome
                {
                    Name="Vegetables",
                    Image="greenveget"
                },
                new SearchItemHome
                {
                    Name="Drinks",
                    Image="colordrink"
                },
                new SearchItemHome
                {
                    Name="Candies",
                    Image="colorcandy"
                },
                new SearchItemHome
                {
                    Name="Cakes",
                    Image="colorcake"
                },
                new SearchItemHome
                {
                    Name="Your Store",
                    Image="colortool"
                },
            };

            SearchText = "";
        }

        public void OnSearch(string text)
        {
            List<SearchItemHome> updatedList = new List<SearchItemHome>();
            foreach (SearchItemHome item in SourceSearchItems)
                if (item.Name.ToLower().Contains(text.ToLower()))
                    updatedList.Add(item);
            SearchList = new ObservableCollection<SearchItemHome>(updatedList);
        }
    }
}
