namespace Brot
{
    using Brot.Patterns;
    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;
    using System;
    using System.Diagnostics;
    using Views;
    using Xamarin.Forms;
    using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);

            InitializeComponent();
            perms();
            inicializar();
        }
        private async void perms()
        {
            await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
        }
        private void inicializar()
        {

            try
            {

                if (Singleton.Instance.LocalJson.IsUserLogged())
                {
                    MainPage = new NavigationPage(new MainTabbed());
                }
                else
                {
                    MainPage = new NavigationPage(new Login());
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                MainPage = new NavigationPage(new Login());
            }
           ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#031540");
        }


        protected override void OnStart()
        {
            Microsoft.AppCenter.AppCenter.Start("android=ce90d30b-e395-4d05-be5b-a1461a3bec8e;" +
                  "ios=0caa730c-a7e0-45b2-82bb-302f376b133d",
                  typeof(Microsoft.AppCenter.Analytics.Analytics), typeof(Microsoft.AppCenter.Crashes.Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
