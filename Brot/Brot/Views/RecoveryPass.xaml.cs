using Brot.ViewModels;
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
    public partial class RecoveryPass : ContentPage
    {
        public RecoveryPass(string id)
        {
            InitializeComponent();
            BindingContext = new RecoveryPassViewModel(id);
        }
    }
}