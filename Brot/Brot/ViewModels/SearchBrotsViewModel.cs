using Brot.Models;
using Brot.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Brot.ViewModels
{
    class SearchBrotsViewModel:BrotTenViewModel
    {
        private string text;
        private ObservableCollection<userModel> all;
        public ObservableCollection<userModel> vendedores { get; set; }
        public ObservableCollection<userModel> Vendedores
        {
            get
            {
                return vendedores;
            }
            set
            {
                vendedores = value;OnPropertyChanged("Vendedores");
            }
        }
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;OnPropertyChanged("Text");
            }
        }
        public SearchBrotsViewModel()
        {
            LoadDataFirstTime();
        }
        public ICommand updateList
        {
            get
            {
                return new Xamarin.Forms.Command(UpdateList);
            }
        }
        public ICommand refreshCommand
        {
            get
            {
                return new Xamarin.Forms.Command(LoadDataFirstTime);
            }
        }
        private async void LoadDataFirstTime()
        {
            var resp = await RestClient.GetAll<userModel>("users/vendors/");
            if (!resp.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se han podido cargar los vendedores", "Aceptar");
            }
            all = (ObservableCollection<userModel>)resp.Result;
            SetProperty(ref all, vendedores);
            //vendedores = (ObservableCollection<userModel>)resp.Result;
        }
        private void UpdateList(object obj)
        {
            if (!String.IsNullOrWhiteSpace(Text))
            {
                var elementos = from item in vendedores where item.puesto_name.Contains(Text) select item;
                Vendedores = (ObservableCollection<userModel>)elementos;
            }
            else
            {
                Vendedores = all;
            }

        }
    }
}
