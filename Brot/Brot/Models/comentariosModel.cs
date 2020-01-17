namespace Brot.Models
{
    using System;

    public class comentariosModel : Brot.ViewModels.ObservableObject
    {
        private string _Contenido;
        public string contenido
        {
            get { return _Contenido; }
            set { SetProperty(ref _Contenido, value); }
        }
        public int id_comentario { get; set; }
        public int id_user { get; set; }
        public int id_post { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<bool> isDeleted { get; set; }

    }
}
