namespace Brot.Views.Tabs
{
    using System.ComponentModel;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;



    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class Profile : ContentPage
    {
        private ProfileViewModel ViewModel;
        public Profile()
        {
            InitializeComponent();

            BindingContext = this.ViewModel = new ProfileViewModel();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}