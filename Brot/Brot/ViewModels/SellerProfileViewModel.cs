

namespace Brot.ViewModels
{
    using Brot.Patterns;
    using Brot.Services;
    using Brot.Views;
    using Models.ResponseApi;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class SellerProfileViewModel : BaseViewModel
    {
        private ResponseUserProfile _Usuario;
        private ResponsePublicacionFeed _publicacionesThis;

        public ResponsePublicacionFeed publicacionesThis
        {
            get { return _publicacionesThis; }
            set
            {
                if (value != null)
                {
                    App.Current.MainPage.Navigation.PushAsync(new Post(new PostViewModel(value)));
                    SetProperty(ref _publicacionesThis, null);
                }
            }
        }
        public ResponseUserProfile UserProfile
        {
            get { return this._Usuario; }
            set => SetProperty(ref _Usuario, value);
        }
        private bool _isMyProfile;
        public bool isMyProfile
        {
            get => _isMyProfile;
            set => SetProperty(ref _isMyProfile, value);
        }

        private Xamarin.Forms.Command _RefreshCommand;
        public Xamarin.Forms.Command RefreshCommand
        {
            get => _RefreshCommand ??= new Xamarin.Forms.Command(CargarDatos);
        }


        public SellerProfileViewModel(Brot.Models.userModel usuarioModel)
        {
            isMyProfile = usuarioModel.id_user == Singleton.Instance.User.id_user;
            UserProfile = new ResponseUserProfile()
            {
                UserProfile = usuarioModel
            };
            CargarDatos();

        }

        public async void CargarDatos()
        {
            IsRefreshing = true;
            ResponseUserProfile profiledata = new ResponseUserProfile();
            if (UserProfile.UserProfile.id_user != Singleton.Instance.User.id_user)
            {
                profiledata = await RestAPI.GetOtherUserrofile(this.UserProfile.UserProfile.id_user, Singleton.Instance.User.id_user);
            }
            else
            {
                //Mi perfil!
                profiledata = await RestAPI.userprofile(Singleton.Instance.User.id_user);
            }

            if (profiledata != null)
            {
                if (profiledata.UserProfile.img != null)
                {
                    profiledata.UserProfile.img = DLL.constantes.urlImages + profiledata.UserProfile.img;
                }
                else
                {
                    profiledata.UserProfile.img = DLL.constantes.ProfileImageError;
                }
                UserProfile = profiledata;

                for (int i = 0; i < UserProfile.publicacionesUser.Count; i++)
                {
                    ///No verifico si la imagen es null, porque ya lo hice en alguna page anterior
                    UserProfile.publicacionesUser[i].UsuarioCreator = UserProfile.UserProfile;
                    UserProfile.publicacionesUser[i].publicacion.img = DLL.constantes.urlImages + UserProfile.publicacionesUser[i].publicacion.img;
                }
            }

            IsRefreshing = false;

        }

    }
}
