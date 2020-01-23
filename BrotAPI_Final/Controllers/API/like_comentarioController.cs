using BrotAPI_Final.Models;
using BrotAPI_Final.Repository;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BrotAPI_Final.Controllers.API
{
    public class like_comentarioController : ApiController
    {
        private readonly DBContextModel db = new DBContextModel();
        private Rlike_comentarioDB r = new Rlike_comentarioDB();

        public HttpResponseMessage GetLikesbyIDComentario(int id)
        {
            if (!ValidandoSiExistenDatosRelacionados.ExistsComment(id))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe tal comentario, id: {id}");
            }
            using (var db = new DBContextModel())
            {
                var usuarios = db.like_comentario
                    .Include(p => p.users)
                    .Where(p => p.id_comentario == id && p.users.isDeleted == false).ToList();

                var usuariosLike = new DLL.ResponseModels.ResponseLikes()
                {
                    id_PostoComentario = id,
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
        [Route("api/like_comentario/borrar")]
        [HttpPost]
        public HttpResponseMessage Delete(like_comentario item)
        {
            try
            {
                var likes = db.like_comentario.Where(l => l.id_comentario == item.id_comentario && l.id_user == item.id_user).ToArray();
                foreach (var like in likes)
                {
                    r.Delete(like.id_like_comentario);
                }
                return Request.CreateResponse(HttpStatusCode.OK, $"like_post fue eliminado correctamente");
            }
            catch (Exception) { }

            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"No eliminado, like_comentario");
        }


        /// <summary>
        /// Recibe un objeto como paramentro y posteriormente intenta almacenarlo en la base de datos
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> Post(like_comentario item)
        {
            if (item == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"El like_comentario no puede estar sin datos");
            }
            //Verfico si ya existe para no agregar dos veces
            try
            {
                var likeDado = db.like_comentario.SingleOrDefault(l => l.id_comentario == item.id_comentario && l.id_user == item.id_user);

                if (likeDado == default(like_comentario))
                {
                    item.fecha = DateTime.Now;

                    if (r.Post(item))
                    {
                        //TODO TEST like comentario
                        var diccionarioPhone_Device = new System.Collections.Generic.Dictionary<string, string>();
                        var usuarioQueDioLike = db.users.SingleOrDefault(u => u.id_user == item.id_user);
                        var comentario = db.comentarios.SingleOrDefault(u => u.id_comentario == item.id_comentario);
                        diccionarioPhone_Device.Add(comentario.users.Phone_OS, comentario.users.Device_id);

                        var pushNotifier = new AppCenterPush(diccionarioPhone_Device);
                        await pushNotifier.Notify("like_comentario",
                            $"A {usuarioQueDioLike.username} le gusta tu publicación",
                            comentario.contenido,
                            new System.Collections.Generic.Dictionary<string, string>() {
                                {DLL.PushConstantes.gotoPage,DLL.PushConstantes.goto_post },
                                {DLL.PushConstantes.id_post, comentario.id_post.ToString()},
                                {DLL.PushConstantes.id_user, comentario.publicaciones.id_user.ToString() }
                            });

                        return Request.CreateResponse(HttpStatusCode.Created, "like_comentario guardado correctamente");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Ya existe dicho like");
                }
            }
            catch (Exception) { }


            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No es posible guardar los datos del like_comentario");
        }



        #endregion


    }
}
