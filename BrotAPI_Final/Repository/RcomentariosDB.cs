﻿using BrotAPI_Final.Models;
using System;

namespace BrotAPI_Final.Repository
{
    public class RcomentariosDB : IRepositoryDB<comentarios>
    {
        /// <summary>
        /// FILTRO DE la base de datos el objeto que tiene el mismo id que el que busco
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public comentarios GetById(int id)
        {
            using (var db = new DBContextModel())
            {
                try
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    return db.comentarios.Find(id);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Intento guardar en la base de datos y luego retorno true si se logra guardar
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Post(comentarios item)
        {
            using (var db = new DBContextModel())
            {
                db.Configuration.ProxyCreationEnabled = false;
                try
                {
                    db.comentarios.Add(item);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }

            }
        }


        /// <summary>
        /// Metodo que verifica si es posible actualizar datos
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Put(int id, comentarios item)
        {
            using (var db = new DBContextModel())
            {
                db.Configuration.ProxyCreationEnabled = false;
                comentarios dbitem = db.comentarios.Find(id);
                if (dbitem == null)
                {
                    return false;
                }
                //Intercambio los atributos del objeto viejo con los del nuevo}
                dbitem.id_user = item.id_user;
                dbitem.id_post = item.id_post;
                dbitem.contenido = item.contenido;
                dbitem.fecha_creacion = item.fecha_creacion;
                dbitem.isDeleted = item.isDeleted;


                //guardo cambios
                try
                {
                    db.Entry(dbitem).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }



        /// <summary>
        /// Compara el ID ingresado y en base a eso retorna true si logra borrarlo de la base de datos y guardar cambios
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (var db = new DBContextModel())
            {
                db.Configuration.ProxyCreationEnabled = false;
                try
                {
                    var item = db.comentarios.Find(id);
                    if (item == null)
                    {
                        return false;
                    }
                    db.comentarios.Remove(item);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}