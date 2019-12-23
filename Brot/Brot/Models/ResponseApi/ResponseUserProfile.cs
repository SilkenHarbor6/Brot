namespace Brot.Models.ResponseApi
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ResponseUserProfile : ViewModels.ObservableObject
    {
        private bool _isFollowed;
        private int _cantSeguidores;
        private int _cantSeguidos;

        public userModel UserProfile { get; set; }
        public int cantSeguidores
        {
            get => _cantSeguidores;
            set => SetProperty(ref _cantSeguidores, value);
        }
        public int cantSeguidos
        {
            get => _cantSeguidos;
            set => SetProperty(ref _cantSeguidos, value);
        }
        public bool isFollowed
        {
            get => _isFollowed;
            set => SetProperty(ref _isFollowed, value);
        }
        public List<ResponsePublicacionFeed> publicacionesUser { get; set; }
        public List<ResponsePublicacionGuardada> publicacionesGuardadas { get; set; }



        #region Seguir al usuario
        private Xamarin.Forms.Command _btnFollowUser;
        public Xamarin.Forms.Command BtnFollowUserCommand => _btnFollowUser ??= new Xamarin.Forms.Command(async () => await FolowUserMethod());
        private async Task FolowUserMethod()
        {

            seguidoresModel seguirObject = new seguidoresModel()
            {
                fecha = DateTime.Now,
                id_seguido = this.UserProfile.id_user,
                seguidor_id = Patterns.Singleton.Instance.User.id_user,
                accepted = true
            };

            if (isFollowed)
            {
                //Dejo de seguir!
                isFollowed = !isFollowed;
                cantSeguidores--;
                var respuesta = await Services.RestClient.Post<seguidoresModel>(DLL.constantes.seguidorest + "/borrar", seguirObject);
            }
            else
            {
                //Sigo al perfil
                isFollowed = !isFollowed;
                cantSeguidores++;
                var respuuestas = await Services.RestClient.Post<seguidoresModel>(DLL.constantes.seguidorest, seguirObject);
            }
        }
        #endregion


        #region Ver Seguidores
        private Xamarin.Forms.Command _VerSeguidoresCommand;
        public Xamarin.Forms.Command VerSeguidoresCommand => _VerSeguidoresCommand ??= new Xamarin.Forms.Command(async () => await _VerSeguidoresMethod());
        private async Task _VerSeguidoresMethod()
        {
            if (cantSeguidores > 0)
            {
                await App.Current.MainPage.Navigation.PushAsync(new Views.LikesPeoplePage(this.UserProfile.id_user, ViewModels.likeType.seguidores));
            }
        }
        #endregion

        #region Ver Seguidos
        private Xamarin.Forms.Command _VerSeguidosCommand;
        public Xamarin.Forms.Command VerSeguidosCommand => _VerSeguidosCommand ??= new Xamarin.Forms.Command(async () => await VerSeguidosMethod());
        private async Task VerSeguidosMethod()
        {
            if (cantSeguidos > 0)
            {
                await App.Current.MainPage.Navigation.PushAsync(new Views.LikesPeoplePage(this.UserProfile.id_user, ViewModels.likeType.seguidos));
            }
        }
        #endregion
    }
}
