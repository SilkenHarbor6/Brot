namespace Brot.Views
{
    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;
    using System.ComponentModel;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class MainTabbed : TabbedPage
    {
        private bool MapsPermited = false;
        public MainTabbed()
        {
            InitializeComponent();
            ask4LocationMaaps().Wait();

            Children.Add(new Tabs.Feed());
            Children.Add(new Tabs.SellersMap());
            Children.Add(new Tabs.BrotTen());
            Children.Add(new Tabs.Profile());

            //CurrentPage = Children[1];

        }
        private async System.Threading.Tasks.Task ask4LocationMaaps()
        {
            try
            {
                PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
                PermissionStatus status2 = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                PermissionStatus status3 = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationAlways);

                if (status == PermissionStatus.Granted)
                {
                    MapsPermited = true;
                }
                else if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await App.Current.MainPage.DisplayAlert("Need location", "Gunna need that location", "OK");
                    }
                    status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                }



                if (status == PermissionStatus.Granted)
                {
                    //Query permission
                    MapsPermited = true;
                }
                else if (status != PermissionStatus.Unknown)
                {
                    //location denied
                    MapsPermited = false;
                }
                else
                {
                    MapsPermited = false;
                }
            }
            catch (System.Exception ex)
            {
                //Something went wrong
                Microsoft.AppCenter.Crashes.Crashes.TrackError(ex,
                    new System.Collections.Generic.Dictionary<string, string>()
                                            { { "Geolocalization","Error"} });
                MapsPermited = false;
            }
        }

    }
}