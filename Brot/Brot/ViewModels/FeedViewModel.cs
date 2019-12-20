
namespace Brot.ViewModels
{
    using Models;
    using Brot.Patterns;
    using Brot.Services;
    using Models.ResponseApi;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Text;
    using System.Windows.Input;
    using Views;
    using System.Threading.Tasks;

    public class FeedViewModel : BaseViewModel
    {
        #region Attributes

        private ObservableCollection<ResponsePublicacionFeed> _lPosts;
        private String _texto;
        private bool _isVendor;
        private ResponsePublicacionFeed _selectedItemLista;
        #endregion

        #region Properties

        public bool IsVendor
        {
            get { return _isVendor; }
            set { SetProperty(ref _isVendor, value); }
        }
        public String texto
        {
            get => _texto;
            set => SetProperty(ref _texto, value);
        }
        public ResponsePublicacionFeed selectedItemLista
        {
            get { return _selectedItemLista; }
            set
            {
                if (value != null)
                {
                    App.Current.MainPage.Navigation.PushAsync(new Post(new PostViewModel(value)));
                    SetProperty(ref _selectedItemLista, null);
                }
            }
        }
        public ObservableCollection<ResponsePublicacionFeed> lPosts
        {
            get { return this._lPosts; }
            set => SetProperty(ref _lPosts, value);
        }

        #endregion

        #region Constructor

        public FeedViewModel()
        {
            IsVendor = Singleton.Instance.User.isVendor;
            this.lPosts = new ObservableCollection<ResponsePublicacionFeed>();
            LoadFeed();
        }

        #endregion

        #region Commands
        public ICommand PostSomething
        {
            get
            {
                return new Xamarin.Forms.Command(AddPost);
            }
        }
        public ICommand takePhoto
        {
            get
            {
                return new Xamarin.Forms.Command(Singleton.Instance.ChangePic);
            }
        }
        public ICommand RefreshCommand
        {
            get { return new Xamarin.Forms.Command(Refresh); }
        }

        public ICommand LikeCommand
        {
            get
            {
                return new Xamarin.Forms.Command<int>(Like);
            }
        }

        #endregion

        #region Methods

        public async void Refresh()
        {
            if (IsRefreshing)
                return;

            IsRefreshing = true;

            try
            {
                this.lPosts.Clear();
                LoadFeed();
            }
            catch (Exception)
            {
                await Singleton.Instance.Dialogs.Message("Is busy", "Couldn't load items");
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        private async void Like(int idLike)
        {
            await App.Current.MainPage.DisplayAlert("EXITO", "Has presionado el boton " + idLike, "Ok");
        }

        public async void LoadFeed()
        {
            IsRefreshing = true;
            var result = await RestClient.GetAll<ResponsePublicacionFeed>($"publicaciones/all/{Singleton.Instance.User.id_user}/");

            if (!result.IsSuccess)
            {
                await Singleton.Instance.Dialogs.Message("There was a problem trying to get the feed", result.Message);
                return;
            }

            foreach (var post in (ObservableCollection<ResponsePublicacionFeed>)result.Result)
            {
                if (string.IsNullOrEmpty(post.publicacion.img))
                {
                    post.publicacion.img = DLL.constantes.ProfileImageError;
                }
                else
                {
                    post.publicacion.img = DLL.constantes.urlImages + post.publicacion.img;
                }

                if (string.IsNullOrEmpty(post.UsuarioCreator.img))
                {
                    post.UsuarioCreator.img = DLL.constantes.ProfileImageError;
                }
                else
                {
                    post.UsuarioCreator.img = DLL.constantes.urlImages + post.UsuarioCreator.img;

                }
                this.lPosts.Add(post);
            }
            IsRefreshing = false;
        }
        public async void AddPost()
        {
            IsRefreshing = true;
            publicacionesModel niu = new publicacionesModel();
            niu.fecha_creacion = DateTime.Now;
            niu.id_user = Singleton.Instance.User.id_user;
            if (PickPhotoAsync.name == null)
            {
                niu.img = null;
                niu.isImg = false;
            }
            else
            {
                niu.img = PickPhotoAsync.name;
                niu.isImg = true;
            }
            niu.descripcion = texto;
            niu.isDeleted = false;
            niu.fecha_actualizacion = null;
            texto = "";
            PickPhotoAsync.name = null;
            Response resp = await RestClient.Post<publicacionesModel>("publicaciones", niu);
            if (!resp.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", resp.Message, "Aceptar");
            }
            if (niu.img == null)
            {
                await Task.Delay(10000);
            }
            else
            {
                await Task.Delay(25000);
            }

            lPosts = new ObservableCollection<ResponsePublicacionFeed>();
            LoadFeed();
        }


        #endregion
    }
}
