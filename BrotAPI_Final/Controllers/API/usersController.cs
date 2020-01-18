using BrotAPI_Final.Models;
using BrotAPI_Final.Repository;
using DLL.Models;
using DLL.ResponseModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BrotAPI_Final.Controllers.API
{
    [RoutePrefix("api/users")]
    public class usersController : ApiController
    {
        private RusersDB r = new RusersDB();

        #region Gets

        [HttpGet]
        [Route("UsernameDisponible/{username}")]
        public HttpResponseMessage validandoUsername(string username)
        {
            using (var db = new SomeeDBBrotEntities())
            {
                var datos = db.users.Where(u => u.username == username).ToList();
                if (datos.Count > 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Ya existe ese nombre nombre de usuario");
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Nombre de usuario disponible");
            }
        }

        //Login
        [HttpPost]  //Retorno el usuario completop para guardarlo en la base de datos
        [Route("login")]
        public HttpResponseMessage login(users item)
        {
            //Retorno objeto con id
            if (item == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No es posible analizar los datos proporcionados");
            }
            try
            {
                using (var db = new SomeeDBBrotEntities())
                {
                    var userObtenido = db.users.Where(u => u.username == item.username || u.email == item.username).ToList();
                    if (userObtenido.Count <= 0)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se encontro el usuario o correo proporcionado");
                    }
                    var usuarioDB = userObtenido.First();
                    if (usuarioDB.pass == item.pass)
                    {
                        usuarioDB.isDeleted = false;
                        db.Entry(usuarioDB).State = EntityState.Modified;
                        db.SaveChanges();

                        userModel usuario = new userModel()
                        {
                            apellido = usuarioDB.apellido,
                            descripcion = usuarioDB.descripcion,
                            email = usuarioDB.email,
                            id_user = usuarioDB.id_user,
                            isVendor = usuarioDB.isVendor,
                            nombre = usuarioDB.nombre,
                            pass = "pass",
                            puntaje = usuarioDB.puntaje,
                            username = usuarioDB.username,
                            img = usuarioDB.img,
                            puesto_name = usuarioDB.puesto_name,
                            isActive = usuarioDB.isActive,
                            dui = usuarioDB.dui,
                            isDeleted = usuarioDB.isDeleted,
                            num_telefono = usuarioDB.num_telefono,
                            xlat = usuarioDB.xlat,
                            ylon = usuarioDB.ylon
                        };
                        return Request.CreateResponse(HttpStatusCode.OK, usuario);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contraseña incorrecta");
                    }

                }

            }
            catch (Exception) { }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No es posible comparar los datos proporcionados");
        }



        //Vendors obtener vendedores
        [HttpGet]
        [Route("vendors")]
        public HttpResponseMessage vendors()
        {
            using (var db = new SomeeDBBrotEntities())
            {
                var vendedores = db.users
                    .Where(u => u.isVendor)
                    .Select(u =>
                        new userModel
                        {
                            apellido = u.apellido,
                            descripcion = u.descripcion,
                            email = u.email,
                            id_user = u.id_user,
                            isVendor = u.isVendor,
                            nombre = u.nombre,
                            pass = "pass",
                            puntaje = u.puntaje,
                            username = u.username,
                            img = u.img,
                            puesto_name = u.puesto_name,
                            isActive = u.isActive,
                            dui = u.dui,
                            isDeleted = u.isDeleted,
                            num_telefono = u.num_telefono,
                            xlat = u.xlat,
                            ylon = u.ylon
                        }
                    ).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, vendedores);
            }

        }



        //Brot Ten
        [HttpGet]
        [Route("brotten")]
        // TODO BrotTen Filtrado
        public HttpResponseMessage brotten()
        {
            using (var db = new SomeeDBBrotEntities())
            {
                var varUserProfile = db.users
               .Include(s => s.seguidores)
               .Include(s => s.seguidores1)
               .Where(u => u.isVendor == true)
               .Select(
                   u =>
                   new ResponseUsuariosFiltro
                   {

                       userData = new userModel
                       {
                           apellido = u.apellido,
                           descripcion = u.descripcion,
                           email = u.email,
                           id_user = u.id_user,
                           isVendor = u.isVendor,
                           nombre = u.nombre,
                           pass = "pass",
                           puntaje = u.puntaje,
                           username = u.username,
                           img = u.img,
                           puesto_name = u.puesto_name,
                           isActive = u.isActive,
                           dui = u.dui,
                           isDeleted = u.isDeleted,
                           num_telefono = u.num_telefono,
                           xlat = u.xlat,
                           ylon = u.ylon
                       },

                       Cantidad = u.seguidores.Count

                   }
               ).OrderBy(u => u.Cantidad).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, varUserProfile);
            }
        }



        // TODO MostLiked Profiles
        //[HttpGet]
        //[Route("mostliked")]
        //public HttpResponseMessage mostliked()
        //{
        //    //Basarme en BrotTen

        //        return Request.CreateResponse(HttpStatusCode.OK, varUserProfile);
        //}



        ////Vendores mas vistos MostViewed
        //[HttpGet]
        //[Route("mostviewed")]
        //public HttpResponseMessage mostviewed()
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, "Res");
        //}


        //Perfil de otro  cliente o de vendedor
        [HttpGet]
        [Route("{idUserProfile}/profile/{idVisitante}")]
        public HttpResponseMessage OtherProfile(int idUserProfile, int idVisitante)
        {
            if (!ValidandoSiExistenDatosRelacionados.ExistsUser(idUserProfile))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe tal usuario, id: {idUserProfile}");
            }
            if (!ValidandoSiExistenDatosRelacionados.ExistsUser(idVisitante))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe tal usuario, id: {idVisitante}");
            }

            using (var db = new SomeeDBBrotEntities())
            {
                var varUserProfile = db.users
               .Include(s => s.publicaciones)
               .Include(s => s.like_post)
               .Include(s => s.seguidores)
               .Include(s => s.seguidores1)
               .Include(s => s.publicacion_guardada)
               .Where(u => u.id_user == idUserProfile)
               .Select(
                   u =>
                   new ResponseUserProfile
                   {
                       isFollowed = u.seguidores1.FirstOrDefault(ss => ss.seguidor_id == idVisitante) == default(seguidores) ? false : true,

                       cantSeguidores = u.seguidores1
                           .Where(s => s.id_seguido == idUserProfile && s.users.isDeleted == false).ToList().Count,
                       cantSeguidos = u.seguidores
                           .Where(s => s.seguidor_id == idUserProfile && s.users1.isDeleted == false).ToList().Count,

                       UserProfile = new userModel
                       {
                           apellido = u.apellido,
                           descripcion = u.descripcion,
                           email = u.email,
                           id_user = u.id_user,
                           isVendor = u.isVendor,
                           nombre = u.nombre,
                           pass = "pass",
                           puntaje = u.puntaje,
                           username = u.username,
                           img = u.img,
                           puesto_name = u.puesto_name,
                           isActive = u.isActive,
                           dui = u.dui,
                           isDeleted = u.isDeleted,
                           num_telefono = u.num_telefono,
                           xlat = u.xlat,
                           ylon = u.ylon
                       },

                       publicacionesUser = u.publicaciones
                       .Where(pu => pu.isDeleted == false)
                       .Select(
                           b =>
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

                                   cantComentarios = b.comentarios.Where(c => c.users.isDeleted == false && c.isDeleted == false).ToList().Count,
                                   cantLikes = b.like_post.Where(l => l.users.isDeleted == false).ToList().Count,
                                   IsLiked = b.like_post.FirstOrDefault(l => l.id_user == idVisitante) == default(like_post) ? false : true,
                                   IsSavedPost = b.publicacion_guardada.FirstOrDefault(p => p.id_user == idVisitante) == default(publicacion_guardada) ? false : true

                               }
                           ).OrderByDescending(f => f.publicacion.fecha_creacion).ToList(),


                   }


               ).ToList().FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, varUserProfile);
            }
        }



        [HttpGet]
        [Route("{idUser}/profile")]
        public HttpResponseMessage userprofile(int idUser)//Cuando un usuario ve su propio perfil
        {
            if (!ValidandoSiExistenDatosRelacionados.ExistsUser(idUser))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe tal usuario, id: {idUser}");
            }
            using (var db = new SomeeDBBrotEntities())
            {
                var varUserProfile = db.users
               .Include(s => s.publicaciones)
               .Include(s => s.like_post)
               .Include(s => s.seguidores)
               .Include(s => s.seguidores1)
               .Include(s => s.publicacion_guardada)
               .Where(u => u.id_user == idUser)
               .Select(
                   u =>
                   new ResponseUserProfile
                   {
                       isFollowed = true,

                       cantSeguidores = u.seguidores1
                           .Where(s => s.id_seguido == idUser && s.users.isDeleted == false).ToList().Count,
                       cantSeguidos = u.seguidores
                           .Where(s => s.seguidor_id == idUser && s.users1.isDeleted == false).ToList().Count,

                       UserProfile = new userModel
                       {
                           apellido = u.apellido,
                           descripcion = u.descripcion,
                           email = u.email,
                           id_user = u.id_user,
                           isVendor = u.isVendor,
                           nombre = u.nombre,
                           pass = "pass",
                           puntaje = u.puntaje,
                           username = u.username,
                           img = u.img,
                           puesto_name = u.puesto_name,
                           isActive = u.isActive,
                           dui = u.dui,
                           isDeleted = u.isDeleted,
                           num_telefono = u.num_telefono,
                           xlat = u.xlat,
                           ylon = u.ylon
                       },







                   }


               ).ToList().FirstOrDefault();


                varUserProfile.publicacionesUser = db.publicaciones
                       .Where(pu => pu.isDeleted == false && pu.users.isDeleted == false && pu.id_user == idUser)
                       .Select(
                           b =>
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

                                   //No lo mando porque ya lo tengo en un JSON y Singleton Localmente

                                   //UsuarioCreator = new userModel()
                                   //{
                                   //    apellido = b.users.apellido,
                                   //    descripcion = b.users.descripcion,
                                   //    email = b.users.email,
                                   //    id_user = b.users.id_user,
                                   //    isVendor = b.users.isVendor,
                                   //    nombre = b.users.nombre,
                                   //    pass = "Password",
                                   //    puntaje = b.users.puntaje,
                                   //    username = b.users.username,
                                   //    img = b.users.img,
                                   //    puesto_name = b.users.puesto_name,
                                   //    isActive = b.users.isActive,
                                   //    dui = b.users.dui,
                                   //    isDeleted = b.users.isDeleted,
                                   //    num_telefono = b.users.num_telefono,
                                   //    xlat = b.users.xlat,
                                   //    ylon = b.users.ylon
                                   //},

                                   cantComentarios = b.comentarios.Where(c => c.users.isDeleted == false && c.isDeleted == false).ToList().Count,
                                   cantLikes = b.like_post.Where(l => l.users.isDeleted == false).ToList().Count,
                                   IsLiked = b.like_post.FirstOrDefault(l => l.id_user == idUser) == default(like_post) ? false : true,
                                   IsSavedPost = b.publicacion_guardada.FirstOrDefault(p => p.id_user == idUser) == default(publicacion_guardada) ? false : true

                               }
                           ).OrderByDescending(f=>f.publicacion.fecha_creacion).ToList();

                varUserProfile.publicacionesGuardadas = db.publicacion_guardada
                    .Where(postSaved => postSaved.id_user == idUser && postSaved.publicaciones.isDeleted == false && postSaved.users.isDeleted == false)
                    .OrderByDescending(f=>f.fecha)
                    .Select(
                           b =>
                     new ResponsePublicacionFeed
                     {

                         publicacion = new publicacionesModel
                         {
                             descripcion = b.publicaciones.descripcion,
                             fecha_actualizacion = b.publicaciones.fecha_actualizacion,
                             fecha_creacion = b.publicaciones.fecha_creacion,
                             id_post = b.publicaciones.id_post,
                             id_user = b.publicaciones.id_user,
                             img = b.publicaciones.img,
                             isImg = b.publicaciones.isImg,
                             isDeleted = b.publicaciones.isDeleted

                         },
                         UsuarioCreator = new userModel()
                         {
                             apellido = b.publicaciones.users.apellido,
                             descripcion = b.publicaciones.users.descripcion,
                             email = b.publicaciones.users.email,
                             id_user = b.publicaciones.users.id_user,
                             isVendor = b.publicaciones.users.isVendor,
                             nombre = b.publicaciones.users.nombre,
                             pass = "pass",
                             puntaje = b.publicaciones.users.puntaje,
                             username = b.publicaciones.users.username,
                             img = b.publicaciones.users.img,
                             puesto_name = b.publicaciones.users.puesto_name,
                             isActive = b.publicaciones.users.isActive,
                             dui = b.publicaciones.users.dui,
                             isDeleted = b.publicaciones.users.isDeleted,
                             num_telefono = b.publicaciones.users.num_telefono,
                             xlat = b.publicaciones.users.xlat,
                             ylon = b.publicaciones.users.ylon
                         },

                         IsSavedPost = true,
                         cantComentarios = b.publicaciones.comentarios.Where(c => c.users.isDeleted == false && c.isDeleted == false).ToList().Count,
                         cantLikes = b.publicaciones.like_post.Where(l => l.users.isDeleted == false).ToList().Count,
                         IsLiked = b.publicaciones.like_post.FirstOrDefault(l => l.id_user == idUser) == default(like_post) ? false : true
                     }
                    ).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, varUserProfile);
            }

        }


        //Seguidores por usuario
        [HttpGet]
        [Route("{idUser}/seguidores")]
        public HttpResponseMessage getSeguidores(int idUser)//idUser es el seguido
        {
            if (!ValidandoSiExistenDatosRelacionados.ExistsUser(idUser))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe tal usuario, id: {idUser}");
            }
            using (var db = new SomeeDBBrotEntities())
            {
                List<userModel> usuarios = db.seguidores.Where(o => o.id_seguido == idUser)
                   .Include(o => o.users).Select(
                        b => new userModel
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
                    ).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, usuarios);
            }

        }


        //Seguidos por usuario
        [HttpGet]
        [Route("{idUser}/seguidos")]
        public HttpResponseMessage getSeguidos(int idUser)//idUser es el seguidor
        {
            if (!ValidandoSiExistenDatosRelacionados.ExistsUser(idUser))
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe tal usuario, id: {idUser}");
            }
            using (var db = new SomeeDBBrotEntities())
            {
                List<userModel> usuarios = db.seguidores
                    .Include(o => o.users1)
                    .Where(o => o.seguidor_id == idUser)
                    .Select(
                        b => new userModel
                        {
                            apellido = b.users1.apellido,
                            descripcion = b.users1.descripcion,
                            email = b.users1.email,
                            id_user = b.users1.id_user,
                            isVendor = b.users1.isVendor,
                            nombre = b.users1.nombre,
                            pass = "pass",
                            puntaje = b.users1.puntaje,
                            username = b.users1.username,
                            img = b.users1.img,
                            puesto_name = b.users1.puesto_name,
                            isActive = b.users1.isActive,
                            dui = b.users1.dui,
                            isDeleted = b.users1.isDeleted,
                            num_telefono = b.users1.num_telefono,
                            xlat = b.users1.xlat,
                            ylon = b.users1.ylon
                        }
                    ).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, usuarios);
            }
        }

        #endregion




        #region DELETE - POST - PUT 



        /// <summary>
        /// Optiene un id y ese es pasado al repositorio para ver si puede eliminar el objeto en la base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var item = r.GetById(id);
            if (item == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe tal user, id: {id}");
            }

            item.isDeleted = true;
            if (r.Put(id, item))
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"user {id} fue eliminado correctamente, " +
                    $"recuerde que puede volver a activar su usuario si vuelve a iniciar sesión");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"No eliminado, user {id}");
        }


        /// <summary>
        /// Recibe un objeto como paramentro y posteriormente intenta almacenarlo en la base de datos
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Post(users item)
        {
            if (item == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"El user no puede estar sin datos");
            }

            item.isDeleted = false;
            if (!item.isVendor)
            {
                item.descripcion = "Soy un nuevo BrotAmigo";
                item.puesto_name = "BrotAmigo";
            }
            if (r.Post(item))
            {
                return Request.CreateResponse(HttpStatusCode.Created, "user guardado correctamente");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No es posible guardar los datos del user");
        }



        /// <summary>
        /// Verifica si existe el id ingresado en la tabla y luego actualiza el registro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut]
        public HttpResponseMessage Put(int id, users item)
        {
            var data = r.GetById(id);
            if (data == null || id != item.id_user)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe en la base de datos sobre el user a actualizar");
            }
            if (r.Put(id, item))
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"Datos modificados para el user {id}");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"No fue posible actualizar el user, id: {id}");
        }


        [HttpPut]
        [Route("pass/{oldPassWord}")]
        public HttpResponseMessage ChangePassWord(string oldPassWord, users itemNew)
        {

            var data = r.GetById(itemNew.id_user);
            if (data == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe en la base de datos sobre el user a actualizar");
            }
            try
            {
                if (data.pass == oldPassWord)
                {

                    data.pass = itemNew.pass;
                    using (var db = new SomeeDBBrotEntities())
                    {
                        db.Entry(data).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, $"Contraseña cambiada");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotModified, $"Contraseña incorrecta");
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"No fue posible actualizar la contraseña");
        }




        [Route("photo/{id}")]
        [HttpPut]
        public HttpResponseMessage ChangePhoto(int id, users itemNew)
        {

            var data = r.GetById(itemNew.id_user);
            if (data == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"No existe en la base de datos sobre el usuario a actualizar");
            }
            try
            {

                data.img = itemNew.img;
                using (var db = new SomeeDBBrotEntities())
                {
                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, $"Foto cambiada");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, $"No fue posible actualizar la contraseña");
        }
        #endregion




    }
}
