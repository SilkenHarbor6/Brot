using Brot.Models;
using Brot.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace Brot.ViewModels
{
    public class mapPopupViewModel:BaseViewModel
    {
        #region Atributos
        private userModel _seller;
        #endregion
        public userModel seller
        {
            get
            {
                return _seller;
            }
            set
            {
                _seller = value;OnPropertyChanged("seller");
            }
        }
        public ICommand frmClicked
        {
            get
            {
                return new Xamarin.Forms.Command(goToProfile);
            }
        }

        private async void goToProfile()
        {
            PopupNavigation.PopAsync();
            await App.Current.MainPage.Navigation.PushAsync(new SellerProfile(new SellerProfileViewModel(seller)));
        }

        public mapPopupViewModel(userModel item)
        {
            seller = item;
        }
    }
}
