using Acr.UserDialogs;
using Com.OneSignal;
using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Services;
using GroceryApp.Views.Drawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroceryApp.Views.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MiddleView : ContentPage
    {
        DataProvider dataProvider = DataProvider.GetInstance();
        string Username;
        string Password;
        bool isAlreadyLogined = false;
        public MiddleView()
        {
            InitializeComponent();
        }

        public MiddleView(string Username,string Password)
        {
            InitializeComponent();
            this.Username = Username;
            this.Password = Password;
        }

        public void ChangeAlreadyLogin()
        {
            this.isAlreadyLogined = true;
        }

        protected  override async void OnAppearing()
        {
            base.OnAppearing();
            using (UserDialogs.Instance.Loading("Loging.."))
            {
                if (!dataProvider.CheckAccountExist(this.Username,this.Password))
                {
                    OneSignal.Current.SetExternalUserId("");

                    var loginpage = LoginView.GetInstance();
                    Application.Current.MainPage =new NavigationPage( LoginView.GetInstance());
                    await Application.Current.MainPage.DisplayAlert("Account not exist", "Wrong username or password, please try again", "OK");
                    return;
                };
                User user = dataProvider.GetUserByIDUser(this.Username);
                user.IsLogined = 1;
                Infor.IDUser = this.Username;
                Infor.IDStore = dataProvider.GetIDStoreByIDUser(this.Username);
                Infor.IDCart = this.Username;

                if (!isAlreadyLogined)
                {
                    //UserID_RandomStr
                    string ExternalStr = "UserID_" + DateTime.Now.ToString("ddMMyyyyHHmmss");
                    user.ExternalId = ExternalStr;
                    Preferences.Set("IDLogin", ExternalStr);

                    OneSignal.Current.SetExternalUserId("");
                    //PUSH NOTI TRƯỚC KHI SET ONESIGNAL
                    string datas = PushNotificationService.ConvertDataLogin(user);
                    PushNotificationService.Push(NotiNumber.Login, datas, false);

                    OneSignal.Current.SetExternalUserId(user.IDUser);
                    //OneSignal.Current.SendTag("IsLogined", "1");
                    var httpClient = new HttpClient();
                    await httpClient.PostAsJsonAsync(ServerDatabase.localhost + "user/update", user);
                }

                
                App.Current.MainPage = AppDrawer.GetInstance();
                if (dataProvider.IsLackOfInfor())
                {
                    
                    bool result = await DisplayAlert("Notice", "Update your infor to use our services!", "Go", "Later");
                    if (result)
                    {
                        var shell= AppDrawer.GetInstance();
                        shell.CurrentItem = shell.flyoutUserSetting;
                    }
                }

                MessageService.Show("Login success",1);
            }
            

            
        }
    }
}