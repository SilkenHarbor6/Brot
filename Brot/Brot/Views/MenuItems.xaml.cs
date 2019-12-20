namespace Brot.Views
{
    using ViewModels;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuItems : ContentPage
    {
        private MenuItemsViewModel ViewModel;
        public MenuItems()
        {
            InitializeComponent();

            BindingContext = ViewModel = new MenuItemsViewModel();
        }
    }
}