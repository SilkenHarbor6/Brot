namespace Brot.Views
{
    using Models;
    using Models.ResponseApi;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellerProfile : ContentPage
    {
        SellerProfileViewModel ViewModel;
        public SellerProfile(SellerProfileViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = viewModel;
        }

        public SellerProfile(userModel usuarioPerfil)
        {
            InitializeComponent();
            BindingContext = this.ViewModel = new SellerProfileViewModel(usuarioPerfil);
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}