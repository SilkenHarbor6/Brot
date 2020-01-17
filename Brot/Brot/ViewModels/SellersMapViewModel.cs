namespace Brot.ViewModels
{
    using Brot.Patterns;
    using Brot.Services;
    using Models;
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows.Input;
    using Xamarin.Forms.GoogleMaps;
    using Xamarin.Forms.Internals;

    public class SellersMapViewModel : BaseViewModel
    {
        public Map Mapa;
        private ObservableCollection<Pin> _places;
        public ObservableCollection<Pin> places
        {
            get
            {
                return _places;
            }
            set
            {
                _places = value; OnPropertyChanged("places");
            }
        }

        public ICommand InitPinsCommand
        {
            get
            {
                return new Xamarin.Forms.Command(InitPins);
            }
        }
        public SellersMapViewModel(ref Map map)
        {
            Mapa = map;
            InitPins();
        }
        public async void InitPins()
        {
            places = new ObservableCollection<Pin>();
            var result = await RestClient.GetAll<userModel>("users/vendors/");

            if (!result.IsSuccess)
            {
                await Singleton.Instance.Dialogs.Message("Error trying to get sellers", result.Message);
                return;
            }
            //api / categoria / GMC /{ id}
            ObservableCollection<String> cats = new ObservableCollection<String>();
            foreach (var seller in (ObservableCollection<userModel>)result.Result)
            {
                var r = await RestClient.GetAllS<string>("categoria/GMC/" + seller.id_user);
                if (!r.IsSuccess)
                {
                    await Singleton.Instance.Dialogs.Message("Error", r.Message);
                    return;
                }
                var c = r.Result.ToString();
                c = c.Replace("\"", "");
                cats.Add(c);
            }
            int i = 0;
            foreach (var seller in (ObservableCollection<userModel>)result.Result)
            {
                Pin pin = new Pin();
                pin.Label = $"Seller name: {seller.username} Description: {seller.descripcion}";
                pin.Position = new Position(Convert.ToDouble(seller.xlat), Convert.ToDouble(seller.ylon));
                pin.Icon = BitmapDescriptorFactory.FromBundle(cats[i]);
                pin.Type = PinType.Place;
                i++;
                places.Add(pin);
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
    [Preserve(AllMembers = true)]
    class Place : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Address { get; }

        public string Description { get; }

        Position _position;
        public Position Position
        {
            get => _position;
            set
            {
                if (!_position.Equals(value))
                {
                    _position = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Position)));
                }
            }
        }

        int _iconNumber;
        public int IconNumber
        {
            get => _iconNumber;
            set
            {
                if (_iconNumber != value)
                {
                    _iconNumber = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IconNumber)));
                }
            }
        }

        public Place(string address, string description, Position position, int iconNumber)
        {
            Address = address;
            Description = description;
            Position = position;
            IconNumber = iconNumber;
        }
    }
}
