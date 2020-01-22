namespace Brot
{
    using Brot.Patterns;
    using Brot.Services;
    using Microsoft.AppCenter;
    using Microsoft.AppCenter.Analytics;
    using Microsoft.AppCenter.Crashes;
    using Microsoft.AppCenter.Push;
    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Views;
    using Xamarin.Forms;
    using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

    public partial class App : Xamarin.Forms.Application
    {
        private bool wasAppCenterKeysSent { get; set; }
        public App()
        {
            wasAppCenterKeysSent = false;
            Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);

            //// Signout en iOS 
            //var jsoner = new UserJson();
            //jsoner.SignOut();

            InitializeComponent();
            perms();
            inicializar();
        }
        private async void perms()
        {
            await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
        }
        private async void inicializar()
        {

            try
            {
                // Avoid duplicate event registration:
                if (!AppCenter.Configured)
                {
                    Push.PushNotificationReceived += Push_PushNotificationReceived;
                }

                if (Singleton.Instance.LocalJson.IsUserLogged())
                {
                    //Main Page!
                    MainPage = new NavigationPage(new MainTabbed());

                    AppCenter.Start("android=ce90d30b-e395-4d05-be5b-a1461a3bec8e;" +
                          "ios=0caa730c-a7e0-45b2-82bb-302f376b133d",
                           typeof(Push), typeof(Analytics), typeof(Crashes));

                    //Registrar telefono en base de datos
                    var usuario = new Models.userModel()
                    {
                        username = Singleton.Instance.User.username,
                        pass = Singleton.Instance.User.pass
                    };
                    var idInstalled02 = await Microsoft.AppCenter.AppCenter.GetInstallIdAsync();
                    usuario.Device_id = idInstalled02.Value.ToString();
                    usuario.Phone_OS = Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS ? "iOS" : "Android";
                    var result = await RestClient.Post<Models.userModel>("users/login", usuario);

                    wasAppCenterKeysSent = true;
                    await Push.SetEnabledAsync(true);
                }
                else
                {
                    MainPage = new NavigationPage(new Login());
                    ActivarAnalyticsConKEYS();
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);

                //Capturo el error y lo mando a AppCenter antes que el app muera!!
                if (!wasAppCenterKeysSent)
                {
                    ActivarAnalyticsConKEYS();
                }

                Crashes.TrackError(ex, new System.Collections.Generic.Dictionary<string, string>() {
                    {"AppStart", "Falló" }
                });


                MainPage = new NavigationPage(new Login());
            }
           ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#031540");
        }


        protected override void OnStart()
        {

            if (!wasAppCenterKeysSent)
            {
                ActivarAnalyticsConKEYS();
            }

            GuardarDispositivoQueEntroAppCenter();
        }

        public async void GuardarDispositivoQueEntroAppCenter()
        {
            var idInstalled02 = await AppCenter.GetInstallIdAsync();
            Analytics.TrackEvent("Device Name", new System.Collections.Generic.Dictionary<string, string>()
                    {
                        { "Device",idInstalled02.Value.ToString() }
                    });


        }

        public async void ActivarAnalyticsConKEYS()
        {
            AppCenter.Start("android=ce90d30b-e395-4d05-be5b-a1461a3bec8e;" +
                  "ios=0caa730c-a7e0-45b2-82bb-302f376b133d",
                  typeof(Analytics), typeof(Crashes));

            wasAppCenterKeysSent = true;
            await Analytics.SetEnabledAsync(true);
            await Crashes.SetEnabledAsync(true);
        }

        public async void ActivarAnalytics()
        {
            AppCenter.Start(typeof(Analytics), typeof(Crashes));
            await Analytics.SetEnabledAsync(true);
            await Crashes.SetEnabledAsync(true);
        }

        public static void ActivarPush()
        {
            //INTERPRETAR PUSH

        }

        public void Push_PushNotificationReceived(object sender, PushNotificationReceivedEventArgs e)
        {
            //TODO hace metodo que redirija a las pages que me interesa con los datos pertinentes de los constructores xd
            //TODO Push on Server Side
            //MainPage.Navigation.InsertPageBefore

            try
            {
                // Add the notification message and title to the message
                var summary = $"Push notification received:" +
                                    $"\n\tNotification title: {e.Title}" +
                                    $"\n\tMessage: {e.Message}";

                // If there is custom data associated with the notification,
                // print the entries
                if (e.CustomData != null)
                {
                    summary += "\n\tCustom data:\n";
                    foreach (var key in e.CustomData.Keys)
                    {
                        summary += $"\t\t{key} : {e.CustomData[key]}\n";
                    }
                }

                // Send the notification summary to debug output
                App.Current.MainPage.DisplayAlert("Push", summary, "Ok");
            }
            catch (Exception ex)
            {

                Crashes.TrackError(ex, new System.Collections.Generic.Dictionary<string, string>() {
                    {"Push Notification","Error interpretandola" }
                });
            }
        }


        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
