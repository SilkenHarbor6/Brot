namespace Brot.Views.Popups
{
    using ViewModels;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeName
    {
        private ChangeNameViewModel ViewModel;
        public ChangeName()
        {
            InitializeComponent();

            BindingContext = ViewModel = new ChangeNameViewModel();
        }
    }
}