using Brot.Models;
using Brot.Services;
using Brot.Views;
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
        private string _code;

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

        public string Code
        {
            get { return _code; }
            set { SetProperty(ref _code, value); }
        }

        public EmailVerifyViewModel()
        {
            EmailVerify = true;
            SendCode = false;
        }

        public ICommand Verify
        {
            get
            {
                return new Xamarin.Forms.Command(VerifyEmail);
            }
        }

        public ICommand Send
        {
            get
            {
                return new Xamarin.Forms.Command(SenCode);
            }
        }

        private async void VerifyEmail(object obj)
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

        private async void SenCode(object obj)
        {
            if (string.IsNullOrEmpty(Code))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Ingresa tu codigo", "Ok");
            }
            else
            {
                var resp = await RestClient.Post<userModel>("users/authcode/"+ Code, null);
                if (resp.IsSuccess)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new RecoveryPass());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Ocurrió un problema", "Ok");
                }
            }
        }
    }
}
