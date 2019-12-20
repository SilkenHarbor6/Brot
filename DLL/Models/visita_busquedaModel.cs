namespace DLL.Models
{
    using System;

    public class visita_busquedaModel
    {
        public int id_visita_busqueda { get; set; }
        public int id_userquebusco { get; set; }
        public int id_perfilvisitado { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }

    }
}
