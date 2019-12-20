namespace Brot.Models
{
    using System;

    public class seguidoresModel
    {
        public int id_seguidores { get; set; }
        public int seguidor_id { get; set; }
        public int id_seguido { get; set; }
        public Nullable<bool> accepted { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }


    }
}
