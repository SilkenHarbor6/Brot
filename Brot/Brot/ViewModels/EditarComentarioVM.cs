using Brot.Models;
using Brot.Models.ResponseApi;
using Brot.Patterns;
using Brot.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brot.ViewModels
{
    public class EditarComentarioVM : BaseViewModel
    {

        private ResponseComentarios _comment;
        public ResponseComentarios Comentario
        {
            get { return _comment; }
            set { SetProperty(ref _comment, value); }
        }

        public EditarComentarioVM(ResponseComentarios comment)
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
            Comentario.comentario.fecha_creacion = DateTime.Now;
            var result = await RestClient.Put<comentariosModel>(DLL.constantes.comentariost, Comentario.comentario.id_comentario, Comentario.comentario);
            var newPage = new Views.Post(new PostViewModel(Comentario.comentario.id_post),Singleton.Instance.id_UserCreator_post);
            await App.Current.MainPage.Navigation.PopAsync();
            await App.Current.MainPage.Navigation.PopAsync();
            await App.Current.MainPage.Navigation.PushAsync(newPage);
            IsRefreshing = false;
        }
    }
}
