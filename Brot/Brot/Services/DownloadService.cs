namespace Brot.Services
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    public class DownloadService 
    {
        public void DownloadImage(String url)
        {
            var webClient = new WebClient();
            var url2 = new Uri(url);
            webClient.DownloadDataCompleted += (s, e) => {
                var bytes = e.Result; // get the downloaded data
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string localFilename = url.Split('/').LastOrDefault();
                string localPath = Path.Combine(documentsPath, localFilename);
                File.WriteAllBytes(localPath, bytes); // writes to local storage
            };
            webClient.DownloadDataAsync(url2);
            Debug.Print("Descargado");
        }
    }
}
