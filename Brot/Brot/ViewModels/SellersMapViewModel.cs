

namespace Brot.ViewModels
{
    using Brot.Patterns;
    using Brot.Services;
    using Models;
    using System;
    using System.Collections.ObjectModel;
    //using Xamarin.Forms.GoogleMaps;
    using System.Diagnostics;
    using System.Windows.Input;
    using Xamarin.Forms.Maps;

    public class SellersMapViewModel : BaseViewModel
    {
        public Map Mapa;

        public ObservableCollection<Pin> Pins { get; set; }

        public ICommand InitPinsCommand
        {
            get
            {
                return new Xamarin.Forms.Command(InitPins);
            }
        }

        public SellersMapViewModel()
        {
            this.Pins = new ObservableCollection<Pin>();
        }

        public SellersMapViewModel(Xamarin.Forms.Maps.Map map)
        {
            Mapa = map as Xamarin.Forms.Maps.Map;
        }

        public async void InitPins()
        {
            var result = await RestClient.GetAll<userModel>("users/vendors/");

            if (!result.IsSuccess)
            {
                await Singleton.Instance.Dialogs.Message("Error trying to get sellers", result.Message);
                return;
            }

            foreach (var seller in (ObservableCollection<userModel>)result.Result)
            {
                Pin pin = new Pin()
                {
                    Label = $"Seller name: {seller.username} Description: {seller.descripcion}",
                    Position = new Position(Convert.ToDouble(seller.xlat), Convert.ToDouble(seller.ylon))
                };
            }
        }
        public ICommand pinClicked
        {
            get
            {
                return new Xamarin.Forms.Command(LoadBottom);
            }
        }
        public void LoadBottom()
        {
            Debug.Print("Clickeado");
        }

        #region InCaseYouWantToAddPins
        /*     
        public ICommand MapClicked
        {
            get
            {
                return new RelayCommand<MapClickedEventArgs>(MapClick);
            }
        }
        
        private void MapClick(MapClickedEventArgs args)
        {
            this.Pins.Clear();
            this.Pins.Add(new Pin()
            {
                Label = "Hellow World!\nI'm a Pin",
                Position = args.Point
            });
            App.Current.MainPage.DisplayAlert("BIEN", string.Format("Si hace click\nLongitud: {0}\nLatitud: {1}", args.Point.Longitude, args.Point.Latitude), "OKAY");
        }*/
        #endregion
    }
}
