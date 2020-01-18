
namespace Brot.ViewModels
{
    using Brot.Patterns;
    using Brot.Services;
    using Brot.Views;
    using Models;
    using System.Collections.Generic;
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

                // TODO Toast de login
                //Toast.MakeText(Android.App.Application.Context, $"Bienvenido {Singleton.Instance.User.username}", ToastLength.Short).Show();
                Microsoft.AppCenter.Analytics.Analytics.TrackEvent("Successful Login", new System.Collections.Generic.Dictionary<string, string>()
                {
                    {"User_ID",Singleton.Instance.User.id_user.ToString() },
                    {"Usuario",Singleton.Instance.User.nombre }
                });

                Microsoft.AppCenter.AppCenter.Start("android=ce90d30b-e395-4d05-be5b-a1461a3bec8e;" +
                         "ios=0caa730c-a7e0-45b2-82bb-302f376b133d",
                          typeof(Microsoft.AppCenter.Push.Push));


                Application.Current.MainPage = new NavigationPage(new MainTabbed());
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
