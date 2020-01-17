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

        public Post(int idPost)
        {
            InitializeComponent();

            BindingContext = ViewModel = new PostViewModel(idPost);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            ((Brot.Models.ResponseApi.ResponseComentarios)e.SelectedItem).usuario.BtnProfileNameClicked.Execute(null);
            ((ListView)sender).SelectedItem = null;
        }
    }
}