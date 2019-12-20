namespace Brot.Models
{
    using System;

    public class publicacionesModel
    {

        public int id_post { get; set; }
        public int id_user { get; set; }
        public string img { get; set; }
        public Nullable<bool> isImg { get; set; }
        public string descripcion { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<System.DateTime> fecha_actualizacion { get; set; }
        public Nullable<bool> isDeleted { get; set; }

    }
}
