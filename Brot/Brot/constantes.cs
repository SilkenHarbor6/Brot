using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public static class constantes
    {
        public static string urlImages { get { return "https://brotimages.azurewebsites.net/Uploads/"; } } //Cambiar tambien el el metodo de subir immagenes
        public static string likeImage { get { return "Love128.png"; } }
        public static string ProfileImageError { get { return "user_placeholder.png"; } }
        public static string dislikeImage { get { return "Love128Blank.png"; } }
        public static int LikeSize { get { return 40; } }   ///Cambiarlo tambien en App.XAML
    }
}
