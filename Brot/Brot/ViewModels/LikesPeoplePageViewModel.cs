using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace Brot.ViewModels
{
    public enum likeType
    {
        comentarios,
        publicacion
    }


    public class LikesPeoplePageViewModel : BaseViewModel
    {
        private likeType tipolike;
        private int id;

        public Models.ResponseApi.ResponseLikes likesRoot;

        private ObservableCollection<Models.userModel> _users;
        public ObservableCollection<Models.userModel> USUARIOS
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        public LikesPeoplePageViewModel(int id, ViewModels.likeType tipolike)
        {
            this.id = id;
            this.tipolike = tipolike;
        }

        private ICommand _RefreshCommand;
        public ICommand RefreshCommand => _RefreshCommand ??= new Xamarin.Forms.Command(async () => await RefreshMethodAsync());

        async System.Threading.Tasks.Task RefreshMethodAsync()
        {
            IsRefreshing = true;

            likesRoot = null;

            try
            {
                switch (tipolike)
                {
                    case likeType.comentarios:
                        likesRoot = await Services.RestAPI.GetLikesbyIDComentario(id);
                        break;

                    case likeType.publicacion:
                        likesRoot = await Services.RestAPI.GetLikesbyIDPost(id);
                        break;
                }

                if (likesRoot!=null)
                {
                    USUARIOS = new ObservableCollection<Models.userModel>();
                    foreach (var user in likesRoot.usuarios)
                    {
                        USUARIOS.Add(user);
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            

            IsRefreshing = false;
        }

    }
}
