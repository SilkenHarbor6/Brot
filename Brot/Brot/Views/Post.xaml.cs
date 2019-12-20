namespace Brot.Views
{
    using Models.ResponseApi;
    using System.ComponentModel;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class Post : ContentPage
    {
        private PostViewModel ViewModel;

        public Post(PostViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = viewModel;
        }
        public Post()
        {
            InitializeComponent();

            BindingContext = ViewModel = new PostViewModel(new ResponsePublicacionFeed());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }


    }
}