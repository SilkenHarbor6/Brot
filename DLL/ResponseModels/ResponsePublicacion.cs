

namespace DLL.ResponseModels
{
    using DLL.Models;
    using System.Collections.Generic;
    public class ResponsePublicacion 
    {

        public ResponsePublicacionFeed publicacion { get; set; }
        public List<ResponseComentarios> comentarios { get; set; }
    }
}
