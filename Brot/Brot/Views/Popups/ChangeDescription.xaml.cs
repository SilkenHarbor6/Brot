namespace Brot.Views.Popups
{
    using ViewModels;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeDescription
    {
        private ChangeDescriptionViewModel ViewModel;
        public ChangeDescription()
        {
            InitializeComponent();

            BindingContext = ViewModel = new ChangeDescriptionViewModel();
        }
    }
}