using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Platform.Android;

namespace GroceryApp.Droid
{
    [Activity(Label = "GroceryApp", Icon = "@mipmap/icon", 
        Theme = "@style/MainTheme", 
        MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
            var currentWindow = Window;
            currentWindow.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar;



            base.OnCreate(savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);


            Forms.SetFlags(new string[] { "IndicatorView_Experimental", "SwipeView_Experimental" });
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Window.SetSoftInputMode(Android.Views.SoftInput.AdjustPan);

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override async void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
                await PopupNavigation.Instance.PopAsync();
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }
    }
}