namespace Brot.Models.ResponseApi
{
    using Models;
    using System.Collections.Generic;
    public class ResponsePublicacion 
    {

        public ResponsePublicacionFeed publicacion { get; set; }
        public List<ResponseComentarios> comentarios { get; set; }
    }
}
