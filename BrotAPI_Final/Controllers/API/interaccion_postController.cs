using BrotAPI_Final.Models;
using BrotAPI_Final.Repository;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BrotAPI_Final.Controllers.API
{
    public class interaccion_postController : ApiController
    {
        private SomeeDBBrotEntities db = new SomeeDBBrotEntities();


        #region DELETE - POST - PUT 


        private Rinteraccion_postDB r = new Rinteraccion_postDB();

        /// <summary>
        /// Optiene un id y ese es pasado al repositorio para ver si puede eliminar el objeto en la base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var item = r.GetById(id);
            if (item == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe tal interaccion_post, id: {id}");
            }
            if (r.Delete(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"interaccion_post {id} fue eliminado correctamente");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"No eliminado, interaccion_post {id}");
        }


        /// <summary>
        /// Recibe un objeto como paramentro y posteriormente intenta almacenarlo en la base de datos en la tabla
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Post(interaccion_post item)
        {
            if (item == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"La interaccion_post no puede estar sin datos");
            }
            item.fecha = DateTime.UtcNow;
            if (r.Post(item))
            {
                return Request.CreateResponse(HttpStatusCode.Created, "interaccion_post guardado correctamente");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No es posible guardar los datos de la interaccion_post");
        }



        /// <summary>
        /// Verifica si existe el id ingresado en la tabla y luego actualiza el registro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut]
        public HttpResponseMessage Put(int id, interaccion_post item)
        {
            var data = r.GetById(id);
            if (data == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe en la base de datos la interaccion_post a actualizar");
            }
            item.fecha = DateTime.UtcNow;
            if (r.Put(id, item))
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Datos modificados para la interaccion_post {id}");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"No fue posible actualizar la interaccion_post, id: {id}");
        }
        #endregion


    }
}
