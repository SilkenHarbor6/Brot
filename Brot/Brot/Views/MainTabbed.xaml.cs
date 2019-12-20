namespace Brot.Views
{
    using System.ComponentModel;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Views.Tabs;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class MainTabbed : TabbedPage
    { 
        public MainTabbed()
        {
            InitializeComponent();

            CurrentPage = Children[0];
        }
    }
}