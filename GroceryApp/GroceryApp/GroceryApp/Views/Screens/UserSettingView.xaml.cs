using GroceryApp.Data;
using GroceryApp.Services;
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

            var changeImageTapGestureRecognizer = new TapGestureRecognizer();
            changeImageTapGestureRecognizer.Tapped += ChangeImage_Clicked;
            changeImageTapGestureRecognizer.NumberOfTapsRequired = 1;
            changeBtn.GestureRecognizers.Add(changeImageTapGestureRecognizer);
        }

        private async void ChangeImage_Clicked(object sender, EventArgs e)
        {
            (sender as PancakeView).IsEnabled = false;


            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return; // user canceled file picking
                string path = fileData.FilePath;
                string fileName = fileData.FileName;
                string contents = System.Text.Encoding.UTF8.GetString(fileData.DataArray);

                System.Console.WriteLine("File name chosen: " + fileName);
                System.Console.WriteLine("File data: " + contents);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception choosing file: " + ex.ToString());
            }

            //Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            //if (stream != null)
            //{
            //    userimage.Source = ImageSource.FromStream(() => stream);
            //};

            (sender as PancakeView).IsEnabled = true;
        }

       
    }
}