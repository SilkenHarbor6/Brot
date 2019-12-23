using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Brot.Views
{
    public partial class LikesPeoplePage : ContentPage
    {
        public LikesPeoplePage(int id, ViewModels.likeType tipolike)
        {
            InitializeComponent();
            BindingContext = new ViewModels.LikesPeoplePageViewModel(id, tipolike);
        }

        private async  void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var UserSelected = e.SelectedItem as Models.userModel;

            if (UserSelected == null)
                return;

            await Navigation.PushAsync(new SellerProfile(new ViewModels.SellerProfileViewModel(UserSelected)));

            //Se quita la seleccion del item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
