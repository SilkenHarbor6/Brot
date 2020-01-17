using BrotAPI_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrotAPI_Final.Repository
{
    interface ICategoria
    {
        IEnumerable<categoria> GetAll();
        categoria AgregarCategoria(categoria item);
        categoria ActualizarCategoria(categoria item);
        bool EliminarCategoria(categoria item);
        IEnumerable<categoria> GetByUser(int id);
        categoria GetMainCategory(int id);
    }
}
