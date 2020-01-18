﻿namespace Brot.Views.Tabs
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
    using Xamarin.Forms.GoogleMaps;
    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;
    using Brot.Services;
    using Brot.Models;
    using Brot.Patterns;
    using System.Collections.ObjectModel;

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

            //this.MyMap.MoveToRegion(
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

        private void MoveToSantaAna()
        {
            Mapa.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(
                        13.994778,
                        -89.556642
                        ),
                    Distance.FromMeters(2500)
                    )
                );
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