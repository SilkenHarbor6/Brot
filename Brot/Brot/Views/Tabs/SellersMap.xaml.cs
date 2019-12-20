namespace Brot.Views.Tabs
{
    using ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Xamarin.Forms;
    //using Xamarin.Forms.GoogleMaps;
    using Xamarin.Forms.Xaml;
    using XamarinStyles;
    using Xamarin.Forms.Maps;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellersMap : ContentPage
    {
        SellersMapViewModel ViewModel;
        public SellersMap()
        {
            InitializeComponent();

            Mapa.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(
                        13.994778,
                        -89.556642
                        ),
                    Distance.FromMeters(2500)
                    )
                );
            BindingContext = this.ViewModel = new SellersMapViewModel(Mapa);

            XamarinMapStyle Style = new XamarinMapStyle();
            //this.MyMap.MapStyle = MapStyle.FromJson(Style.Text);

            //this.MyMap.MoveToRegion(
            //    MapSpan.FromCenterAndRadius(
            //        new Position(
            //            13.994778,
            //            -89.556642
            //            ), 
            //        Distance.FromMeters(2500)
            //        )
            //    );

            this.ViewModel.InitPinsCommand.Execute(null);
        }
    }
}