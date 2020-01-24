namespace Brot
{
    using Brot.Patterns;
    using Brot.Services;
    using Microsoft.AppCenter;
    using Microsoft.AppCenter.Analytics;
    using Microsoft.AppCenter.Crashes;
    using Microsoft.AppCenter.Push;
    using Newtonsoft.Json;
    using Plugin.LocalNotification;
    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Views;
    using Xamarin.Forms;
    using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

    public partial class App : Xamarin.Forms.Application
    {
        private bool wasAppCenterKeysSent { get; set; }
        private bool DentroApp { get; set; } = true;
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
            NotificationCenter.Current.NotificationTapped += Current_NotificationTapped;
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
                    wasAppCenterKeysSent = true;

                    //Registrar telefono en base de datos
                    var usuario = new Models.userModel()
                    {
                        username = Singleton.Instance.User.username,
                        pass = Singleton.Instance.User.pass
                    };
                    var idInstalled02 = await Microsoft.AppCenter.AppCenter.GetInstallIdAsync();
                    usuario.Device_id = idInstalled02.Value.ToString();
                    usuario.Phone_OS = Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS ? "iOS" : "Android";
                    var result = await RestClient.Post<Models.userModel>("users/device", usuario);

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
        private void Current_NotificationTapped(NotificationTappedEventArgs e)
        {
            try
            {
                MainPage = new NavigationPage(new MainTabbed());
                AccionNotificacion(Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(e.Data));
            }
            catch (Exception ex) { }
        }
        public void Push_PushNotificationReceived(object sender, PushNotificationReceivedEventArgs e)
        {

            try
            {
                List<string> variables = new List<string>();
                foreach (var keyPair in e.CustomData)
                {
                    variables.Add(keyPair.Key + "," + keyPair.Value);
                }

                string data = Newtonsoft.Json.JsonConvert.SerializeObject(variables);
                var notification = new NotificationRequest
                {
                    NotificationId = 100,
                    Title = e.Title,
                    Description = e.Message,
                    ReturningData = data // Returning data when tapped on notification.
                                         //NotifyTime = DateTime.Now.(1) // Used for Scheduling local notification, if not specified notification will show immediately.
                };
                NotificationCenter.Current.Show(notification);
            }
            catch (Exception ex)
            {

                Crashes.TrackError(ex, new System.Collections.Generic.Dictionary<string, string>() {
                    {"Push Notification","Error interpretandola" }
                });
            }
        }

        private async void AccionNotificacion(List<string> CustomData)
        {
            int id_user, id_comment, id_post;
            id_user = 1;
            id_comment = 1;
            id_post = 1;
            string gotoPage = String.Empty;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in CustomData)
            {
                var data = item.Split(",");
                dic.Add(data[0], data[1]);
            }
            try
            {
                gotoPage = dic[PushConstantes.gotoPage];
            }
            catch (Exception) { }
            try
            {
                id_comment = Convert.ToInt32(dic[PushConstantes.id_comentario]);
            }
            catch (Exception) { }
            try
            {
                id_user = Convert.ToInt32(dic[PushConstantes.id_user]);
            }
            catch (Exception) { }
            try
            {
                id_post = Convert.ToInt32(dic[PushConstantes.id_post]);
            }
            catch (Exception) { }

            if (gotoPage == PushConstantes.goto_post)
            {
                await App.Current.MainPage.Navigation.PushAsync(new Views.Post(new ViewModels.PostViewModel(id_post), id_user));
            }
            else if (gotoPage == PushConstantes.goto_profile)
            {
                var perfil = await RestAPI.GetOtherUserrofile(id_user, Singleton.Instance.User.id_user);
                perfil.UserProfile.img = String.IsNullOrEmpty(perfil.UserProfile.img)
                                        ? DLL.constantes.ProfileImageError
                                        : DLL.constantes.urlImages + perfil.UserProfile.img;
                await App.Current.MainPage.Navigation.PushAsync(new Views.SellerProfile(perfil.UserProfile));
            }
        }

        protected override void OnSleep()
        {
            DentroApp = false;
        }

        protected override void OnResume()
        {
            DentroApp = true;
        }
    }
}
