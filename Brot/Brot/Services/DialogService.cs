
namespace Brot.Services
{
    using Acr.UserDialogs;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using System.Threading.Tasks;

    public class DialogService
    {
        public async Task Message(string title, string message)
        {
            await App.Current.MainPage.DisplayAlert(title, message, "Accept");
        }

        public async Task<bool> Message(string title, string message, string ok, string cancel)
        {
            return await App.Current.MainPage.DisplayAlert(title, message, ok, cancel);
        }

        public void ToastOk(string mensaje, int miliSegundos)
        {
            ToastConfig toastito = new ToastConfig(mensaje)
                .SetPosition(ToastPosition.Top)
                .SetBackgroundColor(Color.Green)
                .SetMessageTextColor(Color.Black)
                .SetDuration(miliSegundos);

            UserDialogs.Instance.Toast(toastito);
        }

        public void ToastBAD(string mensaje, int miliSegundos)
        {
            ToastConfig toastito = new ToastConfig(mensaje)
                .SetPosition(ToastPosition.Top)
                .SetBackgroundColor(Color.LightCoral)
                .SetMessageTextColor(Color.Black)
                .SetDuration(miliSegundos);

            UserDialogs.Instance.Toast(toastito);
        }


    }
}
