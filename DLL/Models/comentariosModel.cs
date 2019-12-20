namespace DLL.Models
{
    using System;

    public class comentariosModel
    {

        public int id_comentario { get; set; }
        public int id_user { get; set; }
        public int id_post { get; set; }
        public string contenido { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<bool> isDeleted { get; set; }

    }
}
