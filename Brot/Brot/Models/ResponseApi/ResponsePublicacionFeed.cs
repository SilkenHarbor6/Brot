namespace Brot.Models.ResponseApi
{
    using Brot.Patterns;
    using Brot.ViewModels;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using Services;
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
        public Nullable<bool> IsSavedPost { get; set; }


        [JsonIgnore]
        private Xamarin.Forms.Command _BtnLikedClicked;
        [JsonIgnore]
        private Xamarin.Forms.Command _BtnProfileNameClicked;
        [JsonIgnore]
        private bool? _isliked;
        [JsonIgnore]
        private int _cantLikes;

        [JsonIgnore]
        public Xamarin.Forms.Command BtnLikedClicked
        {
            get => _BtnLikedClicked ??= new Xamarin.Forms.Command(BtnLikedMethod);
        }
        [JsonIgnore]
        public Xamarin.Forms.Command BtnProfileNameClicked
        {
            get => _BtnProfileNameClicked ??= new Xamarin.Forms.Command(BtnProfileMethod);
        }

        private void BtnProfileMethod(object obj)
        {
            App.Current.MainPage.Navigation.PushAsync(new Views.SellerProfile(this.UsuarioCreator));
        }

        private async void BtnLikedMethod(object obj)
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

    }
}
