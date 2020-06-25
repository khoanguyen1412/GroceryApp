using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Views.Screens;
using Plugin.SharedTransitions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public interface IListStoresViewModel
    {

    }

    public class ListStoresViewModel : BaseViewModel, IListStoresViewModel
    {
        public int currentType = -1;
        private ObservableCollection<Store> _stores;

        public ObservableCollection<Store> Stores
        {
            get { return _stores; }
            set { _stores = value; OnPropertyChanged(nameof(Stores)); }
        }

        private ObservableCollection<TypeItem> _typeItems;

        public ObservableCollection<TypeItem> TypeItems
        {
            get { return _typeItems; }
            set { _typeItems = value; OnPropertyChanged(nameof(TypeItems)); }
        }

        public ICommand ShowStoreCommand { get; set; }
        public ICommand ChooseTypeCommand { get; set; }
        public ListStoresViewModel()
        {
            LoadProductTypes();
            LoadData();
            ShowStoreCommand = new Command<string>(ShowStore);
            ChooseTypeCommand = new Command<TypeItem>(ChooseType);
        }

        public void ChooseType(TypeItem typeItem)
        {
            int choosingIndex = -1;
            for (int i = 0; i < _typeItems.Count; i++)
            {
                if (_typeItems[i].productType.IDType == typeItem.productType.IDType)
                {
                    choosingIndex = i;
                    _typeItems[i].isChosen = !_typeItems[i].isChosen;
                }
                else
                {
                    _typeItems[i].isChosen = false;
                }

            }
            if (choosingIndex != currentType)
            {
                currentType = choosingIndex;
                LoadStores();
            }
            if (_typeItems[choosingIndex].isChosen == false)
            {
                currentType = -1;
                LoadStores();
            }

            TypeItems = new ObservableCollection<TypeItem>(_typeItems);

        }

        public void LoadStores()
        {
            var dataProvider = DataProvider.GetInstance();
            List<Store> stores = dataProvider.GetListStores();
            if (currentType == -1)
            {
                Stores = new ObservableCollection<Store>(stores);
                return;
            }

            List<Store> filterStores = new List<Store>();
            foreach(Store store in stores)
                if(dataProvider.isTypeInStore((currentType+1).ToString(),store.IDStore))
                {
                    filterStores.Add(store);
                }

            Stores = new ObservableCollection<Store>(filterStores);
        }

        public async void ShowStore(string IDStore)
        {
            var showStoreView =  ShowStoreView.GetInstance();
            showStoreView.BindingContext = new ShowStoreViewModel(IDStore);
            await App.Current.MainPage.Navigation.PushAsync(showStoreView, true);
        }

        public void LoadData()
        {
            var dataProvider = DataProvider.GetInstance();

            Stores = new ObservableCollection<Store>(dataProvider.GetListStores());
        }

        public void LoadProductTypes()
        {
            var dataProvider = DataProvider.GetInstance();
            List<ProductType> types = dataProvider.GetProductTypes();
            List<TypeItem> typeItems = new List<TypeItem>();
            foreach (ProductType type in types)
            {
                TypeItem typeItem = new TypeItem();
                typeItem.productType = type;
                typeItem.isChosen = false;

                typeItems.Add(typeItem);
            }
            _typeItems = new ObservableCollection<TypeItem>(typeItems);
        }
    }
}
