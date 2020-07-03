using Com.OneSignal;
using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Views.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public class ImageItem
    {
        public string source { get; set; }
    }
    public class SplashScreenViewModel:BaseViewModel
    {
        public int LocationX
        {
            get
            {
                //int x= (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density / 2 - 20);
                return 0;
            }
        }
        public int index = 0;
        public List<ImageItem> OnboardingImages { get; set; }
        private ImageItem _currentImage;
        public ImageItem CurrentImage
        {
            get { return _currentImage; }
            set { _currentImage = value; OnPropertyChanged(nameof(CurrentImage)); }
        }

        private Color _color1;
        public Color Color1
        {
            get { return _color1; }
            set { _color1 = value; OnPropertyChanged(nameof(Color1)); }
        }

        private Color _color2;
        public Color Color2
        {
            get { return _color2; }
            set { _color2 = value; OnPropertyChanged(nameof(Color2)); }
        }

        private Color _color3;
        public Color Color3
        {
            get { return _color3; }
            set { _color3 = value; OnPropertyChanged(nameof(Color3)); }
        }

        private string _load;
        public string Load
        {
            get { return _load; }
            set { _load = value; OnPropertyChanged(nameof(Load)); }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged(nameof(Text)); }
        }

        private string _textBold;
        public string TextBold
        {
            get { return _textBold; }
            set { _textBold = value; OnPropertyChanged(nameof(TextBold)); }
        }

        private bool _busy;
        public bool Busy
        {
            get { return _busy; }
            set { _busy = value; OnPropertyChanged(nameof(Busy)); }
        }

        public ICommand OpenLoginCommand { get; set; }

        public SplashScreenViewModel()
        {
            OnboardingImages = new List<ImageItem>
            {
                new ImageItem{source="onboard1" },
                new ImageItem{source="onboard2" },
                new ImageItem{source="onboard3" },
            };
            index = 2;
            Device.StartTimer(TimeSpan.FromSeconds(1.7), (Func<bool>)(() =>
            {
                index = (index + 1) % 3;
                CurrentImage = OnboardingImages[index];
                SetColorbyIndex(index);
                SetTextbyIndex(index);
                return true;
            }));
            LoadData();
            OpenLoginCommand = new Command(OpenLogin);
        }

        public async void OpenLogin()
        {
            LoadData();
            //await App.Current.MainPage.Navigation.PushAsync(LoginView.GetInstance(), true);
        }


        public async void LoadData()
        {
            Busy = true;
            int loadindex = 2;
            Device.StartTimer(TimeSpan.FromSeconds(0.5), (Func<bool>)(() =>
            {
                loadindex = (loadindex + 1) % 3;                
                SetLoadbyIndex(loadindex);
                if (!Busy) return false;
                return true;
            }));

            
            
            LoadDataFromServer();
            //Busy = false;
        }

        public async void LoadDataFromServer()
        {
            try
            {
                await LoadServerDataAsync();
                //Check preference login
                string IDLogin = Preferences.Get("IDLogin", string.Empty);
                if(string.IsNullOrEmpty(IDLogin))
                {
                    App.Current.MainPage =new NavigationPage( LoginView.GetInstance());
                    return;
                }
                
                string IDUser= Preferences.Get("UsernameLogin", string.Empty);
                User user = DataProvider.GetInstance().GetUserByIDUser(IDUser);
                if (user==null || user.ExternalId != IDLogin )
                {
                    OneSignal.Current.SetExternalUserId("");
                    //OneSignal.Current.SendTag("IsLogined", "0");
                    Preferences.Set("IDLogin", "");
                    App.Current.MainPage = new NavigationPage(LoginView.GetInstance());
                    return;
                }

                string username= Preferences.Get("UsernameLogin", string.Empty);
                string password= Preferences.Get("PasswordLogin", string.Empty);
                var middle = new MiddleView(username, password);
                middle.ChangeAlreadyLogin();
                
                await App.Current.MainPage.Navigation.PushAsync(middle);
            }
            catch (Exception e)
            {
                Busy = false;
                HandleException.Onboarding();
                return;
            }
            
        }

        public async Task LoadServerDataAsync()
        {
            while(ServerDatabase.Users.Count==0)
                await ServerDatabase.LoadDataFromServer();

            Database.LoadDataToLocal();
        }

        public void SetColorbyIndex(int index)
        {
            if (index == 0)
            {
                Color1= Color.FromHex("#26B999");
                Color2 = Color.White;
                Color3 = Color.White;
            }
            if (index == 1)
            {
                Color1 = Color.White;
                Color2 = Color.FromHex("#26B999");
                Color3 = Color.White;
            }
            if (index == 2)
            {
                Color1 = Color.White;
                Color2 = Color.White;
                Color3 = Color.FromHex("#26B999");
            }
        }

        public void SetTextbyIndex(int index)
        {
            if (index == 0)
            {
                TextBold = "Choose your item";
                Text = "Easily find your grocery items and you will get delivery in wide range";
            }
            if (index == 1)
            {
                TextBold = "Simplified Shopping";
                Text = "Cut the chaos of grocery shopping with an organized list";
            }
            if (index == 2)
            {
                TextBold = "Tailored Recipes";
                Text = "Get tailored recipes or customise dish according to your kitchen items";
            }
        }

        public void SetLoadbyIndex(int index)
        {
            if (index == 0)
            {
                Load = "Loading.";
            }
            if (index == 1)
            {
                Load = "Loading..";
            }
            if (index == 2)
            {
                Load = "Loading...";
            }
        }
    }
}
