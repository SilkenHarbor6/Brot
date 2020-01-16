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
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
