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




        #region Save Post
        private Command _btnSavePost;
        public Command BtnSavePostCommand => _btnSavePost ??= new Command(async () => await BtnSavePostMethod());
        private async Task BtnSavePostMethod()
        {
            //Modified API! only Saved once! Do it as Likes Method
            var postsavedObject = new publicacion_guardadasModel()
            {
                id_post = publicacion.id_post,
                id_user = Singleton.Instance.User.id_user
            };

            if ((bool)IsSavedPost)
            {
                //Se quita el objeto
                IsSavedPost = !IsSavedPost;
                var x = await RestAPI.Put<publicacion_guardadasModel>(0,postsavedObject, "publicacion_guardada");
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
        public Command BtnLikedClicked => _BtnLikedClicked ??= new Command(async () => await BtnLikedMethod());
        private async Task BtnLikedMethod()
        {
            var likeObject = new like_postModel()
            {
                id_post = publicacion.id_post,
                id_user = Singleton.Instance.User.id_user
            };
            
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
            if (cantLikes>0)
            {
                await App.Current.MainPage.Navigation.PushAsync(new Views.LikesPeoplePage(publicacion.id_post, ViewModels.likeType.publicacion));
            }
        }

        #endregion




    }
}
