namespace Brot.Services
{
    using DLL;
    using Plugin.LocalNotifications;
    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using Xamarin.Forms;

    public class DownloadService 
    {
        IDownloader downloader = DependencyService.Get<IDownloader>();
        public DownloadService()
        {
            downloader.OnFileDownloaded += OnFileDownloaded;
        }
        private void OnFileDownloaded(object sender, DownloadEventArgs e)
        {
            if (e.FileSaved)
            {
                App.Current.MainPage.DisplayAlert("XF Downloader", "File Saved Successfully", "Close");
                CrossLocalNotifications.Current.Show("Imagen guardada", "La imagen ha sido guardada correctamente");
                //var notification = new NotificationRequest
                //{
                //    NotificationId = 100,
                //    Title = "Exito",
                //    Description = "La imagen ha sido descargada",
                //    ReturningData = "Dummy data", // Returning data when tapped on notification.
                //    NotifyTime = DateTime.Now.AddSeconds(1) // Used for Scheduling local notification, if not specified notification will show immediately.

                //};
                //NotificationCenter.Current.Show(notification);
            }
            else
            {
                App.Current.MainPage.DisplayAlert("XF Downloader", "Error while saving the file", "Close");
                CrossLocalNotifications.Current.Show("Error", "No se ha podido guardar la imagen");
                //var notification = new NotificationRequest
                //{
                //    NotificationId = 100,
                //    Title = "Error",
                //    Description = "No se ha podido guardar la imagen",
                //    ReturningData = "Dummy data", // Returning data when tapped on notification.
                //    NotifyTime = DateTime.Now.AddSeconds(1) // Used for Scheduling local notification, if not specified notification will show immediately.
                //};
                //NotificationCenter.Current.Show(notification);
            }
        }

        public async void StartDownload(string url)
        {
            PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (status != PermissionStatus.Granted)
            {
                await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
            }
            downloader.DownloadFile(url, "Brot");
        }
    }
}
