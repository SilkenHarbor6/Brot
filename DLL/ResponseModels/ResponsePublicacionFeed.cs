

namespace DLL.ResponseModels
{
    using DLL.Models;
    using System;
    using System.Collections.Generic;
    public class ResponsePublicacionFeed 
    {

        public publicacionesModel publicacion { get; set; }
        public userModel UsuarioCreator { get; set; }
        public int cantComentarios { get; set; }
        public int cantLikes { get; set; }
        public Nullable<bool> IsLiked { get; set; }
        public Nullable<bool> IsSavedPost { get; set; }



    }
}
