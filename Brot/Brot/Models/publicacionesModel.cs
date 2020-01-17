namespace Brot.Models
{
    using System;

    public class publicacionesModel : ViewModels.BaseViewModel
    {
        private string _descripcion;
        public string descripcion
        {
            get { return _descripcion; }
            set { SetProperty(ref _descripcion, value); }
        }
        public int id_post { get; set; }
        public int id_user { get; set; }
        public string img { get; set; }
        public Nullable<bool> isImg { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<System.DateTime> fecha_actualizacion { get; set; }
        public Nullable<bool> isDeleted { get; set; }

    }
}
