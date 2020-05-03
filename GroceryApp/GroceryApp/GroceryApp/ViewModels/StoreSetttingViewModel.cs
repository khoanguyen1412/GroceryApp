using GroceryApp.DataProviders;
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

    public class StoreInfor
    {
        public string StoreName { get; set; }
        public string StoreDescription { get; set; }
    }
    class StoreSetttingViewModel: BaseViewModel, IStoreSetttingViewModel
    {

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
                                            StoreName= DataProvider.Store.StoreName,
                                            StoreDescription= DataProvider.Store.StoreDescription
                                        };
            await App.Current.MainPage.Navigation.PushAsync(inforPage, true);
        }

        public async void ShowAddressSetting()
        {

            var addressPage = new StoreAddressView();
            /*
            inforPage.BindingContext = new StoreInfor
            {
                StoreName = DataProvider.Store.StoreName,
                StoreDescription = DataProvider.Store.StoreDescription
            };*/
            await App.Current.MainPage.Navigation.PushAsync(addressPage, true);
        }

        public void loadData()
        {
            _storeImage = DataProvider.Store.ImageURL;

        }
    }
}
