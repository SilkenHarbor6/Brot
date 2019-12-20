namespace Brot.Models
{
    using System;
    public class userModel
    {

        public int id_user { get; set; }
        public string username { get; set; }
        public string puesto_name { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string descripcion { get; set; }
        public string pass { get; set; }
        public bool isVendor { get; set; }
        public Nullable<int> puntaje { get; set; }
        public string email { get; set; }
        public Nullable<float> xlat { get; set; }
        public Nullable<float> ylon { get; set; }
        public Nullable<bool> isActive { get; set; }
        public string dui { get; set; }
        public string num_telefono { get; set; }
        public string img { get; set; }
        public Nullable<bool> isDeleted { get; set; }

    }
}
