

namespace DLL.ResponseModels
{
    using DLL.Models;
    using System.Collections.Generic;
    public class ResponseLikes
    {
        public int id_PostoComentario { get; set; }
        public List<userModel> usuarios { get; set; }
    }
}
