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
        private async void inicializar()
        {

            try
            {
                if (Singleton.Instance.LocalJson.IsUserLogged())
                {
                    Microsoft.AppCenter.AppCenter.SetUserId(Singleton.Instance.User.id_user.ToString());

                    // Avoid duplicate event registration:
                    if (!Microsoft.AppCenter.AppCenter.Configured)
                    {
                        Microsoft.AppCenter.Push.Push.PushNotificationReceived += (sender, e) =>
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
                            System.Diagnostics.Debug.WriteLine(summary);
                        };
                    }

                    // AppCenter.start after
                    Microsoft.AppCenter.AppCenter.Start("android=ce90d30b-e395-4d05-be5b-a1461a3bec8e;" +
                          "ios=0caa730c-a7e0-45b2-82bb-302f376b133d",
                           typeof(Microsoft.AppCenter.Push.Push));
                    //Main Page!
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
                Microsoft.AppCenter.Crashes.Crashes.TrackError(ex, new System.Collections.Generic.Dictionary<string, string>() {
                    {"AppStart", "Falló" }
                });
                MainPage = new NavigationPage(new Login());
            }
           ((NavigationPage)Xamarin.Forms.Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#031540");

            var idInstalled02 = await Microsoft.AppCenter.AppCenter.GetInstallIdAsync();
            //https://stackoverflow.com/questions/56982641/how-to-implement-appcenter-push-api
            //TODO Make it work!
        }


        protected async override void OnStart()
        {
            Microsoft.AppCenter.AppCenter.Start("android=ce90d30b-e395-4d05-be5b-a1461a3bec8e;" +
                  "ios=0caa730c-a7e0-45b2-82bb-302f376b133d",
                  typeof(Microsoft.AppCenter.Analytics.Analytics), typeof(Microsoft.AppCenter.Crashes.Crashes));

            if (await Microsoft.AppCenter.Analytics.Analytics.IsEnabledAsync())
            {
                await Microsoft.AppCenter.Analytics.Analytics.SetEnabledAsync(true);
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
