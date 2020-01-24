namespace Brot.Models.ResponseApi
{
    using Brot.Patterns;
    using Brot.ViewModels;
    using Models;
    using Newtonsoft.Json;
    using System;
    using Services;
    using System.Diagnostics;
    using Xamarin.Forms;
    using System.Threading.Tasks;
    using DLL;

    public class ResponsePublicacionFeed : ObservableObject
    {

        public publicacionesModel publicacion { get; set; }
        public userModel UsuarioCreator { get; set; }
        public int cantComentarios { get; set; }
        public int cantLikes
        {
            get => _cantLikes;
            set => SetProperty(ref _cantLikes, value);
        }

        public Nullable<bool> IsLiked
        {
            get => _isliked;
            set => SetProperty(ref _isliked, value);
        }
        public Nullable<bool> IsSavedPost
        {
            get => _issaved;
            set => SetProperty(ref _issaved, value);
        }


        private bool? _isliked;
        private bool? _issaved;
        private int _cantLikes;
        private DownloadService ds;


        #region Opciones Command
        private Xamarin.Forms.Command _OpcionesCommand;
        public Xamarin.Forms.Command OpcionesCommand { get => _OpcionesCommand ??= new Xamarin.Forms.Command(OpcionesMethod); }

        private async void OpcionesMethod(object obj)
        {
            string respuesta = String.Empty;
            if (publicacion.isImg==true)
            {
                if (Singleton.Instance.User.id_user == publicacion.id_user) //Su propio comentario
                {
                    respuesta = await App.Current.MainPage.DisplayActionSheet("Opciones de comentario", "Atras", "Atras",
                    new string[] { "Editar", "Eliminar", "Dar Like", "Download Media" });
                }
                else
                {
                    respuesta = await App.Current.MainPage.DisplayActionSheet("Opciones de comentario", "Atras", "Atras",
                    new string[] { "Dar Like", "Guardar", "Download Media" });
                }
            }
            else
            {
                if (Singleton.Instance.User.id_user == publicacion.id_user) //Su propio comentario
                {
                    respuesta = await App.Current.MainPage.DisplayActionSheet("Opciones de comentario", "Atras", "Atras",
                    new string[] { "Editar", "Eliminar", "Dar Like" });
                }
                else
                {
                    respuesta = await App.Current.MainPage.DisplayActionSheet("Opciones de comentario", "Atras", "Atras`",
                    new string[] { "Dar Like", "Guardar" });
                }
            }
            switch (respuesta)
            {
                case "Editar":
                    await App.Current.MainPage.Navigation.PushAsync(new Views.EditarPublicacion(this));

                    break;
                case "Eliminar":
                    var resultDelete = await RestClient.Delete<publicacionesModel>(DLL.constantes.publicacionest, publicacion.id_post);
                    App.Current.MainPage = new NavigationPage(new Views.MainTabbed());
                    break;
                case "Dar Like":
                    await BtnLikedMethod("Like");
                    break;
                case "Guardar":
                    await BtnSavePostMethod("Guardar");
                    break;
                case "Download Media":
                    ds = new DownloadService();
                    ds.StartDownload(publicacion.img);
                    break;
                default:
                    break;
            }
        }
        #endregion
        


        #region Save Post
        private Command _btnSavePost;
        public Command BtnSavePostCommand => _btnSavePost ??= new Command(async () => await BtnSavePostMethod(null));
        private async Task BtnSavePostMethod(Object obj)
        {
            //Modified API! only Saved once! Do it as Likes Method
            var postsavedObject = new publicacion_guardadasModel()
            {
                id_post = publicacion.id_post,
                id_user = Singleton.Instance.User.id_user
            };
            if (obj != null) //Cuando da 2 taps al comentario
            {
                if ((bool)IsSavedPost)
                {
                    //Se quita y se vuelve a pponer, solo por la animacion minima
                    IsSavedPost = !IsSavedPost;
                    IsSavedPost = !IsSavedPost;
                }
                else
                {
                    //Se crea el Like
                    IsSavedPost = !IsSavedPost;
                    var y = await RestAPI.Post<publicacion_guardadasModel>(postsavedObject, "publicacion_guardada");
                }
                return;
            }
            if ((bool)IsSavedPost)
            {
                //Se quita el objeto
                IsSavedPost = !IsSavedPost;
                var x = await RestAPI.Put<publicacion_guardadasModel>(0, postsavedObject, "publicacion_guardada");
            }
            else
            {
                //se crea el objecto
                IsSavedPost = !IsSavedPost;
                var y = await RestAPI.Post<publicacion_guardadasModel>(postsavedObject, "publicacion_guardada");
            }
        }

        #endregion

        #region Like Buttom Clicked
        private Command _BtnLikedClicked;
        public Command BtnLikedClicked => _BtnLikedClicked ??= new Command(async () => await BtnLikedMethod(null));
        private async Task BtnLikedMethod(Object obj)
        {
            var likeObject = new like_postModel()
            {
                id_post = publicacion.id_post,
                id_user = Singleton.Instance.User.id_user
            };

            if (obj != null) //Cuando da 2 taps al comentario
            {
                if ((bool)IsLiked)
                {
                    //Se quita el like
                    IsLiked = !IsLiked;
                    IsLiked = !IsLiked;
                }
                else
                {
                    //Se crea el Like
                    IsLiked = !IsLiked;
                    cantLikes++;
                    await Brot.Services.RestClient.Post<like_postModel>(DLL.constantes.like_postt, likeObject);
                }
                return;
            }

            if ((bool)IsLiked)
            {
                //Se quita el like
                IsLiked = !IsLiked;
                cantLikes--;
                await RestClient.Post<like_postModel>("like_post/borrar", likeObject);
            }
            else
            {
                //se crea el like!
                IsLiked = !IsLiked;
                cantLikes++;
                await RestClient.Post<like_postModel>("like_post", likeObject);
            }
        }

        #endregion

        #region People who liked this!
        private Command _BtnLikesPeopleCommand;
        public Command BtnLikesPeopleCommand => _BtnLikesPeopleCommand ??= new Command(async () => await LikesPeopleMethod());
        private async Task LikesPeopleMethod()
        {
            if (cantLikes > 0)
            {
                await App.Current.MainPage.Navigation.PushAsync(new Views.LikesPeoplePage(publicacion.id_post, ViewModels.likeType.publicacion));
            }
        }

        #endregion




    }
}
