

namespace Brot.ViewModels
{
    using Brot.Patterns;
    using Brot.Views;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class ProfileViewModel : BaseViewModel
    {
        #region Attributes

        public ICommand EditProfileCommand
        {
            get { return new Command(EditProfile); }
        }
        public ICommand SignOutCommand { get { return new Command(Signout); } }

      

        #endregion

        #region Methods

        private void EditProfile()
        {
            Application.Current.MainPage.Navigation.PushAsync(new EditProfile());
        }
        private void Signout()
        {
            Singleton.Instance.LocalJson.SignOut();
            App.Current.MainPage = new NavigationPage(new Login());
        }


        #endregion
    }
}
