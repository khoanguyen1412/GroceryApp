using Acr.UserDialogs;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Services;
using GroceryApp.Views.Screens;
using GroceryApp.Views.TabBars;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
        string ImagePath;
        
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

            if (ImagePath != "")
            {
                Account account = new Account(
                    "ungdung-grocery-xamarin-by-dk",
                    "378791526477571",
                    "scsyCxQS_C74MbAGdOutpwrzlnU"
                    );

                Cloudinary cloudinary = new Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(ImagePath)
                };
                try
                {
                    using (UserDialogs.Instance.Loading("Saving.."))
                    {
                        var uploadResult = await cloudinary.UploadAsync(uploadParams);
                        string url = uploadResult.SecureUri.ToString();
                        myStore.ImageURL = url;
                    }


                }
                catch (Exception ex)
                {
                    var page = TabbarStoreManager.GetInstance().Children.ElementAt(4) as StoreSettingView;
                    await page.DisplayAlert("Error", "Error upload image to server, try again!", "Ok");
                }
            }
            
            //update store ở database server
            var httpClient = new HttpClient();
            await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "store/update", myStore);
            //update store ở database local
            DataUpdater.UpdateStore(myStore);
        }

        public async void ChangeImage()
        {
            
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return; // user canceled file picking
                string path = fileData.FilePath;

                StoreImage = (ImageSource)path;
                ImagePath = path;
            }
            catch (Exception ex)
            {
                var page =TabbarStoreManager.GetInstance().Children.ElementAt(4) as StoreSettingView;
                await page.DisplayAlert("Error", "Error picking file from divice, try again!", "Ok");
            }
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
            ImagePath = "";
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
