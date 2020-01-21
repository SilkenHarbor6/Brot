namespace Brot.Views
{
    using System.ComponentModel;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class Post : ContentPage
    {

        public Post(PostViewModel viewModel, int id_User)
        {
            InitializeComponent();

            Brot.Patterns.Singleton.Instance.id_UserCreator_post = id_User;
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            ((Models.ResponseApi.ResponseComentarios)e.SelectedItem).usuario.BtnProfileNameClicked.Execute(null);
            ((ListView)sender).SelectedItem = null;
        }
    }
}