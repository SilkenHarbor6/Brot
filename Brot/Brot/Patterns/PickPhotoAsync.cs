using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace Brot.Patterns
{
    public class PickPhotoAsync
    {
        private MediaFile _mediaFile;
        public static String name;
        public static ImageSource path;
        public async void ChangePicture()
        {
            await CrossMedia.Current.Initialize();
            PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if ( status!= PermissionStatus.Granted)
            {
                await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                
            }
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No es posible elegir una foto", "Aceptar");
                return;
            }
            _mediaFile = await CrossMedia.Current.PickPhotoAsync();
            if (_mediaFile == null)
            {
                return;
            }
            name = _mediaFile.Path.Split('/').LastOrDefault();
            var resp = await App.Current.MainPage.DisplayAlert("Confirmacion", "Desea utilizar esta imagen", "Aceptar", "Cancelar");
            if (resp)
            {
                if (Singleton.fromProfile)
                {
                    Singleton.Instance.User.img = name;
                }
                path = ImageSource.FromStream(() =>
                {
                    return _mediaFile.GetStream();
                });
                UploadImage();
            }
        }
        private async void UploadImage()
        {
            //metodo para publicar la imagen en el servidor web
            try
            {
                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(_mediaFile.GetStream()), "\"file\"", $"\"{_mediaFile.Path}\"");
                var httpClient = new HttpClient();
                var uploadServiceBaseAddress = "http://brotimageapi.azurewebsites.net/api/Upload";  //API/CONTROLLER
                var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);
                Debug.Print(await httpResponseMessage.Content.ReadAsStringAsync());
                //await DisplayAlert("Exito", await httpResponseMessage.Content.ReadAsStringAsync(), "Aceptar");
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "Aceptar");
            }
        }
    }
}
