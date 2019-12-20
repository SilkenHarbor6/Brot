namespace Brot.Models.ResponseApi
{
    using Models;
    using System;
    public class ResponsePublicacionGuardada
    {

        public publicacionesModel publicacion { get; set; }
        public userModel UsuarioCreator { get; set; }
        public publicacion_guardadasModel publicacionGuardada { get; set; }
        public int cantComentarios { get; set; }
        public int cantLikes { get; set; }
        public Nullable<bool> IsLiked { get; set; }
        public Nullable<bool> IsSavedPost { get; set; }
    }
}
