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
    
    public partial class usuario_categoria
    {
        public int id_usuario_categoria { get; set; }
        public int id_usuario { get; set; }
        public int id_categoria { get; set; }
        public int isPrimary { get; set; }
    
        public virtual categoria categoria { get; set; }
        public virtual users users { get; set; }
    }
}