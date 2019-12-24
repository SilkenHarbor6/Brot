

namespace Brot.ViewModels
{
    using Brot.Models.ResponseApi;
    using Brot.Patterns;
    using Brot.Services;
    using Brot.Views;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class ProfileViewModel : BaseViewModel
    {
        private ResponseUserProfile _Usuario;
        public ResponseUserProfile UserProfile
        {
            get { return this._Usuario; }
            set => SetProperty(ref _Usuario, value);
        }

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
        #region Commands

        public ICommand EditProfileCommand
        {
            get { return new Command(EditProfile); }
        }
        public ICommand SignOutCommand { get { return new Command(Signout); } }

        private Xamarin.Forms.Command _RefreshCommand;
        public Xamarin.Forms.Command RefreshCommand
        {
            get => _RefreshCommand ??= new Xamarin.Forms.Command(CargarDatos);
        }
        #endregion


        public ProfileViewModel()
        {
            CargarDatos();
        }
        public async void CargarDatos()
        {
            IsRefreshing = true;
            ResponseUserProfile profiledata = new ResponseUserProfile();

                profiledata = await RestAPI.userprofile(Singleton.Instance.User.id_user);

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
        #region Methods

        private void EditProfile()
        {
            Application.Current.MainPage.Navigation.PushAsync(new EditProfile());
        }
        private void Signout()
        {
            Singleton.Instance.LocalJson.SignOut();
            App.Current.MainPage = new NavigationPage(new Login());
        }


        #endregion
    }
}
