namespace DLL.Models
{
    using System;

    public class visita_perfil_postModel
    {
        public int id_visita_pefil_post { get; set; }
        public int id_post { get; set; }
        public int id_userquevisito { get; set; }
        public int id_perfilvisitado { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }

    }
}
