

namespace Brot.ViewModels
{
    using Views;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class SignupViewModel : BaseViewModel
    {
        public ICommand LoginCommand
        {
            get
            {
                return new Xamarin.Forms.Command(LoginUser);
            }
        }

        public ICommand GoToMainCommand
        {
            get
            {
                return new Xamarin.Forms.Command(GoToMainPage);
            }
        }


        #region Commands

        public void LoginUser()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public void GoToMainPage()
        {
            Application.Current.MainPage = new NavigationPage(new Master());
        }

        #endregion
    }
}
