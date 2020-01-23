﻿namespace Brot.ViewModels
{
    using Views;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Brot.Models;
    using Brot.Services;
    using DLL;
    using Brot.Patterns;
    using System;

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
                await Application.Current.MainPage.DisplayAlert("Error", "Los campos no pueden quedar vacios", "Aceptar");
                IsRefreshing = false;
                return;
            }
            if (password != spassword)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Las claves no coinciden", "Aceptar");
                IsRefreshing = false;
                return;
            }

            var valid = await RestClient.Post<userModel>("users/signupverify", new userModel() { email = Email });
            if (valid.IsSuccess)
            {
                IsRefreshing = false;
                var result = (codigoModel)valid.Result;
                var code = Convert.ToString(result.codigo);
                userModel user = new userModel();
                user.apellido = Apellido;
                user.nombre = Nombre;
                user.username = Username;
                user.email = Email;
                user.pass = Password;
                user.isActive = true;
                await App.Current.MainPage.Navigation.PushAsync(new SignUpVerify(user, code));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El correo ya ha sido registrado", "Aceptar");
            }
        }
        #endregion
    }
}
