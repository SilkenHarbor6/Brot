namespace Brot.Views.Popups
{
    using ViewModels;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeLastname
    {
        private ChangeLastnameViewModel ViewModel;
        public ChangeLastname()
        {
            InitializeComponent();

            BindingContext = ViewModel = new ChangeLastnameViewModel();
        }
    }
}