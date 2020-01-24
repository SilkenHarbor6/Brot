using Brot.Models;
using Brot.Services;
using DLL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Brot.ViewModels
{
    public class SignUpVerifyViewModel : BaseViewModel
    {

        private string _code;
        private bool _isEnabled;
        private string _currentCode;
        private userModel _item;
        public string Code
        {
            get { return _code; }
            set { SetProperty(ref _code, value); }
        }
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }

        public SignUpVerifyViewModel(userModel item, string currentCode)
        {
            _item = item;
            _currentCode = currentCode;
        }

        public ICommand Send
        {
            get
            {
                return new Xamarin.Forms.Command(SendCode);
            }
        }

        private async void SendCode(object obj)
        {
            IsRefreshing = true;
            IsEnabled = false;
            if (Code == _currentCode)
            {
                var resp = await RestClient.Post4Reg<userModel>(constantes.userst, _item);
                if (!resp.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("No se ha podido registrar el usuario", resp.Message, "Aceptar");
                    IsRefreshing = false;
                    return;
                }
                await Application.Current.MainPage.DisplayAlert("Exito", "El usuario ha sido registrado", "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
                IsRefreshing = false;
            }
        }
    }
}
