using BrotAPI_Final.Models;
using System;

namespace BrotAPI_Final.Repository
{
    public class Rpublicacion_guardadaDB : IRepositoryDB<publicacion_guardada>
    {


        /// <summary>
        /// FILTRO DE la base de datos el objeto que tiene el mismo id que el que busco
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public publicacion_guardada GetById(int id)
        {
            using (var db = new DBContextModel())
            {
                try
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    return db.publicacion_guardada.Find(id);
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
        public bool Post(publicacion_guardada item)
        {
            using (var db = new DBContextModel())
            {
                db.Configuration.ProxyCreationEnabled = false;
                try
                {
                    db.publicacion_guardada.Add(item);
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
        public bool Put(int id, publicacion_guardada item)
        {
            using (var db = new DBContextModel())
            {
                db.Configuration.ProxyCreationEnabled = false;
                publicacion_guardada dbitem = db.publicacion_guardada.Find(id);
                if (dbitem == null)
                {
                    return false;
                }
                //Intercambio los atributos del objeto viejo con los del nuevo}
                dbitem.id_user = item.id_user;
                dbitem.id_post = item.id_post;
                dbitem.fecha = item.fecha;

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
                    var item = db.publicacion_guardada.Find(id);
                    if (item == null)
                    {
                        return false;
                    }
                    db.publicacion_guardada.Remove(item);
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