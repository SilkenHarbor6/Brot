namespace Brot.Models
{
    using System;

    public class interaccion_postModel
    {
        public int id_interaccion_post { get; set; }
        public int id_post { get; set; }
        public int id_userqueinteractuo { get; set; }
        public int id_perfilvisitado { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }

    }
}
