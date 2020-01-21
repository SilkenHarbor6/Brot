using BrotAPI_Final.Models;
using BrotAPI_Final.Repository;
using DLL.ResponseModels;
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
    public class publicacion_guardadaController : ApiController
    {
        private DBContextModel db = new DBContextModel();



        private Rpublicacion_guardadaDB r = new Rpublicacion_guardadaDB();


        [HttpGet]
        [Route("api/publicacion_guardada/{idUser}")]
        public HttpResponseMessage GetPostsSaved(int idUser)
        {
            using (var db = new DBContextModel())
            {
                var publicacionesGuardadas = db.publicacion_guardada
                    .Include(p => p.users)
                    .Include(p => p.publicaciones)
                    .Include(p => p.publicaciones.like_post)
                    .Where(p => p.id_user == idUser)
                    .OrderByDescending(f=>f.fecha)
                    .Select(
                        p =>
                            new ResponsePublicacionFeed
                            {

                                publicacion = new DLL.Models.publicacionesModel
                                {
                                    descripcion = p.publicaciones.descripcion,
                                    fecha_actualizacion = p.publicaciones.fecha_actualizacion,
                                    fecha_creacion = p.publicaciones.fecha_creacion,
                                    id_post = p.publicaciones.id_post,
                                    id_user = p.publicaciones.id_user,
                                    img = p.publicaciones.img,
                                    isImg = p.publicaciones.isImg,
                                    isDeleted = p.publicaciones.isDeleted
                                },
                                UsuarioCreator = new DLL.Models.userModel
                                {
                                    apellido = p.users.apellido,
                                    descripcion = p.users.descripcion,
                                    email = p.users.email,
                                    id_user = p.users.id_user,
                                    isVendor = p.users.isVendor,
                                    nombre = p.users.nombre,
                                    pass = "pass",
                                    puntaje = p.users.puntaje,
                                    username = p.users.username,
                                    img = p.users.img,
                                    puesto_name = p.users.puesto_name,
                                    isActive = p.users.isActive,
                                    dui = p.users.dui,
                                    isDeleted = p.users.isDeleted,
                                    num_telefono = p.users.num_telefono,
                                    xlat = p.users.xlat,
                                    ylon = p.users.ylon
                                },

                                cantComentarios = p.publicaciones.comentarios.Count,
                                cantLikes = p.publicaciones.like_post.Count,
                                IsLiked = p.publicaciones.like_post.FirstOrDefault(l => l.id_user == idUser) == default(like_post) ? false : true,
                                IsSavedPost = true
                            }

                    ).OrderByDescending(f => f.publicacion.fecha_creacion).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, publicacionesGuardadas);
            }
        }



        #region  POST - PUT - DELETE




        /// <summary>
        /// Recibe un objeto como paramentro y posteriormente intenta almacenarlo en la base de datos
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Post(publicacion_guardada item)
        {
            if (item == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"La publicacion_guardada no puede estar sin datos");
            }
            try
            {
                var publicacionGuardada = db.publicacion_guardada.SingleOrDefault(l => l.id_post == item.id_post && l.id_user == item.id_user);

                if (publicacionGuardada == default(publicacion_guardada))
                {
                    item.fecha = DateTime.UtcNow;
                    if (r.Post(item))
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, "publicacion guardada guardado");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Ya has guardado esta publicación");
                }
            }
            catch (Exception e) { Debug.Print(e.Message); }

            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No es posible guardar el post en este momento");
        }



        /// <summary>
        /// Verifica si existe el id ingresado en la tabla y luego actualiza el registro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/publicacion_guardada/{CualquierNumeroxd}")]
        public HttpResponseMessage Put(int CualquierNumeroxd, publicacion_guardada item)
        {

            try
            {
                var publicacionesGuardada = db.publicacion_guardada.Where(l => l.id_post == item.id_post && l.id_user == item.id_user).ToList();
                foreach (var publicacion in publicacionesGuardada)
                {
                    r.Delete(publicacion.id_publicacion_guardada);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            
            return Request.CreateErrorResponse(HttpStatusCode.OK, $"Publicación quitada de guardados");
        }
        #endregion



    }
}
