using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Views.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public interface IStoreSetttingViewModel
    {

    }

    
    class StoreSetttingViewModel: BaseViewModel, IStoreSetttingViewModel
    {
        DataProvider dataProvider = DataProvider.GetInstance();
        private Store myStore;
        private string _storeImage;

        public string StoreImage
        {
            get { return _storeImage; }
            set { _storeImage = value; OnPropertyChanged(nameof(StoreImage)); }
        }

        public ICommand InforSettingCommand { get; set; }
        public ICommand AddressSettingCommand { get; set; }
        public StoreSetttingViewModel()
        {
            loadData();
            InforSettingCommand = new Command(ShowInforSetting);
            AddressSettingCommand = new Command(ShowAddressSetting);
        }

        public async void ShowInforSetting()
        {
   
            var inforPage = new StoreInformationView();
            
            inforPage.BindingContext = new StoreInfor 
                                        { 
                                            StoreName= myStore.StoreName,
                                            StoreDescription= myStore.StoreDescription
                                        };
                                        
            await App.Current.MainPage.Navigation.PushAsync(inforPage, true);
        }

        public async void ShowAddressSetting()
        {

            var addressPage = new StoreAddressView();

            string[] items = myStore.StoreAddress.Split('#');
            addressPage.BindingContext = new AddressItem
            {
                Country=items[0],
                City=items[1],
                HouseNumber=items[2]
            };
            await App.Current.MainPage.Navigation.PushAsync(addressPage, true);
        }

        public void loadData()
        {
            myStore = dataProvider.GetStoreByIDStore(Infor.IDStore);
            StoreImage = myStore.ImageURL;
        }

        public void ChangeInfor(StoreInfor newInfor)
        {
            myStore.StoreName = newInfor.StoreName;
            myStore.StoreDescription = newInfor.StoreDescription;
        }

        public void ChangeAddress(AddressItem newAddress)
        {
            string address = newAddress.Country + "#" + newAddress.City + "#" + newAddress.HouseNumber;
            myStore.StoreAddress = address;
        }
    }
}
