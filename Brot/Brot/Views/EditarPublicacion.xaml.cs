using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            BindingContext = new ViewModels.EditarPublicacionVM(post);

        }
    }
}