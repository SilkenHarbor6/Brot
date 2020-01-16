namespace Brot.Views
{
    using System.ComponentModel;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Views.Tabs;
    using Plugin.Permissions.Abstractions;
    using Plugin.Permissions;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class MainTabbed : TabbedPage
    { 
        public MainTabbed()
        {
            ask4Location();
            InitializeComponent();
            CurrentPage = Children[0];

        }
        private async void ask4Location()
        {
            await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
        }
    }
}