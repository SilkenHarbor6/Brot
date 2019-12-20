
namespace Brot.ViewModels
{


    using Brot.Services;
    using Models;
    using Models.ResponseApi;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;

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
            this.lBrotTen = new ObservableCollection<ResponseUsuariosFiltro>();

            cargarUsers();
        }

        //SOLO PRUBEA
        //BORRAR CUANDO FUNCIONE EL API 
        public async void cargarUsers()
        {

            var resultBrotTEN = await RestAPI.getBrotTen();
            this._lBrotTen.Clear();
            for (int i = 0; i < resultBrotTEN.Count; i++)
            {
                resultBrotTEN[i].userData.img = resultBrotTEN[i].userData.img!= null
                    ? DLL.constantes.urlImages + resultBrotTEN[i].userData.img
                    : DLL.constantes.ProfileImageError;
            }

            lBrotTen = new ObservableCollection<ResponseUsuariosFiltro>(resultBrotTEN);

        }

        public System.Windows.Input.ICommand RefreshCommand { get { return new Xamarin.Forms.Command(cargarUsers); } }
    }
}
