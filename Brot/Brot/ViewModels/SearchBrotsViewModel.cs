using Brot.Models;
using Brot.Services;
using Brot.Views;
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
        private ObservableCollection<string> _categorias;
        private int _sind;
        private userModel usuario;
        public userModel Usuario
        {
            get
            {
                return usuario;
            }
            set
            {
                SetProperty(ref usuario, value);GoToProfile();
            }
        }
        public int sind
        {
            get
            {
                return _sind;
            }
            set
            {
                SetProperty(ref _sind, value);UpdateList();
            }
        }
        public ObservableCollection<userModel> Vendedores
        {
            get => _vendedores;
            set => SetProperty(ref _vendedores, value);
        }
        public ObservableCollection<string> Categorias
        {
            get => _categorias;
            set => SetProperty(ref _categorias, value);
        }
        public string Texto
        {
            get => text;
            set => SetProperty(ref text, value);
        }
        public SearchBrotsViewModel()
        {
            LoadDataFirstTime();
            Categorias = new ObservableCollection<string>();
            Categorias.Add("Todos");
            Categorias.Add("Tortas");
            Categorias.Add("HotDogs");
            Categorias.Add("Cafeteria");
            Categorias.Add("Tienda");
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
       
        private async void GoToProfile()
        {
            await App.Current.MainPage.Navigation.PushAsync(new SellerProfile(new SellerProfileViewModel(Usuario)));
            Usuario = null;
        }
        private async void LoadDataFirstTime()
        {
            IsRefreshing = true;
            var resp = await RestClient.GetAll<userModel>("users/vendors/");
            if (!resp.IsSuccess)
            {
                //await App.Current.MainPage.DisplayAlert("Error", "No se han podido cargar los vendedores", "Aceptar");
                return;
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
                if (sind!=0)
                {
                    var ele = from item in all
                              where item.nombreCategoria.Equals(Categorias[sind])
                              select item;

                    var elementos = from item in ele
                                    where item.puesto_name.ToLower().Contains(Texto.ToLower()) ||
                                    item.descripcion.ToLower().Contains(Texto.ToLower())
                                    select item;
                    Vendedores = new ObservableCollection<userModel>(elementos.ToList());
                }
                else
                {
                    var elementos = from item in all
                                    where item.puesto_name.ToLower().Contains(Texto.ToLower()) ||
                                    item.descripcion.ToLower().Contains(Texto.ToLower())
                                    select item;
                    ObservableCollection<userModel> temp = 
                    Vendedores = new ObservableCollection<userModel>(elementos.ToList());
                }
                
            }
            else if (sind!=0)
            {
                var ele = from item in all
                          where item.nombreCategoria.ToLower().Equals(Categorias[sind].ToLower())
                          select item;
                Vendedores = new ObservableCollection<userModel>(ele.ToList());
            }
            else
            {
                Vendedores = all;
            }

        }
    }
}
