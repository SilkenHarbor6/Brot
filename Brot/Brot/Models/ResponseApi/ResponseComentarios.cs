namespace Brot.Models.ResponseApi
{
    using Brot.ViewModels;
    using Models;
    using System.Threading.Tasks;
    using Rg.Plugins;
    using System;
    using Brot.Patterns;
    using Brot.Services;

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



        private int _cantLikes;
        private bool _isliked;

        #region Opciones Command
        private Xamarin.Forms.Command _OpcionesCommand;
        public Xamarin.Forms.Command OpcionesCommand { get => _OpcionesCommand ??= new Xamarin.Forms.Command(OpcionesMethod); }

        private async void OpcionesMethod(object obj)
        {
            string respuesta = String.Empty;
            if (Singleton.Instance.User.id_user == comentario.id_user) //Su propio comentario
            {
                respuesta = await App.Current.MainPage.DisplayActionSheet("Opciones de comentario", "Atras", "",
                new string[] { "Editar", "Eliminar", "Dar Like" });
            }
            else
            {
                respuesta = await App.Current.MainPage.DisplayActionSheet("Opciones de comentario", "Atras", "",
                new string[] { "Dar Like" });
            }
            switch (respuesta)
            {
                case "Editar":
                    await App.Current.MainPage.Navigation.PushAsync(new Views.EditarComentario(comentario));

                    break;
                case "Eliminar":
                    var resultDelete = await RestClient.Delete<comentariosModel>(DLL.constantes.comentariost, comentario.id_comentario);
                    await App.Current.MainPage.Navigation.PopAsync();
                    break;
                case "Dar Like":
                    BtnLikedMethod("Like");
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region LikeButtom Clicked

        private Xamarin.Forms.Command _BtnLikedClicked;
        public Xamarin.Forms.Command BtnLikedClicked
        {
            get => _BtnLikedClicked ??= new Xamarin.Forms.Command(BtnLikedMethod);
        }
        private async void BtnLikedMethod(object obj)
        {
            var likeObject = new like_comentarioModel()
            {
                id_comentario = comentario.id_comentario,
                id_user = Brot.Patterns.Singleton.Instance.User.id_user
            };

            if (obj != null) //Cuando da 2 taps al comentario
            {
                if (isLiked)
                {
                    //Se quita el like
                    isLiked = !isLiked;
                    isLiked = !isLiked;
                }
                else
                {
                    //Se crea el Like
                    isLiked = !isLiked;
                    CantLikes++;
                    await Brot.Services.RestClient.Post<like_comentarioModel>("like_comentario", likeObject);
                }
                return;
            }

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
        #endregion

        #region See People who liked this!
        private Xamarin.Forms.Command _BtnLikesPeopleCommand;
        public Xamarin.Forms.Command BtnLikesPeopleCommand => _BtnLikesPeopleCommand ??= new Xamarin.Forms.Command(async () => await LikesPeopleMethod());
        private async System.Threading.Tasks.Task LikesPeopleMethod()
        {
            if (CantLikes > 0)
            {
                await App.Current.MainPage.Navigation.PushAsync(new Views.LikesPeoplePage(comentario.id_comentario, ViewModels.likeType.comentarios));
            }
        }

        #endregion

    }
}
