using Brot.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Brot.Views.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchBrots : ContentPage
    {
        public SearchBrots()
        {
            InitializeComponent();
            BindingContext = new SearchBrotsViewModel();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}