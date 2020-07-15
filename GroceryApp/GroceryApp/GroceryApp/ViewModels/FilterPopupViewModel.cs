using Acr.UserDialogs;
using GroceryApp.Views.Screens;
using GroceryApp.Views.TabBars;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public class FilterPopupViewModel : BaseViewModel
    {
        public List<string> SortBy
        {
            get
            {
                return new List<string> { "Name", "Price" };
            }
            set { }
        }
        public List<string> OrderByName = new List<string> { "A->Z", "Z->A" };
        public List<string> OrderByPrice = new List<string> { "Min->Max", "Max->Min" };
        private ObservableCollection<string> _orderBy;
        public ObservableCollection<string> OrderBy
        {
            get { return _orderBy; }
            set { _orderBy = value; OnPropertyChanged(nameof(OrderBy)); }
        }
        private string _selectedSort;
        public string SelectedSort
        {
            get { return _selectedSort; }
            set { 
                _selectedSort = value;
                OnSelectedSortChange(value);
                OnPropertyChanged(nameof(SelectedSort)); }
        }

        private string _selectedOrder;
        public string SelectedOrder
        {
            get { return _selectedOrder; }
            set { _selectedOrder = value; OnPropertyChanged(nameof(SelectedOrder)); }
        }

        private bool _isFilter;
        public bool IsFilter
        {
            get { return _isFilter; }
            set { _isFilter = value; OnPropertyChanged(nameof(IsFilter)); }
        }

        private bool _isSort;
        public bool IsSort
        {
            get { return _isSort; }
            set { _isSort = value; OnPropertyChanged(nameof(IsSort)); }
        }

        private bool _isUse;
        public bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; OnPropertyChanged(nameof(IsUse)); }
        }

        private int _lowPrice;
        public int LowPrice
        {
            get { return _lowPrice; }
            set {
                if (value < 0 || value>HighPrice)
                {
                    LowPrice = _lowPrice;
                    OnPropertyChanged(nameof(LowPrice));
                    return;
                }
                _lowPrice = value;
                OnPropertyChanged(nameof(LowPrice)); }
        }

        private int _highPrice;
        public int HighPrice
        {
            get { return _highPrice; }
            set {
                if (value > HighestValue ||value<=LowPrice)
                {
                    HighPrice = _highPrice;
                    OnPropertyChanged(nameof(HighPrice));
                    return;
                }
                _highPrice = value; OnPropertyChanged(nameof(HighPrice)); }
        }

        public double HighestValue { get; set; }

        public ICommand ApplyCommand { get; set; }
        public ICommand GobackCommand { get; set; }
        public FilterPopupViewModel(double highestPrice)
        {
            LoadData(highestPrice);
            ApplyCommand = new Command(Apply);
            GobackCommand = new Command(Goback);

        }

        public void LoadData(double highestPrice)
        {
            this.HighestValue = (int)highestPrice;
            IsFilter = false;
            IsSort = false;
            IsUse = false;
            HighPrice = (int)highestPrice;
            LowPrice = 0;
            SelectedSort = SortBy[0];
            OrderBy = new ObservableCollection<string>(OrderByName);
            SelectedOrder = OrderBy[0];
            ApplyCommand = new Command(Apply);
        }

        public async void Apply()
        {
            using(UserDialogs.Instance.Loading("Applying filter.."))
            {
                bool isFilter = IsUse;
                string SortType = SelectedOrder;
                double FilterMin = -1;
                double FilterMax = -1;

                if (IsFilter)
                {
                    FilterMin = LowPrice;
                    FilterMax = HighPrice;
                }

                var ShowStoreVM = ShowStoreView.GetInstance().BindingContext as ShowStoreViewModel;
                ShowStoreVM.ApplyFilter(isFilter, SortType, FilterMin, FilterMax);
                await PopupNavigation.Instance.PopAllAsync();
            }
            
        }
        public async void Goback()
        {
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAllAsync();
        }

        public void OnSelectedSortChange(string value)
        {
            if(value== "Name")
            {
                OrderBy = new ObservableCollection<string>(OrderByName);
            }
            else
            {
                OrderBy = new ObservableCollection<string>(OrderByPrice);
            }
            SelectedOrder = OrderBy[0];
        }

        public FilterPopupViewModel()
        {
            //NO USE
        }
    }
}
