using Acr.UserDialogs;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GroceryApp.Data;
using GroceryApp.Services;
using GroceryApp.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace GroceryApp.Views.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserSettingView : ContentPage
    {
        private static UserSettingView _instance;

        public static UserSettingView GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UserSettingView();
            }
            return _instance;
        }

        public static void Destroy()
        {
            _instance = null;
        }
        private UserSettingView()
        {
            InitializeComponent();
            
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                datePicker.Focus();
            };
            tapGestureRecognizer.NumberOfTapsRequired = 1;
            dateCard.GestureRecognizers.Add(tapGestureRecognizer);


        }

       
    }
}