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
    
    public partial class comentarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public comentarios()
        {
            this.like_comentario = new HashSet<like_comentario>();
        }
    
        public int id_comentario { get; set; }
        public int id_user { get; set; }
        public int id_post { get; set; }
        public string contenido { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<bool> isDeleted { get; set; }
    
        public virtual publicaciones publicaciones { get; set; }
        public virtual users users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<like_comentario> like_comentario { get; set; }
    }
}
