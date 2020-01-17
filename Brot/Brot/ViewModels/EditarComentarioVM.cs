using Brot.Models;
using Brot.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brot.ViewModels
{
    public class EditarComentarioVM : BaseViewModel
    {

        private comentariosModel _comment;
        public comentariosModel Comentario
        {
            get { return _comment; }
            set { SetProperty(ref _comment, value); }
        }

        public EditarComentarioVM(comentariosModel comment)
        {
            Comentario = comment;
        }



        private Xamarin.Forms.Command _ActualizarCommand;
        public Xamarin.Forms.Command ActualizarCommand
        {
            get => _ActualizarCommand ?? (_ActualizarCommand = new Xamarin.Forms.Command(ActualizarMethod));
        }
        private async void ActualizarMethod(object obj)
        {
            IsRefreshing = true;
            Comentario.fecha_creacion = DateTime.Now;
            var result = await RestClient.Put<comentariosModel>(DLL.constantes.comentariost, Comentario.id_comentario, Comentario);
            await App.Current.MainPage.Navigation.PopAsync();
            await App.Current.MainPage.Navigation.PopAsync();   
            IsRefreshing = false;
        }
    }
}
