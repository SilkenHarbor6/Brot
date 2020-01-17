
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Brot.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarComentario : ContentPage
    {
        public EditarComentario(Models.comentariosModel comentario)
        {
            InitializeComponent();
            BindingContext = new ViewModels.EditarComentarioVM(comentario);
        }
    }
}