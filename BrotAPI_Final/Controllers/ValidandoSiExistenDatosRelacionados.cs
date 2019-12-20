using BrotAPI_Final.Repository;

namespace BrotAPI_Final.Controllers
{
    public static class ValidandoSiExistenDatosRelacionados
    {
        public static bool ExistsUser(int id)
        {
            var r = new RusersDB();

            if (r.GetById(id) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool ExistsComment(int id)
        {
            var r = new RcomentariosDB();

            if (r.GetById(id) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool ExistsPublicacion(int id)
        {
            var r = new RpublicacionesDB();

            if (r.GetById(id) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



    }
}