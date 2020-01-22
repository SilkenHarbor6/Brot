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
        private string Id;
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
            IsRefreshing = true;
            if (string.IsNullOrEmpty(Email))
            {
               await App.Current.MainPage.DisplayAlert("Error", "Ingresa tu correo", "Ok");
                IsRefreshing = false;
            }
            else
            {
                var resp = await RestClient.Post<userModel>("users/verify", new userModel() { email = Email });
                if (resp.IsSuccess)
                {
                    EmailVerify = false;
                    SendCode = true;
                    IsRefreshing = false;
                    var result = (userModel)resp.Result;
                    Id = Convert.ToString(result.id_user);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No se pudo encontrar el email", "Ok");
                    IsRefreshing = false;
                }
            }  
        }

        private async void SenCode(object obj)
        {
            IsRefreshing = true;
            if (string.IsNullOrEmpty(Code))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Ingresa tu codigo", "Ok");
                IsRefreshing = false;
            }
            else
            {
                var item = new userModel();
                item.id_user = int.Parse(Id);
                var resp = await RestClient.Post<userModel>("users/authcode/"+ Code, item);
                if (resp.IsSuccess)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new RecoveryPass(Id));
                    IsRefreshing = false;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No se pudo comprobar el codigo", "Ok");
                    IsRefreshing = false;
                }
            }
        }
    }
}
