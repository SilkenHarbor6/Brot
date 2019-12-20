
namespace DLL.ResponseModels
{
    using DLL.Models;
    public class ResponseComentarios
    {
        public bool isLiked { get; set; }
        public comentariosModel comentario { get; set; }
        public userModel usuario { get; set; }
        public int CantLikes { get; set; }
    }
}
