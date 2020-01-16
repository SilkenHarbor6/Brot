using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public static class constantes
    {
        public static string urlImages { get { return "http://brotimageapi.azurewebsites.net/Uploads/"; } } //Cambiar tambien el el metodo de subir immagenes
        public static string likeImage { get { return "like.png"; } }
        public static string ProfileImageError { get { return "user_placeholder.png"; } }
        public static string dislikeImage { get { return "dislike.png"; } }
        public static string SavedPost { get { return "SavedPost.png"; } }
        public static string SavedPostNOT { get { return "SavedPostNOT.png"; } }


        #region API CONTROLLERS
        public static string userst { get { return "users"; } }
        public static string publicacionest { get { return "publicaciones"; } }
        public static string comentariost { get { return "comentarios"; } }
        public static string like_postt { get { return "like_post"; } }
        public static string publicacion_guardadat { get { return "publicacion_guardada"; } }
        public static string seguidorest { get { return "seguidores"; } }
        public static string visita_busquedat { get { return "visita_busqueda"; } }
        public static string interaccion_postt { get { return "interaccion_post"; } }
        public static string visita_pefil_postt { get { return "visita_pefil_post"; } }
        public static string like_comentariot { get { return "like_comentario"; } }
        public static string urlAPIbase { get { return "cibomarket.somee.com/api/"; } }
        public static string appJSonString { get { return "application/json"; } }
        #endregion


    /**

    // Reloj para calcular tiempo!
    Stopwatch reloj = new Stopwatch();
    reloj.Start();
    Console.WriteLine("Segundo en hacer Like "+reloj.Elapsed.TotalSeconds);

    **/
}
}
