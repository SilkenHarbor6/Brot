namespace Brot.Views.Tabs
{
    using System;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.GoogleMaps;
    using Plugin.Permissions.Abstractions;
    using Plugin.Permissions;
    using Xamarin.Forms.Xaml;
    using XamarinStyles;
    using Xamarin.Essentials;
    using Microsoft.AppCenter.Crashes;
    using System.Collections.Generic;
    using Brot.Services;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellersMap : ContentPage
    {
        SellersMapViewModel ViewModel;
        private bool MapsPermited = false;

        public SellersMap()
        {

            InitializeComponent();

            //TODO pedir permisos y si no los acepta, entonces NO ABRIR EL MAPA, porque dar error en iOS




            //Mapa.MapStyle = MapStyle.FromJson(new XamarinMapStyle().Text);
            BindingContext = this.ViewModel = new SellersMapViewModel(ref Mapa);
            XamarinMapStyle Style = new XamarinMapStyle();
            //this.Mapa.MapStyle = MapStyle.FromJson(Style.Text);

            this.Mapa.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(
                        13.994778,
                        -89.556642
                        ),
                    Distance.FromMeters(4000)
                    )
                );

            //this.ViewModel.InitPinsCommand.Execute(null);
        }


        private async System.Threading.Tasks.Task ask4Location()
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
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
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


        private async void MoveToSantaAna()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                System.Threading.CancellationTokenSource cts = new System.Threading.CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    Mapa.MoveToRegion(
                    MapSpan.FromCenterAndRadius(
                    new Position(
                        location.Latitude,
                        location.Longitude
                        ),
                    Distance.FromMeters(1000)
                    )
                );
                }
                else
                {
                    this.Mapa.MoveToRegion(
                        MapSpan.FromCenterAndRadius(
                            new Position(
                                13.994778,
                                -89.556642
                                ),
                            Distance.FromMeters(2500)
                            )
                        );
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                Crashes.TrackError(fnsEx, new Dictionary<string, string>() { { "Mapa", "FeatureNotSupportedException" } });
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                if (Device.RuntimePlatform == Device.Android)
                {
                    DependencyService.Get<IGPSService>().turnOnGps();
                    MoveToSantaAna();
                }
                Crashes.TrackError(fneEx, new Dictionary<string, string>() { { "Mapa", "FeatureNotEnabledException" } });
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                Crashes.TrackError(pEx, new Dictionary<string, string>() { { "Mapa", "PermissionException" } });
            }
            catch (Exception ex)
            {
                // Unable to get location
                Crashes.TrackError(ex, new Dictionary<string, string>() { { "Mapa", "Exception" } });
            }

        }

        private void Mapa_PinClicked(object sender, PinClickedEventArgs e)
        {
            var pin = e.Pin;
            for (int i = 0; i < ((Xamarin.Forms.GoogleMaps.Map)sender).Pins.Count; i++)
            {
                if (e.Pin.Equals(((Xamarin.Forms.GoogleMaps.Map)sender).Pins[i]))
                {

                    ViewModel.pinClicked.Execute(i);
                    return;
                }
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            MoveToSantaAna();
        }
    }
}