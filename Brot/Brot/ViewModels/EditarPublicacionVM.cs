using Brot.Models;
using Brot.Services;

namespace Brot.ViewModels
{
    internal class EditarPublicacionVM : BaseViewModel
    {
        private Models.ResponseApi.ResponsePublicacionFeed _post;
        public Models.ResponseApi.ResponsePublicacionFeed userM
        {
            get { return _post; }
            set { SetProperty(ref _post, value); }
        }
        public EditarPublicacionVM(Models.ResponseApi.ResponsePublicacionFeed post)
        {
            this.userM = post;
        }



        private Xamarin.Forms.Command _Comando;
        public Xamarin.Forms.Command ActualizarCommand
        {
            get => _Comando ?? (_Comando = new Xamarin.Forms.Command(ActualizarMethod));
        }
        private async void ActualizarMethod(object obj)
        {
            IsRefreshing = true;

            var resultSuccess = await RestClient.Put<publicacionesModel>(DLL.constantes.publicacionest, userM.publicacion.id_post, userM.publicacion);

            if (resultSuccess)
            {
                var newPage = new Views.Post(new PostViewModel(userM));
                await App.Current.MainPage.Navigation.PopAsync();
                await App.Current.MainPage.Navigation.PopAsync();
                await App.Current.MainPage.Navigation.PushAsync(newPage);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Descripción no actualizada", "Intente de nuevo o pruebe más tarde", "Ok");
            }
            IsRefreshing = false;
        }
    }
}