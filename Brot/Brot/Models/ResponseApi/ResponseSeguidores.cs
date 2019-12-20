namespace Brot.Models.ResponseApi
{
    public class ResponseSeguidores
    {
        public int id_user { get; set; }
        public string username { get; set; }
        public string descripcion { get; set; }
        public bool? isVendor { get; set; }
        public int? puntaje { get; set; }
    }
}
