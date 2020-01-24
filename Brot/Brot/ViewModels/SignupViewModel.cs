namespace Brot.ViewModels
{
    using Views;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Brot.Models;
    using Brot.Services;
    using DLL;
    using Brot.Patterns;
    using System;
    using System.Net.Http;

    public class SignupViewModel : BaseViewModel
    {
        #region Atributos
        private string nombre;
        private string apellido;
        private string username;
        private string email;
        private string password;
        private string spassword;
        #endregion
        #region Propiedades
        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                SetProperty(ref nombre, value);
            }
        }
        public string Apellido
        {
            get
            {
                return apellido;
            }
            set
            {
                SetProperty(ref apellido, value);
            }
        }
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                SetProperty(ref username, value);
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                SetProperty(ref email, value);
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                SetProperty(ref password, value); Singleton.passw = value;
            }
        }
        public string RepeatedPassword
        {
            get
            {
                return spassword;
            }
            set
            {
                SetProperty(ref spassword, value);
            }
        }

        private bool _correctPassword;
        public bool CorrectPassword
        {
            get { return _correctPassword; }
            set { SetProperty(ref _correctPassword, value); }
        }
        #endregion
        #region Comandos
        public ICommand LoginCommand
        {
            get
            {
                return new Xamarin.Forms.Command(LoginUser);
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                return new Xamarin.Forms.Command(Register);
            }
        }
        #endregion
        #region Metodos

        public void LoginUser()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public void GoToMainPage()
        {
            Application.Current.MainPage = new NavigationPage(new Master());
        }
        public async void Register()
        {
            IsRefreshing = true;
            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Apellido) || string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", "Los campos no pueden quedar vacios", "Aceptar");
                return;
            }
            if (password != spassword)
            {
                IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", "Las claves no coinciden", "Aceptar");
                return;
            }
            string code= string.Empty;
            using (var cliente = new HttpClient())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(new userModel() { email = Email });
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await cliente.PostAsync($"http://brotmainapi.azurewebsites.net/api/users/signupverify", content);
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var codigo = Newtonsoft.Json.JsonConvert.DeserializeObject<codigoModel>(await response.Content.ReadAsStringAsync());
                        code = codigo.codigo;
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }

            if (!String.IsNullOrEmpty(code))
            {
                IsRefreshing = false;
                userModel user = new userModel();
                user.apellido = Apellido;
                user.nombre = Nombre;
                user.username = Username;
                user.email = Email;
                user.pass = Password;
                user.isActive = true;
                IsRefreshing = false;
                await App.Current.MainPage.Navigation.PushAsync(new SignUpVerify(user, code));
            }
            else
            {
                IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", "El correo ya ha sido registrado ", "Aceptar");
            }
        }
        #endregion
    }
}
