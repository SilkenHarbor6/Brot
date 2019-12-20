

namespace Brot.ViewModels
{
    using Brot.Patterns;
    using Brot.Views;
    using Brot.Views.Popups;
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class MenuItemsViewModel : BaseViewModel
    {
        #region Commands

        public ICommand SignoutCommand
        {
            get
            {
                return new Xamarin.Forms.Command(Signout);
            }
        }
        public ICommand ChangeNameCommand
        {
            get
            {
                return new Xamarin.Forms.Command(ChangeName);
            }
        }
        public ICommand ChangeLastnameCommand
        {
            get
            {
                return new Xamarin.Forms.Command(ChangeLastname);
            }
        }
        public ICommand ChangeDescriptionCommand
        {
            get
            {
                return new Xamarin.Forms.Command(ChangeDescription);
            }
        }

        #endregion

        #region Constructor


        #endregion

        #region Methods

        private void Signout()
        {
            if (Singleton.Instance.LocalJson.IsUserLogged())
                Singleton.Instance.LocalJson.SignOut();

            Singleton.Instance.User = null;
            Application.Current.MainPage = new NavigationPage(new Login());
        }

        private async void ChangeDescription()
        {
            await PopupNavigation.PushAsync(new ChangeDescription());
        }

        private async void ChangeLastname()
        {
            await PopupNavigation.PushAsync(new ChangeLastname());
        }

        private async void ChangeName()
        {
            await PopupNavigation.PushAsync(new ChangeName());
        }

        #endregion
    }
}
