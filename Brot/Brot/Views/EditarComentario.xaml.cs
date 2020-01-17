
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Brot.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarComentario : ContentPage
    {
        public EditarComentario(ViewModels.EditarComentarioVM VM)
        {
            InitializeComponent();
            BindingContext = VM;
        }
    }
}