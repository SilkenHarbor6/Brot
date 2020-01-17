using Brot.Models;
using Brot.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Brot.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class mapPopup
    {
        public mapPopup(userModel item)
        {
            InitializeComponent();
            BindingContext = new mapPopupViewModel(item);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
        }
    }
}