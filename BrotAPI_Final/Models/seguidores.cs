//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BrotAPI_Final.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class seguidores
    {
        public int id_seguidores { get; set; }
        public int seguidor_id { get; set; }
        public int id_seguido { get; set; }
        public Nullable<bool> accepted { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
    
        public virtual users users { get; set; }
        public virtual users users1 { get; set; }
    }
}
