using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Brot.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarPublicacion : ContentPage
    {
        public EditarPublicacion(Models.ResponseApi.ResponsePublicacionFeed post)
        {
            InitializeComponent();
            Patterns.Singleton.Instance.id_UserCreator_post = post.publicacion.id_user;
            BindingContext = new ViewModels.EditarPublicacionVM(post);

        }
    }
}