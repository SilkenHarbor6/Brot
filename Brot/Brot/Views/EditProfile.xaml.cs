namespace Brot.Views
{
    using Brot.ViewModels;
    using System.ComponentModel;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;


    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class EditProfile : ContentPage
    {
        public EditProfile()
        {
            InitializeComponent();
            BindingContext = new EditProfileViewModel();
        }
    }
}