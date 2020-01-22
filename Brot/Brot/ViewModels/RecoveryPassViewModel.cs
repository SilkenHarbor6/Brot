using Brot.Models;
using Brot.Services;
using Brot.Views;
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
        private string Id;
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
        public RecoveryPassViewModel(string ids)
        {
            Id = ids;
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
            userModel item = new userModel();
            item.pass = np;
            var resp = await RestClient.Put<userModel>("users/recpass", Id, item);
            if (resp)
            {
                await App.Current.MainPage.DisplayAlert("", "La clave ha sido cambiada exitosamente", "Aceptar");
                IsRefreshing = false;
                await App.Current.MainPage.Navigation.PushAsync(new Login());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Hubo un error al actualizar", "Aceptar");
                IsRefreshing = false;
            }
        }
    }
}
