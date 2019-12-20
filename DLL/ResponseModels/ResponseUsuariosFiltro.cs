using DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.ResponseModels
{
    public class ResponseUsuariosFiltro
    {
        public userModel userData { get; set; }
        public int Cantidad { get; set; }
    }
}
