using BrotAPI_Final.Models;
using BrotAPI_Final.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BrotAPI_Final.Controllers.API
{
    public class visita_busquedaController : ApiController
    {
        private SomeeDBBrotEntities db = new SomeeDBBrotEntities();
        private Rvisita_busquedaDB r = new Rvisita_busquedaDB();


        #region DELETE - POST - PUT 



        /// <summary>
        /// Optiene un id y ese es pasado al repositorio para ver si puede eliminar el objeto en la base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id)
        {
            var item = r.GetById(id);
            if (item == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe tal visita_busqueda, id: {id}");
            }
            if (r.Delete(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"visita_busqueda {id} fue eliminado correctamente");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"No eliminado, visita_busqueda {id}");
        }


        /// <summary>
        /// Recibe un objeto como paramentro y posteriormente intenta almacenarlo en la base de datos
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(visita_busqueda item)
        {
            if (item == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"La visita_busqueda no puede estar sin datos");
            }
            item.fecha = DateTime.Now;
            if (r.Post(item))
            {
                return Request.CreateResponse(HttpStatusCode.Created, "visita_busqueda guardado correctamente");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No es posible guardar los datos de la publicación");
        }



        /// <summary>
        /// Verifica si existe el id ingresado en la tabla y luego actualiza el registro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(int id, visita_busqueda item)
        {
            var data = r.GetById(id);
            if (data == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe en la base de datos la visita_busqueda a actualizar");
            }
            if (r.Put(id, item))
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Datos modificados para la visita_busqueda {id}");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"No fue posible actualizar la visita_busqueda, id: {id}");
        }
        #endregion



    }
}
