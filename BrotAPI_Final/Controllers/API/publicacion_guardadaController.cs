﻿using BrotAPI_Final.Models;
using BrotAPI_Final.Repository;
using DLL.ResponseModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace BrotAPI_Final.Controllers.API
{
    public class publicacion_guardadaController : ApiController
    {
        private SomeeDBBrotEntities db = new SomeeDBBrotEntities();



        private Rpublicacion_guardadaDB r = new Rpublicacion_guardadaDB();


        [HttpGet]
        [Route("api/publicacion_guardada/{idUser}")]
        public HttpResponseMessage GetPostsSaved(int idUser)
        {
            using (var db = new SomeeDBBrotEntities())
            {
                var publicacionesGuardadas = db.publicacion_guardada
                    .Include(p => p.users)
                    .Include(p => p.publicaciones)
                    .Include(p => p.publicaciones.like_post)
                    .Where(p => p.id_user == idUser)
                    .Select(
                        p =>
                            new ResponsePublicacionGuardada
                            {
                                publicacionGuardada = new DLL.Models.publicacion_guardadasModel
                                {
                                    fecha = p.fecha,
                                    id_post = p.id_post,
                                    id_user = p.id_user,
                                    //Primary Key
                                    id_publicacion_guardada = p.id_publicacion_guardada
                                },

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

                    ).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, publicacionesGuardadas);
            }
        }



        #region  POST - PUT - DELETE




        /// <summary>
        /// Recibe un objeto como paramentro y posteriormente intenta almacenarlo en la base de datos
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(publicacion_guardada item)
        {
            if (item == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"La publicacion_guardada no puede estar sin datos");
            }
            try
            {
                var likeDado = db.publicacion_guardada.SingleOrDefault(l => l.id_post == item.id_post && l.id_user == item.id_user);

                if (likeDado == default(publicacion_guardada))
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
        public HttpResponseMessage Put(int id, publicacion_guardada item)
        {

            ///TODO: Ris -  Falta aqui para que lo borre!
            var data = r.GetById(id);
            if (data == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe en la base de datos la publicacion_guardada a actualizar");
            }
            if (r.Put(id, item))
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Datos modificados para la publicacion_guardada {id}");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"No fue posible actualizar la publicacion_guardada, id: {id}");
        }
        #endregion



    }
}
