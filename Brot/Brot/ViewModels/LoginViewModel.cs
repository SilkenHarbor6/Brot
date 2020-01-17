
namespace Brot.ViewModels
{
    using Brot.Patterns;
    using Brot.Services;
    using Brot.Views;
    using Models;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        #region Attributes

        private string _Username;
        private string _Password;
        private bool _Remember = true; //True por defecto

        #endregion

        #region Properties

        public string Username
        {
            get { return this._Username; }
            set
            {
                if (this._Username == value)
                    return;

                this._Username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return this._Password; }
            set
            {
                if (this._Password == value)
                    return;

                this._Password = value;
                OnPropertyChanged();
            }
        }

        public bool Remember
        {
            get { return this._Remember; }
            set
            {
                if (this._Remember == value)
                    return;

                this._Remember = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand SignupCommand
        {
            get
            {
                return new Xamarin.Forms.Command(RegisterUser);
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                return new Xamarin.Forms.Command(Login);
            }
        }

        #endregion

        #region Methods

        private void RegisterUser()
        {
            Application.Current.MainPage.Navigation.PushAsync(new Signup());
        }

        private async void Login()
        {
            IsRefreshing = true;
            if (string.IsNullOrEmpty(this.Username) || string.IsNullOrEmpty(this.Password))
            {
                await Singleton.Instance.Dialogs.Message("Error", "You must fill all fields");
                IsRefreshing = false;
                return;
            }

            var result = await RestClient.Post<userModel>("users/login", new userModel()
            {
                username = this.Username,
                pass = this.Password
            });

            if (result.IsSuccess)
            {
                Singleton.Instance.User = (userModel)result.Result;

                //if (this.Remember)
                    Singleton.Instance.LocalJson.SaveData((userModel) result.Result);

                //Toast.MakeText(Android.App.Application.Context, $"Bienvenido {Singleton.Instance.User.username}", ToastLength.Short).Show();
                Application.Current.MainPage = new NavigationPage(new Master());
            }
            else
            {
                await Singleton.Instance.Dialogs.Message("Server connection error", result.Message);
            }
            IsRefreshing = false;

        }

        #endregion
    }
}
