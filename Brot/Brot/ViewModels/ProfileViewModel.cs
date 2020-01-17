

namespace Brot.ViewModels
{
    using Brot.Models.ResponseApi;
    using Brot.Patterns;
    using Brot.Services;
    using Brot.Views;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Threading.Tasks;
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

        private string _UsuarioNombreMostrar = "Usuario";
        public string UsuarioNombreMostrar
        {
            set => SetProperty(ref _UsuarioNombreMostrar, value);
            get => _UsuarioNombreMostrar;
        }

        private bool _VerPostPropios;
        public bool VerPostPropios
        {
            get { return _VerPostPropios; }
            set { SetProperty(ref _VerPostPropios, value); }
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

        private ObservableCollection<ResponsePublicacionFeed> _publicacionesGuardadas;
        private ObservableCollection<ResponsePublicacionFeed> _PublicacionesMostradas;
        public ObservableCollection<ResponsePublicacionFeed> publicacionesMostradas
        {
            get { return _PublicacionesMostradas; }
            set { SetProperty(ref _PublicacionesMostradas, value); }
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
            VerPostPropios = true;
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

                //Publicaciones Propias
                for (int i = 0; i < UserProfile.publicacionesUser.Count; i++)
                {
                    ///No verifico si la imagen es null, porque ya lo hice en alguna page anterior
                    UserProfile.publicacionesUser[i].UsuarioCreator = UserProfile.UserProfile;
                    UserProfile.publicacionesUser[i].publicacion.img = DLL.constantes.urlImages + UserProfile.publicacionesUser[i].publicacion.img;
                }

                //Postsguardados
                _publicacionesGuardadas = new ObservableCollection<ResponsePublicacionFeed>();
                for (int i = 0; i < UserProfile.publicacionesGuardadas.Count; i++)
                {
                    try
                    {
                        UserProfile.publicacionesGuardadas[i].UsuarioCreator.img = DLL.constantes.urlImages + UserProfile.publicacionesGuardadas[i].UsuarioCreator.img;
                    }
                    catch (Exception)
                    {
                            ///No llegar el usuario
                            ///TODO Modificar api Usuario de pOst guardados
                    }
                    UserProfile.publicacionesGuardadas[i].publicacion.img = DLL.constantes.urlImages + UserProfile.publicacionesGuardadas[i].publicacion.img;
                    _publicacionesGuardadas.Add(new ResponsePublicacionFeed()
                    {
                        cantComentarios = UserProfile.publicacionesGuardadas[i].cantComentarios,
                        cantLikes = UserProfile.publicacionesGuardadas[i].cantLikes,
                        IsLiked = UserProfile.publicacionesGuardadas[i].IsLiked,
                        IsSavedPost = UserProfile.publicacionesGuardadas[i].IsSavedPost,
                        publicacion = UserProfile.publicacionesGuardadas[i].publicacion,
                        UsuarioCreator = UserProfile.publicacionesGuardadas[i].UsuarioCreator
                    });
                }


                if (VerPostPropios)
                {
                    publicacionesMostradas = new ObservableCollection<ResponsePublicacionFeed>(UserProfile.publicacionesUser);
                }
                else
                {
                    publicacionesMostradas = new ObservableCollection<ResponsePublicacionFeed>(_publicacionesGuardadas);
                }
            }

            UsuarioNombreMostrar = UserProfile.UserProfile.isVendor ? UserProfile.UserProfile.puesto_name : UserProfile.UserProfile.nombre + " " + UserProfile.UserProfile.apellido;
            await Task.Delay(200);
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



        private Xamarin.Forms.Command _ChangePostPropiosViews;
        public Xamarin.Forms.Command ChangePostPropiosViews
        {
            get => _ChangePostPropiosViews ?? (_ChangePostPropiosViews = new Xamarin.Forms.Command<string>(ChangePostPropiosViewsMethod));
        }
        private void ChangePostPropiosViewsMethod(string obj)
        {
            if (obj!="Null")
            {
                if (obj == "True")
                {
                    VerPostPropios = true;
                }
                else
                {
                    VerPostPropios = false;
                }
            }

            if (VerPostPropios)
            {
                if (UserProfile.publicacionesUser!=null)
                {
                    publicacionesMostradas = new ObservableCollection<ResponsePublicacionFeed>(UserProfile.publicacionesUser);
                }
            }
            else
            {
                if (_publicacionesGuardadas!=null)
                {
                    publicacionesMostradas = new ObservableCollection<ResponsePublicacionFeed>(_publicacionesGuardadas);
                }
            }

        }

        #endregion
    }
}
