//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BrotAPI_Final.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class visita_busqueda
    {
        public int id_visita_busqueda { get; set; }
        public int id_userquebusco { get; set; }
        public int id_perfilvisitado { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
    
        public virtual users users { get; set; }
        public virtual users users1 { get; set; }
    }
}
