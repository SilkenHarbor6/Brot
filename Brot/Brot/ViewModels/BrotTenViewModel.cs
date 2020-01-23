
namespace Brot.ViewModels
{


    using Brot.Services;
    using Models;
    using Models.ResponseApi;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Threading.Tasks;

    class BrotTenViewModel : BaseViewModel
    {
        private ObservableCollection<ResponseUsuariosFiltro> _lBrotTen;

        public ObservableCollection<ResponseUsuariosFiltro> lBrotTen
        {
            get { return this._lBrotTen; }
            set
            {
                if (this._lBrotTen == value)
                    return;

                this._lBrotTen = value;
                OnPropertyChanged();
            }
        }

        public BrotTenViewModel()
        {

            cargarUsers();
            //Task.Run(async () => await  cargarUsers());
        }

        //SOLO PRUBEA
        //BORRAR CUANDO FUNCIONE EL API 

        private Xamarin.Forms.Command _RefreshComman;
        public Xamarin.Forms.Command RefreshCommand
        {
            get => _RefreshComman ?? (_RefreshComman = new Xamarin.Forms.Command(cargarUsers));
        }
        public async void cargarUsers()
        {
            IsRefreshing = true;

            var resultBrotTEN = await RestAPI.getBrotTen();
            this.lBrotTen = new ObservableCollection<ResponseUsuariosFiltro>();
            for (int i = 0; i < resultBrotTEN.Count; i++)
            {
                resultBrotTEN[i].userData.img = resultBrotTEN[i].userData.img != null
                    ? DLL.constantes.urlImages + resultBrotTEN[i].userData.img
                    : DLL.constantes.ProfileImageError;
            }

            lBrotTen = new ObservableCollection<ResponseUsuariosFiltro>(resultBrotTEN);

            IsRefreshing = false;

        }


    }
}
