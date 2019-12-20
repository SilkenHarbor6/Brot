using BrotAPI_Final.Models;
using BrotAPI_Final.Repository;
using DLL.Models;
using DLL.ResponseModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace BrotAPI_Final.Controllers.API
{
    [RoutePrefix("api/publicaciones")]
    public class publicacionesController : ApiController
    {
        private SomeeDBBrotEntities db = new SomeeDBBrotEntities();
        private RpublicacionesDB r = new RpublicacionesDB();


        [Route("all/{idUser}")]
        public HttpResponseMessage GetPublicacionesALL(int idUser)//id del usuario
        {
            if (!ValidandoSiExistenDatosRelacionados.ExistsUser(idUser))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe tal usuario, id: {idUser}");
            }

            var varPublicaciones = db.publicaciones
                .Include(s => s.users)
                .Include(s => s.like_post)
                .Include(s => s.comentarios)
                .Where(s => s.isDeleted == false && s.users.isDeleted == false)
                .Select(b =>
                    new ResponsePublicacionFeed
                    {

                        publicacion = new publicacionesModel
                        {
                            descripcion = b.descripcion,
                            fecha_actualizacion = b.fecha_actualizacion,
                            fecha_creacion = b.fecha_creacion,
                            id_post = b.id_post,
                            id_user = b.id_user,
                            img = b.img,
                            isImg = b.isImg,
                            isDeleted = b.isDeleted

                        },

                        UsuarioCreator = new DLL.Models.userModel()
                        {
                            apellido = b.users.apellido,
                            descripcion = b.users.descripcion,
                            email = b.users.email,
                            id_user = b.users.id_user,
                            isVendor = b.users.isVendor,
                            nombre = b.users.nombre,
                            pass = b.users.pass,
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
                        cantComentarios = b.comentarios.Where(c => c.users.isDeleted == false && c.isDeleted == false).ToList().Count,
                        cantLikes = b.like_post.Where(l => l.users.isDeleted == false).ToList().Count,
                        IsLiked = b.like_post.FirstOrDefault(l => l.users.id_user == idUser) == default(like_post) ? false : true,
                        IsSavedPost = b.publicacion_guardada.FirstOrDefault(p => p.id_user == idUser) == default(publicacion_guardada) ? false : true


                    }
                )
                .OrderByDescending(o => o.publicacion.fecha_creacion)
                .ToList();


            return Request.CreateResponse(HttpStatusCode.OK, varPublicaciones);

        }


        [Route("feed/{idUser}")]
        public HttpResponseMessage GetPublicacionesFeed(int idUser)//id del usuario
        {
            if (!ValidandoSiExistenDatosRelacionados.ExistsUser(idUser))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe tal usuario, id: {idUser}");
            }

            var varPublicaciones = db.seguidores
                .Include(o => o.users1)
                .Include(o => o.users1.publicaciones)
                .Where(u => u.seguidor_id == idUser) //Yo y de ahi me voy a mis seguidos
                .Select(
                seg =>

                seg.users1.publicaciones
                .Where(u => u.users.isDeleted == false && u.isDeleted == false)
                .Select(b =>
                    new ResponsePublicacionFeed
                    {
                        //Corregir aca poque debe de ser una lista al final
                        publicacion = new publicacionesModel
                        {
                            descripcion = b.descripcion,
                            fecha_actualizacion = b.fecha_actualizacion,
                            fecha_creacion = b.fecha_creacion,
                            id_post = b.id_post,
                            id_user = b.id_user,
                            img = b.img,
                            isImg = b.isImg,
                            isDeleted = b.isDeleted

                        },

                        UsuarioCreator = new DLL.Models.userModel()
                        {
                            apellido = b.users.apellido,
                            descripcion = b.users.descripcion,
                            email = b.users.email,
                            id_user = b.users.id_user,
                            isVendor = b.users.isVendor,
                            nombre = b.users.nombre,
                            pass = b.users.pass,
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
                        cantComentarios = b.comentarios.Where(c => c.users.isDeleted == false && c.isDeleted == false).ToList().Count,
                        cantLikes = b.like_post.Where(l => l.users.isDeleted == false).ToList().Count,
                        IsLiked = b.like_post.FirstOrDefault(l => l.users.id_user == idUser) == default(like_post) ? false : true,
                        IsSavedPost = b.publicacion_guardada.FirstOrDefault(p => p.id_user == idUser) == default(publicacion_guardada) ? false : true


                    })
                .OrderByDescending(p => p.publicacion.fecha_creacion)
                .ToList()

                ).ToList();

            List<ResponsePublicacionFeed> publicacionesOrdenadas = new List<ResponsePublicacionFeed>();

            foreach (var publicacionesUsuario in varPublicaciones)
            {
                foreach (var publicacion in publicacionesUsuario)
                {
                    publicacionesOrdenadas.Add(publicacion);
                }
            }

            publicacionesOrdenadas.OrderByDescending(p => p.publicacion.fecha_creacion);


            return Request.CreateResponse(HttpStatusCode.OK, publicacionesOrdenadas);

        }



        [Route("{idPost}/user/{idUser}")]
        public HttpResponseMessage Getpublicacion(int idPost, int idUser)//id del usuario
        {
            if (!ValidandoSiExistenDatosRelacionados.ExistsPublicacion(idPost))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe tal publicación, id: {idPost}");
            }
            if (!ValidandoSiExistenDatosRelacionados.ExistsUser(idUser))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe tal usuario, id: {idUser}");
            }



            var publicacionAllData = db.publicaciones
                .Where(o => o.id_post == idPost)
                .Include(o => o.users)
                .Include(o => o.like_post)
                .Include(o => o.comentarios)
                .ToList();

            ResponsePublicacion publicacion = new ResponsePublicacion();

            ResponsePublicacionFeed publicacionContent = publicacionAllData
                   .Select(b =>
                       new ResponsePublicacionFeed()
                       {
                           publicacion = new publicacionesModel
                           {
                               descripcion = b.descripcion,
                               fecha_actualizacion = b.fecha_actualizacion,
                               fecha_creacion = b.fecha_creacion,
                               id_post = b.id_post,
                               id_user = b.id_user,
                               img = b.img,
                               isImg = b.isImg,
                               isDeleted = b.isDeleted
                           },
                           UsuarioCreator = new userModel()
                           {
                               apellido = b.users.apellido,
                               descripcion = b.users.descripcion,
                               email = b.users.email,
                               id_user = b.users.id_user,
                               isVendor = b.users.isVendor,
                               nombre = b.users.nombre,
                               pass = b.users.pass,
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
                           cantLikes = b.like_post.Where(l => l.users.isDeleted == false).ToList().Count,
                           cantComentarios = b.comentarios.Where(c => c.isDeleted == false && c.users.isDeleted == false).ToList().Count,
                           IsLiked = b.like_post.FirstOrDefault(l => l.users.id_user == idUser) == default(like_post) ? false:true,
                           IsSavedPost = b.publicacion_guardada.FirstOrDefault(l => l.users.id_user == idUser) == default(publicacion_guardada) ? false : true
                       }


                   ).ToList().First();


            List<ResponseComentarios> comentariosPost = db.comentarios
                .Where(c=>c.id_post==idPost)
                .Include(o => o.users)
                .Include(o => o.like_comentario)
                        .Select(c =>
                                   new ResponseComentarios()
                                   { 
                                       CantLikes = c.like_comentario.Where(l => l.users.isDeleted == false).ToList().Count,
                                       isLiked = c.like_comentario.FirstOrDefault(l => l.id_user == idUser) == default(like_comentario) ? false : true,

                                       comentario = new comentariosModel()
                                       {
                                           contenido = c.contenido,
                                           fecha_creacion = c.fecha_creacion,
                                           id_comentario = c.id_comentario,
                                           id_post = c.id_post,
                                           id_user = c.id_user,
                                           isDeleted = c.isDeleted

                                       },
                                       usuario = new userModel()
                                       {
                                           apellido = c.users.apellido,
                                           img = c.users.img,
                                           isActive = c.users.isActive,
                                           email = c.users.email,
                                           dui = c.users.dui,
                                           descripcion = c.users.descripcion,
                                           id_user = c.users.id_user,
                                           isVendor = c.users.isVendor,
                                           nombre = c.users.nombre,
                                           pass = c.users.pass,
                                           puntaje = c.users.puntaje,
                                           username = c.users.username,
                                           puesto_name = c.users.puesto_name,
                                           isDeleted = c.users.isDeleted,
                                           num_telefono = c.users.num_telefono,
                                           xlat = c.users.xlat,
                                           ylon = c.users.ylon
                                       }
                                   }
                                ).ToList();

            publicacion.publicacion = publicacionContent;
            publicacion.comentarios = comentariosPost;


            //if (lpublicaciones == null || lpublicaciones == default(ResponsePublicacion))
            //{
            //    return Request.CreateResponse(HttpStatusCode.NoContent, "No hay publicaciones");
            //}
            return Request.CreateResponse(HttpStatusCode.OK, publicacion);

        }



        //[Route("all/{idUser}")]
        //public HttpResponseMessage GetpublicacionesALL(int idUser)//id del usuario
        //{

        //    var lpublicaciones = db.publicaciones
        //        .Include(o => o.users)
        //        .Include(o => o.like_post)
        //        .Include(o => o.comentarios)
        //        .Select(b =>
        //             new ResponsePublicacionFeed()
        //             {
        //                 publicacion = new publicacionesModel
        //                 {
        //                     descripcion = b.descripcion,
        //                     fecha_actualizacion = b.fecha_actualizacion,
        //                     fecha_creacion = b.fecha_creacion,
        //                     id_post = b.id_post,
        //                     id_user = b.id_user,
        //                     img = b.img,
        //                     isImg = b.isImg,
        //                     isDeleted = b.isDeleted
        //                 },
        //                 UsuarioCreator = new DLL.Models.userModel()
        //                 {
        //                     apellido = b.users.apellido,
        //                     descripcion = b.users.descripcion,
        //                     email = b.users.email,
        //                     id_user = b.users.id_user,
        //                     isVendor = b.users.isVendor,
        //                     nombre = b.users.nombre,
        //                     pass = "Password",
        //                     puntaje = b.users.puntaje,
        //                     username = b.users.username,
        //                     img = b.users.img,
        //                     puesto_name = b.users.puesto_name,
        //                     isActive = b.users.isActive,
        //                     dui = b.users.dui,
        //                     isDeleted = b.users.isDeleted,
        //                     num_telefono = b.users.num_telefono,
        //                     xlat = b.users.xlat,
        //                     ylon = b.users.ylon

        //                 },
        //                 cantComentarios = b.comentarios.Where(c => c.users.isDeleted == false && c.isDeleted == false).ToList().Count,
        //                 cantLikes = b.like_post.Where(l => l.users.isDeleted == false).ToList().Count,
        //                 IsLiked = b.like_post.FirstOrDefault(l => l.users.id_user == idUser) == default(like_post) ? false : true,
        //                 IsSavedPost = b.publicacion_guardada.FirstOrDefault(p=>p.id_user==idUser) ==default(publicacion_guardada) ?false:true


        //             }).ToList();


        //    if (lpublicaciones == null || lpublicaciones.Count == 0)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NoContent, "No hay publicaciones");
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK, lpublicaciones);

        //}


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
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe tal publicación, id: {id}");
            }
            item.isDeleted = true;
            if (r.Put(id, item))
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"publicación {id} fue eliminado correctamente");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"No eliminado, publicación {id}");
        }


        /// <summary>
        /// Recibe un objeto como paramentro y posteriormente intenta almacenarlo en la base de datos
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(publicaciones item)
        {
            item.isDeleted = false;
            if (item == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"La publicación no puede estar sin datos");
            }
            //valido si existe el user que publicó
            if (item.id_user == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se recibio ID del usuario");
            }
            if (!ValidandoSiExistenDatosRelacionados.ExistsUser(item.id_user))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No existe el usuario");
            }

            item.fecha_creacion = DateTime.Now;
            item.fecha_actualizacion = null;
            item.isDeleted = false;
            //Intenta el post
            if (r.Post(item))
            {
                return Request.CreateResponse(HttpStatusCode.Created, "publicación guardado correctamente");
            }


            //Culpa del server xd
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "No es posible guardar los datos de la publicación");
        }



        /// <summary>
        /// Verifica si existe el id ingresado en la tabla y luego actualiza el registro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(int id, publicaciones item)
        {
            var data = r.GetById(id);
            if (data == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe en la base de datos la publicación a actualizar");
            }
            if (r.Put(id, item))
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Datos modificados para la publicación {id}");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"No fue posible actualizar la publicación, id: {id}");
        }
        #endregion

    }
}
