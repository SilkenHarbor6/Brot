namespace Brot.Views.Tabs
{
    using ViewModels;
    using System;
    using System.ComponentModel;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;


    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class Feed : ContentPage
    {
        FeedViewModel ViewModel;
        public Feed()
        {
            InitializeComponent();
            BindingContext = ViewModel = new FeedViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((FeedViewModel)BindingContext).selectedItemLista = null;
        }
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            ImageButton LikePressed = (sender as ImageButton);

            if (int.TryParse(LikePressed.ClassId, out int idLikeButton))
            {
                this.ViewModel.LikeCommand.Execute(idLikeButton);
            }
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}