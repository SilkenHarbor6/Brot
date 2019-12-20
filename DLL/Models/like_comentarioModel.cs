namespace DLL.Models
{
    using System;

    public class like_comentarioModel
    {
        public int id_like_comentario { get; set; }
        public int id_user { get; set; }
        public int id_comentario { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }


    }
}
