using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Permissions;
using Xamarin.Forms.GoogleMaps.Android;
using Microsoft.AppCenter.Push;
using Plugin.LocalNotifications;
using Plugin.LocalNotification;
using Android.Content;

namespace Brot.Droid
{
    [Activity(Label = "Brot", Icon = "@mipmap/BrotClient", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this,savedInstanceState);

            //Nugets!! Brot
            Acr.UserDialogs.UserDialogs.Init(this);
            //Initialazing Rounded Img
            ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init();
            //Images
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            //Initializaing Popup
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState);
            NotificationCenter.CreateNotificationChannel();
            LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.Brot500;
            LoadApplication(new App());
            NotificationCenter.NotifyNotificationTapped(Intent);
        }
        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        //{
        //    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //    PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnNewIntent(Android.Content.Intent intent)
        {
            //bool LlegoAppCenter = false;
            //try
            //{
            //    NotificationCenter.NotifyNotificationTapped(intent);
            //    Push.CheckLaunchedFromNotification(this, intent);
            //    LlegoAppCenter = true;
            //}
            //catch (Exception)
            //{
            //    if (LlegoAppCenter==false)
            //    {
            //        Push.CheckLaunchedFromNotification(this, intent);
            //    }
            //}


            NotificationCenter.NotifyNotificationTapped(intent);
            Push.CheckLaunchedFromNotification(this, intent);

            base.OnNewIntent(intent);
        }
    }
}