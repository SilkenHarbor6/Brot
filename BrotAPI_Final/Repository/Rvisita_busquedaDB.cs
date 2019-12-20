using BrotAPI_Final.Models;
using System;

namespace BrotAPI_Final.Repository
{
    public class Rvisita_busquedaDB : IRepositoryDB<visita_busqueda>
    {


        /// <summary>
        /// FILTRO DE la base de datos el objeto que tiene el mismo id que el que busco
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public visita_busqueda GetById(int id)
        {
            using (var db = new SomeeDBBrotEntities())
            {
                try
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    return db.visita_busqueda.Find(id);
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
        public bool Post(visita_busqueda item)
        {
            using (var db = new SomeeDBBrotEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                try
                {
                    db.visita_busqueda.Add(item);
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
        public bool Put(int id, visita_busqueda item)
        {
            using (var db = new SomeeDBBrotEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                visita_busqueda dbitem = db.visita_busqueda.Find(id);
                if (dbitem == null)
                {
                    return false;
                }
                //Intercambio los atributos del objeto viejo con los del nuevo}
                dbitem.id_userquebusco = item.id_userquebusco;
                dbitem.id_perfilvisitado = item.id_perfilvisitado;
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
                    var item = db.visita_busqueda.Find(id);
                    if (item == null)
                    {
                        return false;
                    }
                    db.visita_busqueda.Remove(item);
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