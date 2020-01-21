using Brot.Models;
using Brot.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Brot.ViewModels
{
   public class RecoveryPassViewModel : BaseViewModel
    {
        private String np;
        private String rp;
        public String newPass
        {
            get { return np; }
            set { SetProperty(ref np, value); }
        }
        public String repeatedPass
        {
            get { return rp; }
            set { SetProperty(ref rp, value); }
        }
        public ICommand passwordCommand
        {
            get
            {
                return new Xamarin.Forms.Command(recoveryPass);
            }
        }

        private async void recoveryPass()
        {
            IsRefreshing = true;

            if (np != rp)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Las claves no coinciden", "Aceptar");
                IsRefreshing = false;
                return;
            }
            userModel u = new userModel();
            u.id_user = Singleton.Instance.User.id_user;
            u.pass = np;
            var resp = await RestClient.Put<userModel>("users/pass", op, u);
            if (!resp)
            {
                await App.Current.MainPage.DisplayAlert("Error", "La clave antigua es incorrecta, por favor revisela", "Aceptar");
                IsRefreshing = false;
                return;
            }
            await App.Current.MainPage.DisplayAlert("", "La clave ha sido cambiada exitosamente", "Aceptar");
            IsRefreshing = false;
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
