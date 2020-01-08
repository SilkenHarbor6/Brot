using BrotAPI_Final.Models;
using BrotAPI_Final.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BrotAPI_Final.Controllers
{
    public class CategoriaController : ApiController
    {
        //private SomeeDBBrotEntities db = new SomeeDBBrotEntities();
        private ICategoria cate = new RCategoria();

        [Route("api/categoria/Get")]
        public HttpResponseMessage GetCategorias()
        {
            var resp = cate.GetAll();
            if (resp.Count()==0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No hay categorias registradas");
            }
            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }
        [Route("api/categoria/Add")]
        public HttpResponseMessage AddCat(categoria item)
        {
            var resp = cate.AgregarCategoria(item);
            if (resp==null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Error al agregar el item a la base de datos");
            }
            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }
        [Route("api/categoria/Remove")]
        public HttpResponseMessage RemoveCat(categoria item)
        {
            var resp = cate.EliminarCategoria(item);
            if (resp)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "No se ha podido eliminar el registro");
            }
            return Request.CreateResponse(HttpStatusCode.OK, "El registro ha sido eliminado");
        }
        [Route("api/categoria/GBU/{id}")]
        public HttpResponseMessage GetByCustomer(int id)
        {
            var resp = cate.GetByUser(id);
            if (resp==null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No se ha encontrado categorias para ese usuario");
            }
            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }
    }
}
