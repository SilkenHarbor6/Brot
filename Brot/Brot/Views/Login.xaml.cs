namespace Brot.Views
{
    using System.ComponentModel;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;


    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class Login : ContentPage
    {
        LoginViewModel ViewModel;
        public Login()
        {
            InitializeComponent();

            BindingContext = this.ViewModel = new LoginViewModel();
        }
    }
}