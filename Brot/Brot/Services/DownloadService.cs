using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Brot.Services
{
    public class DownloadService 
    {
        public async void DownloadImage()
        {
            var webClient = new WebClient();
            webClient.DownloadDataCompleted += (s, e) => {
                var bytes = e.Result; // get the downloaded data
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string localFilename = "downloaded.png";
                string localPath = Path.Combine(documentsPath, localFilename);
                File.WriteAllBytes(localPath, bytes); // writes to local storage
            };
            var url = new Uri("https://www.xamarin.com/content/images/pages/branding/assets/xamagon.png");
            webClient.DownloadDataAsync(url);
        }
    }
}
