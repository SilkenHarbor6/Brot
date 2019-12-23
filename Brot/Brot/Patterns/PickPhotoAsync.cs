using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace Brot.Patterns
{
    public class PickPhotoAsync
    {
        private MediaFile _mediaFile;
        String pa;
        private ImageSource img;
        FileStream fs;
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
                await Application.Current.MainPage.DisplayAlert("Error", "No es posible elegir una foto", "Aceptar");
                return;
            }
            _mediaFile = await CrossMedia.Current.PickPhotoAsync();
            if (_mediaFile == null)
            {
                return;
            }
            FileInfo fi = new FileInfo(_mediaFile.Path);
            //name = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Singleton.Instance.User.username);
            name = Singleton.Instance.User.username;
            name += DateTime.Now;
            name = name.Replace(".", "");
            name = name.Replace(" ", "");
            name = name.Replace('\n', '_');
            name = name.Replace('\r', '_');
            name = name.Replace(":", "");
            name = name.Replace("_", "");
            name = name.Replace("/", "");
            name += fi.Extension;
            pa = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            pa += "/" + name;
            File.Copy(_mediaFile.Path, pa);
            fs = new FileStream(pa, FileMode.Open,FileAccess.Read);
            //_mediaFile = new MediaFile(pa, new Func<Stream>());
            var resp = await Application.Current.MainPage.DisplayAlert("Confirmacion", "Desea utilizar esta imagen", "Aceptar", "Cancelar");
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
                content.Add(new StreamContent(fs), "\"file\"", $"\"{pa}\"");
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
