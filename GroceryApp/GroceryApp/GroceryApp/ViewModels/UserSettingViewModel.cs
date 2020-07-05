using Acr.UserDialogs;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Services;
using GroceryApp.Views.Screens;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public interface IUserSettingViewModel
    {

    }
    public class UserSettingViewModel: BaseViewModel, IUserSettingViewModel
    {
        
        DataProvider dataProvider = DataProvider.GetInstance();

        string ImagePath;
        bool isNewImage = false;

        private ImageSource _userImage;

        public ImageSource UserImage
        {
            get { return _userImage; }
            set { _userImage = value; OnPropertyChanged(nameof(UserImage)); }
        }

        private User _currentUser;

        public User CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; OnPropertyChanged(nameof(CurrentUser)); }
        }

        private DateTime _birthDate;

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; OnPropertyChanged(nameof(BirthDate)); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(nameof(Address)); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; OnPropertyChanged(nameof(FullName)); }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }

        #region edit mode
        private bool _fullNameEditMode;
        public bool FullNameEditMode
        {
            get { return _fullNameEditMode; }
            set { _fullNameEditMode = value; OnPropertyChanged(nameof(FullNameEditMode)); }
        }

        private bool _phoneNumberEditMode;
        public bool PhoneNumberEditMode
        {
            get { return _phoneNumberEditMode; }
            set { _phoneNumberEditMode = value; OnPropertyChanged(nameof(PhoneNumberEditMode)); }
        }

        private bool _emailEditMode;
        public bool EmailEditMode
        {
            get { return _emailEditMode; }
            set { _emailEditMode = value; OnPropertyChanged(nameof(EmailEditMode)); }
        }

        #endregion

        public ICommand ChangePasswordCommand { get; set; }
        public ICommand ChangeAddressCommand { get; set; }
        public ICommand FullNameEditCommand { get; set; }
        public ICommand PhoneNumberEditCommand { get; set; }
        public ICommand EmailEditCommand { get; set; }
        public ICommand SaveChangeCommand { get; set; }
        public ICommand ChangeImageCommand { get; set; }
        public ICommand GobackCommand { get; set; }
        public UserSettingViewModel()
        {
            LoadData();
            ChangePasswordCommand = new Command(ChangePassword);
            ChangeAddressCommand = new Command(ChangeAddress);
            FullNameEditCommand = new Command(ChangeFullNameEdit);
            PhoneNumberEditCommand = new Command(ChangePhoneNumberEdit);
            EmailEditCommand = new Command(ChangeEmailEdit);
            SaveChangeCommand = new Command(SaveChange);
            ChangeImageCommand = new Command(ChangeImage);
            GobackCommand = new Command(GoBack);
        }

        public async void GoBack()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        public async void SaveChange()
        {
            string message = CheckInfor();
            if (message != "")
            {
                var app = GroceryApp.Views.Drawer.AppDrawer.GetInstance();
                app.DisplayAlert("Error", message, "OK");
                return;
            }
            if (ImagePath != "" && isNewImage)
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
                        CurrentUser.ImageURL = url;
                        isNewImage = false;
                    }


                }
                catch (Exception ex)
                {
                    var page = UserSettingView.GetInstance();
                    await page.DisplayAlert("Error", "Error upload image to server, try again!", "Ok");
                }
            }

            CurrentUser.UserName = FullName;
            CurrentUser.PhoneNumber = PhoneNumber;
            CurrentUser.BirthDate = BirthDate;
            CurrentUser.Email= Email;
            CurrentUser.Password = MD5Service.EncodeToMD5(Infor.Password);
            using (UserDialogs.Instance.Loading("Updating.."))
            {
                //update user ở database server
                var httpClient = new HttpClient();
                await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "user/update", CurrentUser);
                //update user ở database local
                DataUpdater.UpdateUser(CurrentUser);
            }

            MessageService.Show("Update infor successfully", 0);

            //PUSH NOTI
            string datas = PushNotificationService.ConvertDataUpdateUser(CurrentUser);
            PushNotificationService.Push(NotiNumber.UpdateUser, datas, true);


        }

        public string CheckInfor()
        {
            if (string.IsNullOrEmpty(FullName)) return "User's name must not be blank, try again!";
            if (string.IsNullOrEmpty(PhoneNumber)) return "Phone number must not be blank, try again!";
            if (string.IsNullOrEmpty(Email)) return "Email must not be blank, try again!";
            if (!EmailService.CheckValidEmail(Email)) return "Email is not valid, try again!";
            if (EmailService.CheckExistedEmail(Email)) return "Email is used for another account, try again!";
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

                UserImage = (ImageSource)path;
                ImagePath = path;
                isNewImage = true;
            }
            catch (Exception ex)
            {
                var page = UserSettingView.GetInstance();
                await page.DisplayAlert("Error", "Error picking file from divice, try again!", "Ok");
            }
        }

        public void ChangeFullNameEdit()
        {
            FullNameEditMode = !FullNameEditMode;
        }
        public void ChangePhoneNumberEdit()
        {
            PhoneNumberEditMode = !PhoneNumberEditMode;
        }
        public void ChangeEmailEdit()
        {
            EmailEditMode = !EmailEditMode;
        }

        public async void ChangeAddress()
        {

            var addressPage = new StoreAddressView();
            addressPage.SetForUser();
            addressPage.parentName = "UserSetting";
            string[] items = CurrentUser.Address.Split('#');
            addressPage.BindingContext = new AddressItem
            {
                HouseNumber = items[0],
                District = items[1],
                City = items[2],
                Country = items[3]
            };
            await App.Current.MainPage.Navigation.PushAsync(addressPage, true);
        }

        public async void ChangePassword()
        {
            var changePasswordPage = new ChangePasswordView();
            ChangePasswordViewModel vm = new ChangePasswordViewModel(Infor.Password);
            changePasswordPage.BindingContext = vm;
            await App.Current.MainPage.Navigation.PushAsync(changePasswordPage, true);
        }

        public void LoadData()
        {
            CurrentUser = dataProvider.GetUserByIDUser(Infor.IDUser);
            BirthDate = CurrentUser.BirthDate;
            Address = CurrentUser.Address;
            FullName = CurrentUser.UserName;
            PhoneNumber = CurrentUser.PhoneNumber;
            Email = CurrentUser.Email;
            UserImage = CurrentUser.ImageURL;
            Password = ConvertPassword(Infor.Password);

            FullNameEditMode = false;
            PhoneNumberEditMode = false;
            EmailEditMode = false;
        }

        public void ChangeAddress(AddressItem newAddress)
        {
            string address = newAddress.HouseNumber + "#" + newAddress.District + "#" + newAddress.City + "#" + newAddress.Country;
            Address = address;
            CurrentUser.Address = address;
        }

        public void ChangePassword(string newPassword)
        {
            Password = ConvertPassword(newPassword);
            Infor.Password = newPassword;
            
        }


        public string ConvertPassword(string originPassword)
        {
            string hiddenPassword = "";
            for (int i = 0; i < originPassword.Length; i++)
            {
                hiddenPassword += "*";
            }

            return hiddenPassword;
        }
    }
}
