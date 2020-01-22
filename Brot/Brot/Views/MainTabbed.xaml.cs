namespace Brot.Views
{
    using System.ComponentModel;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class MainTabbed : TabbedPage
    {
        public MainTabbed()
        {
            InitializeComponent();
            Children.Add(new Tabs.Feed());
            Children.Add(new Tabs.SellersMap());
            Children.Add(new Tabs.BrotsTabbedxaml());
            //Children.Add(new Tabs.BrotTen());
            Children.Add(new Tabs.Profile());

            //CurrentPage = Children[1];

        }
    }
}