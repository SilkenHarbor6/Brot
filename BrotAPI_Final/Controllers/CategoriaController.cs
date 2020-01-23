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
        private RCategoria cate = new RCategoria();

        [Route("api/categoria")]
        [HttpGet]
        public HttpResponseMessage GetCategorias()
        {
            var resp = cate.GetAll();
            if (resp.Count() == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No hay categorias registradas");
            }

            var categorias = resp.Select(
                c => new categoria
                {
                    id_categoria = c.id_categoria,
                    img = c.img,
                    nombre = c.nombre
                }
                ).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, categorias);
        }


        [Route("api/categoria")]
        [HttpPost]
        public HttpResponseMessage AddCat(categoria item)
        {
            var resp = cate.AgregarCategoria(item);
            if (resp == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Error al agregar el item a la base de datos");
            }
            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }

        [Route("api/categoria/{id_categoria}")]
        [HttpDelete]
        public HttpResponseMessage RemoveCat(int id_categoria)
        {
            var resp = cate.EliminarCategoria(id_categoria);
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
            if (resp == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No se ha encontrado categorias para ese usuario");
            }
            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }
    }
}
