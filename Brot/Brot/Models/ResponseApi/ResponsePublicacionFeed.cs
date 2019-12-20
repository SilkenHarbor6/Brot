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


        private Command _BtnLikedClicked;
        private Command _BtnProfileNameClicked;
        private Command _BtnLikesPeopleCommand;
        private bool? _isliked;
        private bool? _issaved;
        private int _cantLikes;


        public Command BtnLikedClicked => _BtnLikedClicked ??= new Command(async () => await BtnLikedMethod());
        public Command BtnProfileNameClicked => _BtnProfileNameClicked ??= new Command(async () => await BtnProfileMethod());
        public Command BtnLikesPeopleCommand => _BtnLikesPeopleCommand ??= new Command(async() => await LikesPeopleMethod()); 

        private async Task LikesPeopleMethod()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Views.LikesPeoplePage(publicacion.id_post, ViewModels.likeType.publicacion));
        }

        private async Task BtnProfileMethod()
        {
            await App.Current.MainPage.Navigation.PushAsync(new Views.SellerProfile(this.UsuarioCreator));
        }

        private async Task BtnLikedMethod()
        {
            var likeObject = new like_postModel()
            {
                id_post = publicacion.id_post,
                id_user = Singleton.Instance.User.id_user
            };
            Stopwatch reloj = new Stopwatch();
            reloj.Start();
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
            reloj.Stop();
            Console.WriteLine(reloj.Elapsed.TotalSeconds);
        }

    }
}
