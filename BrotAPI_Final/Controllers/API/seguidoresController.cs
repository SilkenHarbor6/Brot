using BrotAPI_Final.Models;
using BrotAPI_Final.Repository;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BrotAPI_Final.Controllers.API
{
    public class seguidoresController : ApiController
    {

        private RseguidoresDB r = new RseguidoresDB();

        #region DELETE - POST - PUT 

        /// <summary>
        /// Optiene un id y ese es pasado al repositorio para ver si puede eliminar el objeto en la base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/seguidores/borrar")]
        [HttpPost]
        public HttpResponseMessage Delete(seguidores item)
        {

            try
            {
                using (var db = new Models.SomeeDBBrotEntities())
                {

                    var Seguidors = db.seguidores.Where(l => l.seguidor_id == item.seguidor_id && l.id_seguido == item.id_seguido).ToArray();

                    for (int i = 0; i < Seguidors.Length; i++)
                    {
                        r.Delete(Seguidors[i].id_seguidores);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, $"Has dejado de seguir al usuario");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"No es posible dejar de seguir al usuario en estos momentos");
        }


        /// <summary>
        /// Recibe un objeto como paramentro y posteriormente intenta almacenarlo en la base de datos
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(seguidores item)
        {
            if (item == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"El seguidor no puede estar sin datos");
            }
            if (!ValidandoSiExistenDatosRelacionados.ExistsUser(item.id_seguido))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"No existe el usuario seguido {item.id_seguido}");
            }
            if (!ValidandoSiExistenDatosRelacionados.ExistsUser(item.seguidor_id))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"No existe el seguidor {item.seguidor_id}");
            }
            if (item.id_seguido == item.seguidor_id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"No puedes seguirte a ti mismo");
            }
            using (var db = new SomeeDBBrotEntities())
            {
                var seguidorDatos = db.seguidores
                    .FirstOrDefault(s => s.id_seguido == item.id_seguido && s.seguidor_id == item.seguidor_id);
                if (seguidorDatos == default(seguidores))
                {
                    item.accepted = true;
                    item.fecha = DateTime.Now;
                    if (r.Post(item))
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, "seguidor guardado correctamente");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Ya sigues a este usuario, actualiza la página en la que te encuentras");
                }

            }

            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No es posible guardar los datos del seguidor");
        }

        #endregion

    }
}
