using Brot.Models;
using Brot.Patterns;
using Brot.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Brot.ViewModels
{
    class ChngPViewModel:BaseViewModel
    {
        private String op;
        private String np;
        private String rp;
        public String oldPass
        {
            get
            {
                return op;
            }
            set
            {
                op = value;OnPropertyChanged("oldPass");
            }
        }
        public String newPass
        {
            get
            {
                return np;
            }
            set
            {
                np = value;OnPropertyChanged("newPass"); Singleton.passw = value;
            }
        }
        public String repeatedPass
        {
            get
            {
                return rp;
            }
            set
            {
                rp = value;OnPropertyChanged("repeatedPass");
            }
        }
        public ICommand passwordCommand
        {
            get
            {
                return new Xamarin.Forms.Command(chnPass);
            }
        }

        private async void chnPass()
        {
            if (np!=rp)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Las claves no coinciden", "Aceptar");
            }
            userModel u = new userModel();
            u.id_user= Singleton.Instance.User.id_user;
            u.pass = np;
            var resp = await RestClient.Put<userModel>("users/pass", op, u);
            if (!resp)
            {
                await App.Current.MainPage.DisplayAlert("Error", "La clave antigua es incorrecta, por favor revisela", "Aceptar");
                return;
            }
            await App.Current.MainPage.DisplayAlert("", "La clave ha sido cambiada exitosamente", "Aceptar");
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
