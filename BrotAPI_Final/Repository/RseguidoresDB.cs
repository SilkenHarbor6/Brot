using BrotAPI_Final.Models;
using System;

namespace BrotAPI_Final.Repository
{
    public class RseguidoresDB : IRepositoryDB<seguidores>
    {


        /// <summary>
        /// FILTRO DE la base de datos el objeto que tiene el mismo id que el que busco
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public seguidores GetById(int id)
        {
            using (var db = new SomeeDBBrotEntities())
            {
                try
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    return db.seguidores.Find(id);
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
        public bool Post(seguidores item)
        {
            using (var db = new SomeeDBBrotEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                try
                {
                    db.seguidores.Add(item);
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
        /// Metodo que verifica si es posible actualizar datos
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Put(int id, seguidores item)
        {
            using (var db = new SomeeDBBrotEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                seguidores dbitem = db.seguidores.Find(id);
                if (dbitem == null)
                {
                    return false;
                }
                //Intercambio los atributos del objeto viejo con los del nuevo}
                dbitem.seguidor_id = item.seguidor_id;
                dbitem.id_seguido = item.id_seguido;
                dbitem.accepted = item.accepted;
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
            using (var db = new SomeeDBBrotEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                try
                {
                    var item = db.seguidores.Find(id);
                    if (item == null)
                    {
                        return false;
                    }
                    db.seguidores.Remove(item);
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