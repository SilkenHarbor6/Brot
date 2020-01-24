

namespace Brot.ViewModels
{
    using Brot.Patterns;
    using Models;
    using Models.ResponseApi;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Text;
    using System.Windows.Input;

    public class PostViewModel : BaseViewModel
    {
        #region Propiedades
        private int idPublicacion;
        private ObservableCollection<ResponseComentarios> _comentariosData;
        private ResponsePublicacion _Post;
        public ObservableCollection<ResponseComentarios> ComentariosData
        {
            get { return _comentariosData; }
            set
            {
                if (_comentariosData != value)
                {
                    _comentariosData = value;
                    OnPropertyChanged("ComentariosData");
                }
            }
        }
        private bool _isActivityActive;
        public bool isActivityActive
        {
            get { return _isActivityActive; }
            set
            {
                if (_isActivityActive != value)
                {
                    _isActivityActive = value;
                    OnPropertyChanged("isActivityActive");
                }
            }
        }

        public ResponsePublicacion Post
        {
            get { return _Post; }
            set
            {
                if (_Post != value)
                {
                    _Post = value;
                    OnPropertyChanged("Post");
                }
            }
        }
        private string _texto;
        public string texto
        {
            get { return _texto; }
            set
            {
                if (_texto != value)
                {
                    _texto = value;
                    OnPropertyChanged("texto");
                }
            }
        }

        private bool _footerVisible;
        public bool FooterVisible
        {
            get => _footerVisible;
            set => SetProperty(ref _footerVisible, value);
        }
        #endregion

        public PostViewModel(ResponsePublicacionFeed postFeed)
        {
            isActivityActive = true;
            Post = new ResponsePublicacion();
            ComentariosData = new ObservableCollection<ResponseComentarios>();
            //Esto lo hago para que ya tenga los datos del Feed mientras están cargando los demás datos como comentarios
            Post.publicacion = postFeed;
            Post.comentarios = new List<ResponseComentarios>();
            if (postFeed.cantComentarios>0)
            {
                FooterVisible = false;
            }
            else
            {
                FooterVisible = true;
            }
            idPublicacion = Post.publicacion.publicacion.id_post;
            CargarDatos();
            isActivityActive = false;
            IsRefreshing = false;
        }
        public PostViewModel(int idPost)
        {
            idPublicacion = idPost;
            IsRefreshing = true;
            CargarDatos();
            IsRefreshing = false;
        }


        #region Commands

        public ICommand RefreshCommand
        {
            get
            {
                return new Xamarin.Forms.Command(() =>
          {
              IsRefreshing = true;
              CargarDatos();
              IsRefreshing = false;

          });
            }
        }

        private async void CargarDatos()
        {
            try
            {

                ResponsePublicacion publicacionData = await RestAPI.Getpublicacion(idPublicacion, Singleton.Instance.User.id_user);
                if (publicacionData != null)
                {
                    publicacionData.publicacion.publicacion.img = DLL.constantes.urlImages + publicacionData.publicacion.publicacion.img;
                    //Imagen del usuario
                    if (string.IsNullOrEmpty(publicacionData.publicacion.UsuarioCreator.img))
                    {
                        publicacionData.publicacion.UsuarioCreator.img = DLL.constantes.ProfileImageError;
                    }
                    else
                    {
                        publicacionData.publicacion.UsuarioCreator.img = DLL.constantes.urlImages + publicacionData.publicacion.UsuarioCreator.img;
                    }

                    Post = publicacionData;
                    if (Post.comentarios.Count > 0)
                    {
                        FooterVisible = false;
                        //Siempre validar cuando instanciamos un Observable Collection porque por default es null
                        //ObservableCollection<ResponseComentarios>(null)    DA ERROR!!
                        for (int i = 0; i < Post.comentarios.Count; i++)
                        {
                            //imagen del usuario
                            if (string.IsNullOrEmpty(Post.comentarios[i].usuario.img))
                            {
                                Post.comentarios[i].usuario.img = DLL.constantes.ProfileImageError;
                            }
                            else
                            {
                                Post.comentarios[i].usuario.img = DLL.constantes.urlImages + Post.comentarios[i].usuario.img;
                            }

                        }
                        ComentariosData = new ObservableCollection<ResponseComentarios>(Post.comentarios);
                    }
                    else
                    {
                        FooterVisible = true;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Print($"Error en postViewmodel  --{e.Message}");
            }
        }


        private Xamarin.Forms.Command _ComentarCommand;
        public Xamarin.Forms.Command ComentarCommand
        {
            get => _ComentarCommand ?? (_ComentarCommand = new Xamarin.Forms.Command(comentar));
        }
        private async void comentar()
        {
            isActivityActive = true;
            comentariosModel coment = new comentariosModel()
            {
                contenido = texto,
                id_user = Singleton.Instance.User.id_user,
                id_post = Post.publicacion.publicacion.id_post
            };
            var result = await RestAPI.Post<comentariosModel>(coment, DLL.constantes.comentariost);
            if (result)
            {
                FooterVisible = false;
                CargarDatos();
                texto = "";
            }

            isActivityActive = false;
        }






        #endregion



    }
}
