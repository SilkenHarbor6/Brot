using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.ResponseModels
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
