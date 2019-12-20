namespace Brot.Views.Tabs
{
    using Models.ResponseApi;
    using System.ComponentModel;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;



    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class BrotTen : ContentPage
    {
        private BrotTenViewModel ViewModel;
        public BrotTen()
        {
            InitializeComponent();

            BindingContext = ViewModel = new BrotTenViewModel();
        }

        private async void FeedListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selected = e.SelectedItem as ResponseUsuariosFiltro;

            if (selected == null)
                return;

            //await Navigation.PushAsync(new SellerProfile(new SellerProfileViewModel(item)));
            await Navigation.PushAsync(new SellerProfile(new SellerProfileViewModel(selected.userData)));

            //Se quita la seleccion del item
            ((ListView)sender).SelectedItem = null;
        }
    }
}