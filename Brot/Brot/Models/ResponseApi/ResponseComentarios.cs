namespace Brot.Models.ResponseApi
{
    using Brot.ViewModels;
    using Models;
    using Newtonsoft.Json;

    public class ResponseComentarios : ObservableObject
    {
        
        public comentariosModel comentario { get; set; }
        public userModel usuario { get; set; }
        public int CantLikes
        {
            get => _cantLikes;
            set => SetProperty(ref _cantLikes, value);
        }
        public bool isLiked
        {
            get => _isliked;
            set => SetProperty(ref _isliked, value);
        }



        [JsonIgnore]
        private int _cantLikes;
        [JsonIgnore]
        private bool _isliked;
        [JsonIgnore]
        private Xamarin.Forms.Command _BtnLikedClicked;
        [JsonIgnore]
        private Xamarin.Forms.Command _BtnProfileNameClicked;


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
            App.Current.MainPage.Navigation.PushAsync(new Views.SellerProfile(this.usuario));
        }
        private async void BtnLikedMethod(object obj)
        {
            var likeObject = new like_comentarioModel()
            {
                id_comentario = comentario.id_comentario,
                id_user = Brot.Patterns.Singleton.Instance.User.id_user
            };
            if (isLiked)
            {
                //Se quita el like
                isLiked = !isLiked;
                CantLikes--;
                await Brot.Services.RestClient.Post<like_comentarioModel>("like_comentario/borrar", likeObject);
            }
            else
            {
                //Se crea el Like
                isLiked = !isLiked;
                CantLikes++;
                await Brot.Services.RestClient.Post<like_comentarioModel>("like_comentario", likeObject);
            }
        }
    }
}
