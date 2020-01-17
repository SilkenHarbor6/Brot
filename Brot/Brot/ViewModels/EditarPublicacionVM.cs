using Brot.Models;

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
        private void ActualizarMethod(object obj)
        {
            IsRefreshing = true;


            IsRefreshing = false;
        }
    }
}