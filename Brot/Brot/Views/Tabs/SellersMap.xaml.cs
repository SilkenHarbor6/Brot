namespace Brot.Views.Tabs
{
    using System;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.GoogleMaps;
    //using Xamarin.Forms.GoogleMaps;
    using Xamarin.Forms.Xaml;
    using XamarinStyles;
    using Xamarin.Essentials;
    using Microsoft.AppCenter.Crashes;
    using System.Collections.Generic;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellersMap : ContentPage
    {
        SellersMapViewModel ViewModel;
        public SellersMap()
        {

            InitializeComponent();

            MoveToSantaAna();

            //Mapa.MapStyle = MapStyle.FromJson(new XamarinMapStyle().Text);
            BindingContext = this.ViewModel = new SellersMapViewModel(ref Mapa);
            XamarinMapStyle Style = new XamarinMapStyle();
            this.Mapa.MapStyle = MapStyle.FromJson(Style.Text);

            //this.Mapa.MoveToRegion(
            //    MapSpan.FromCenterAndRadius(
            //        new Position(
            //            13.994778,
            //            -89.556642
            //            ), 
            //        Distance.FromMeters(2500)
            //        )
            //    );

            //this.ViewModel.InitPinsCommand.Execute(null);
        }

        private async void MoveToSantaAna()
        {
            try
            {
                var location = await Geolocation.GetLocationAsync();

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
                Crashes.TrackError(fnsEx, new Dictionary<string, string>() { { "Mapa","FeatureNotSupportedException" } });
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception

                //TODO activar ubicacion
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
            ViewModel.pinClicked.Execute(Convert.ToInt32(pin.Address));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            MoveToSantaAna();
        }
    }
}