using BrotAPI_Final.Models;
using BrotAPI_Final.Repository;
using DLL.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace BrotAPI_Final.Controllers.API
{
    public class comentariosController : ApiController
    {
        //private DB_BrotEntitiesLast db = new DB_BrotEntitiesLast();
        private RcomentariosDB r = new RcomentariosDB();



        #region Gets
        [Route("api/comentarios/{idComentario}")]
        [HttpGet]
        public HttpResponseMessage Getbyid(int idComentario)
        {
            using (var db = new DBContextModel())
            {
                var comment = db.comentarios
                    .Where(p => p.id_comentario == idComentario)
                    .Include(p => p.users)
                    .Select(b =>
                       new ResponseComentarios()
                       {

                           comentario = new DLL.Models.comentariosModel()
                           {
                               contenido = b.contenido,
                               fecha_creacion = b.fecha_creacion,
                               id_comentario = b.id_comentario,
                               id_post = b.id_post,
                               id_user = b.id_user,
                               isDeleted = b.isDeleted
                           },
                           usuario = new DLL.Models.userModel()
                           {
                               apellido = b.users.apellido,
                               descripcion = b.users.descripcion,
                               email = b.users.email,
                               id_user = b.users.id_user,
                               isVendor = b.users.isVendor,
                               nombre = b.users.nombre,
                               pass = "pass",
                               puntaje = b.users.puntaje,
                               username = b.users.username,
                               img = b.users.img,
                               puesto_name = b.users.puesto_name,
                               isActive = b.users.isActive,
                               dui = b.users.dui,
                               isDeleted = b.users.isDeleted,
                               num_telefono = b.users.num_telefono,
                               xlat = b.users.xlat,
                               ylon = b.users.ylon
                           },
                           CantLikes = b.like_comentario.Count

                       }).FirstOrDefault(p => p.comentario.id_comentario == idComentario);
                return Request.CreateResponse(HttpStatusCode.OK, comment);
            }
        }
        #endregion

        #region DELETE - POST - PUT 



        /// <summary>
        /// Optiene un id y ese es pasado al repositorio para ver si puede eliminar el objeto en la base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [HttpDelete]
        [Route("api/comentarios/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var item = r.GetById(id);
            if (item == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe tal comentario, id: {id}");
            }
            item.isDeleted = true;
            item.fecha_creacion = DateTime.Now;
            if (r.Put(id, item))
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"comentario {id} fue eliminado correctamente");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"No eliminado, comentario {id}");
        }


        /// <summary>
        /// Recibe un objeto como paramentro y posteriormente intenta almacenarlo en la base de datos
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public async  Task<HttpResponseMessage> Post(comentarios item)
        {
            if (item == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"El comentario no puede estar sin datos");
            }

            //Valido si existe la publicacion, si no se manda el valor, por defecto es CERO
            if (!ValidandoSiExistenDatosRelacionados.ExistsPublicacion(item.id_post))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No existe dicha publicación");
            }

            //valido si existe el user que comenta
            if (!ValidandoSiExistenDatosRelacionados.ExistsUser(item.id_user))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No existe el usuario");
            }

            item.fecha_creacion = DateTime.Now;
            item.isDeleted = false;
            if (r.Post(item))
            {
                //TODO PUSH for comment
                using (var db = new DBContextModel())
                {
                    users creatorPost = db.publicaciones.SingleOrDefault(u => u.id_post == item.id_post).users;
                    var receiptInstallID = new Dictionary<string, string>
                            {
                                { creatorPost.Phone_OS,creatorPost.Device_id }
                            };

                    AppCenterPush appCenterPush = new AppCenterPush(receiptInstallID);
                    users CommenterPost = db.users.SingleOrDefault(u => u.id_user == item.id_user);
                    await appCenterPush.Notify("comentarios",
                        $"{CommenterPost.nombre} comentó tu publicación", 
                        item.contenido, 
                        new Dictionary<string, string>() {
                            { DLL.PushConstantes.gotoPage, DLL.PushConstantes.goto_post },
                            { DLL.PushConstantes.id_post, item.id_post.ToString()},
                            { DLL.PushConstantes.id_user, creatorPost.id_user.ToString()}
                        });

                }

                return Request.CreateResponse(HttpStatusCode.Created, item);
            }



            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "No es posible guardar los datos del comentario");
        }



        /// <summary>
        /// Verifica si existe el id ingresado en la tabla y luego actualiza el registro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/comentarios/{id}")]
        public HttpResponseMessage Put(int id, comentarios item)
        {
            var data = r.GetById(id);
            if (data == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe en la base de datos el comentario a actualizar");
            }
            if (r.Put(id, item))
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Datos modificados para el comentario {id}");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"No fue posible actualizar el comentario, id: {id}");
        }
        #endregion


    }
}
