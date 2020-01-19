using Brot.Models;
using Brot.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Brot.ViewModels
{
    public class EmailVerifyViewModel : BaseViewModel
    {
        private bool _emailverify;
        private bool _sendcode;
        private string _email;

        public bool EmailVerify
        {
            get { return _emailverify; }
            set { SetProperty(ref _emailverify, value); }
        }
        public bool SendCode
        {
            get { return _sendcode; }
            set { SetProperty(ref _sendcode, value); }
        }

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public EmailVerifyViewModel()
        {
            EmailVerify = true;
            SendCode = false;
        }

        public ICommand Send
        {
            get
            {
                return new Xamarin.Forms.Command(SendCod);
            }
        }

        private async void SendCod(object obj)
        {
            if (string.IsNullOrEmpty(Email))
            {
               await App.Current.MainPage.DisplayAlert("Error", "Ingresa tu correo", "Ok");
            }
            else
            {
                var resp = await RestClient.Post<userModel>("users/verify", new userModel() { email = Email });
                if (resp.IsSuccess)
                {
                    EmailVerify = false;
                    SendCode = true;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", resp.Message, "Ok");
                }
            }
        }
    }
}
