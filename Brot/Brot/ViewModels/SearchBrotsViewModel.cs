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
    class SearchBrotsViewModel:BaseViewModel
    {
        private string text;
        private ObservableCollection<userModel> all;
        private ObservableCollection<userModel> _vendedores;
        public ObservableCollection<userModel> Vendedores
        {
            get => _vendedores;
            set => SetProperty(ref _vendedores, value);
        }
        public string Texto
        {
            get => text;
            set => SetProperty(ref text, value);
        }
        public SearchBrotsViewModel()
        {
            LoadDataFirstTime();
        }

        public ICommand updateListCommand
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
            IsRefreshing = true;
            var resp = await RestClient.GetAll<userModel>("users/vendors/");
            if (!resp.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se han podido cargar los vendedores", "Aceptar");
            }       
            try
            {
                all = (ObservableCollection<userModel>)resp.Result;
                for (int i = 0; i < all.Count; i++)
                {
                    all[i].img =String.IsNullOrEmpty(all[i].img) ? DLL.constantes.ProfileImageError
                                    : DLL.constantes.urlImages + all[i].img;
                }
                Vendedores = all;
            }
            catch (Exception ex) 
            {
                Microsoft.AppCenter.Crashes.Crashes.TrackError(ex);
            }
            
            //vendedores = (ObservableCollection<userModel>)resp.Result;
            IsRefreshing = false;
        }
        private void UpdateList()
        {
            if (!String.IsNullOrWhiteSpace(Texto))
            {
                var elementos = from item in all 
                                where item.puesto_name.ToLower().Contains(Texto.ToLower()) ||
                                item.descripcion.ToLower().Contains(Texto.ToLower()) 
                                select item;
                Vendedores = new ObservableCollection<userModel>(elementos.ToList());
            }
            else
            {
                Vendedores = all;
            }

        }
    }
}
