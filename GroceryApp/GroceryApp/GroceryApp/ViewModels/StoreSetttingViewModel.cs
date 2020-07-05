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
        bool changeActive = false;
        
        private ImageSource _storeImage;

        public ImageSource StoreImage
        {
            get { return _storeImage; }
            set { _storeImage = value; OnPropertyChanged(nameof(StoreImage)); }
        }


        private int _isActive;

        public int IsActive
        {
            get { return _isActive; }
            set { 
                _isActive = value;
                if (_isActive == 1) ActiveImage = "active";
                else ActiveImage = "inactive";
                OnPropertyChanged(nameof(IsActive)); 
            }
        }

        private string _activeImage;

        public string ActiveImage
        {
            get { return _activeImage; }
            set { _activeImage = value; OnPropertyChanged(nameof(ActiveImage)); }
        }

        public ICommand InforSettingCommand { get; set; }
        public ICommand AddressSettingCommand { get; set; }
        public ICommand SaveChangeCommand { get; set; }
        public ICommand ChangeImageCommand { get; set; }
        public ICommand ChangeActiveCommand { get; set; }
        public StoreSetttingViewModel()
        {
            loadData();
            InforSettingCommand = new Command(ShowInforSetting);
            AddressSettingCommand = new Command(ShowAddressSetting);
            SaveChangeCommand = new Command(SaveChange);
            ChangeImageCommand = new Command(ChangeImage);
            ChangeActiveCommand = new Command(ChangeActive);
        }

        public void ChangeActive()
        {
            IsActive = 1-IsActive;
            if (IsActive == 1)
            {
                if(dataProvider.IsLackOfStoreInfor())
                {
                    TabbarStoreManager.GetInstance().DisplayAlert("Notice", "Store's infor is not enough, please provide more information and try again", "OK");
                    IsActive = 1 - IsActive;
                    return;
                }
                if (!dataProvider.HasProductInMyStore())
                {
                    TabbarStoreManager.GetInstance().DisplayAlert("Notice", "There is no product for selling in your store, revise your products and try again", "OK");
                    IsActive = 1 - IsActive;
                    return;
                }
            }
            changeActive = true;
            myStore.IsActive = IsActive;
            SaveChange();

        }

        public async void SaveChange()
        {
            string message = CheckInfor();
            if (message != "")
            {
                var app = GroceryApp.Views.Drawer.AppDrawer.GetInstance();
                await app.DisplayAlert("Error", message, "OK");
                return;
            }
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

            using (UserDialogs.Instance.Loading("Saving.."))
            {
                //update store ở database server
                var httpClient = new HttpClient();
                await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "store/update", myStore);
                //update store ở database local
                DataUpdater.UpdateStore(myStore);
            }

            if (changeActive)
            {
                if(myStore.IsActive==0) MessageService.Show("Deactivate successfully", 0);
                else MessageService.Show("Activate successfully", 0);
                changeActive = false;
            }
            else MessageService.Show("Setting store successfully",0);

            //PUSH NOTI
            string datas = PushNotificationService.ConvertDataUpdateStore(myStore);
            PushNotificationService.Push(NotiNumber.UpdateStore, datas, true);
        }

        public string CheckInfor()
        {
            if (string.IsNullOrEmpty(myStore.StoreName)) return "Store's name must not be blank, try again!";
            string[] parts = myStore.StoreAddress.Split('#');
            bool empty = true;
            foreach(string part in parts)
                if (!string.IsNullOrEmpty(part))
                {
                    empty = false;
                    break;
                }
            if (empty) return "Store's address must not be blank, try again!";
            return "";
        }

        public async void ChangeImage()
        {
            
            try
            {
                string[] types = { "image/*" };
                FileData fileData = await CrossFilePicker.Current.PickFile(types);
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
            IsActive = myStore.IsActive;
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
