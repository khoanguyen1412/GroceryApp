using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Services;
using GroceryApp.Views.Screens;
using System;
using System.Collections.Generic;
using System.IO;
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
        private ImageSource _storeImage;

        public ImageSource StoreImage
        {
            get { return _storeImage; }
            set { _storeImage = value; OnPropertyChanged(nameof(StoreImage)); }
        }

        public ICommand InforSettingCommand { get; set; }
        public ICommand AddressSettingCommand { get; set; }
        public ICommand SaveChangeCommand { get; set; }
        public ICommand ChangeImageCommand { get; set; }
        public StoreSetttingViewModel()
        {
            loadData();
            InforSettingCommand = new Command(ShowInforSetting);
            AddressSettingCommand = new Command(ShowAddressSetting);
            SaveChangeCommand = new Command(SaveChange);
            ChangeImageCommand = new Command(ChangeImage);
        }

        public async void SaveChange()
        {
            Store store = this.myStore;
            
        }
             
        public async void ChangeImage()
        {
            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                StoreImage = ImageSource.FromStream(() => stream);

            };
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
                HouseNumber = items[0],
                District=items[1],
                City = items[2],
                Country = items[3]
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
            string address = newAddress.HouseNumber + "#" + newAddress.District + "#" + newAddress.City+"#"+newAddress.Country;
            myStore.StoreAddress = address;
        }
    }
}
