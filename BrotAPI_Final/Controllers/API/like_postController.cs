using BrotAPI_Final.Models;
using BrotAPI_Final.Repository;
using System;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BrotAPI_Final.Controllers.API
{
    public class like_postController : ApiController
    {

        private DBContextModel db = new DBContextModel();
        private Rlike_postDB r = new Rlike_postDB();


        [Route("api/like_post/{idPost}")]
        public HttpResponseMessage GetLikesbyIDPost(int idPost)
        {

            //Validando si existe el post
            if (idPost == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se recibio ID de la publicación");
            }
            if (!ValidandoSiExistenDatosRelacionados.ExistsPublicacion(idPost))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No existe dicha publicación");
            }

            using (var db = new DBContextModel())
            {
                var usuarios = db.like_post
                    .Include(p => p.users)
                    .Where(p => p.id_post == idPost && p.users.isDeleted == false)
                    .ToList();

                var usuariosLike = new DLL.ResponseModels.ResponseLikes()
                {
                    id_PostoComentario = idPost,
                    usuarios = usuarios.Select(b =>
                           new DLL.Models.userModel()
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
                           }
                       ).ToList()
                };
                return Request.CreateResponse(HttpStatusCode.OK, usuariosLike);
            }
        }




        #region DELETE - POST - PUT 



        /// <summary>
        /// Optiene un id y ese es pasado al repositorio para ver si puede eliminar el objeto en la base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/like_post/borrar")]
        [HttpPost]
        public HttpResponseMessage Delete(like_post item)
        {

            try
            {
                var likes = db.like_post.Where(l => l.id_post == item.id_post && l.id_user == item.id_user).ToArray();

                foreach (var like in likes)
                {
                    r.Delete(like.id);
                }
                return Request.CreateResponse(HttpStatusCode.OK, $"like_post fue eliminado correctamente");
            }
            catch (Exception) { }

            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"No eliminado, like_post");

        }


        /// <summary>
        /// Recibe un objeto como paramentro y posteriormente intenta almacenarlo en la base de datos
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Post(like_post item)
        {
            if (item == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"El like_post no puede estar sin datos");
            }

            try
            {
                var likeDado = db.like_post.SingleOrDefault(l => l.id_post == item.id_post && l.id_user == item.id_user);

                if (likeDado == default(like_post))
                {
                    item.fecha = DateTime.UtcNow;

                    if (r.Post(item))
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, "like_post guardado correctamente");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Ya existe dicho like");
                }
            }
            catch (Exception e) { Debug.Print(e.Message); }

            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No es posible guardar los datos del like_post");
        }


        #endregion


    }
}
