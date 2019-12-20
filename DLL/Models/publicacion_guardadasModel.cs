namespace DLL.Models
{
    using System;

    public class publicacion_guardadasModel
    {
        public int id_publicacion_guardada { get; set; }
        public Nullable<int> id_user { get; set; }
        public Nullable<int> id_post { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }

    }
}
