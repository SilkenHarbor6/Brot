using BrotAPI_Final.Models;
using System;

namespace BrotAPI_Final.Repository
{
    public class Rlike_comentarioDB : IRepositoryDB<like_comentario>
    {

        /// <summary>
        /// FILTRO DE la base de datos el objeto que tiene el mismo id que el que busco
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public like_comentario GetById(int id)
        {
            using (var db = new DBContextModel())
            {
                try
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    return db.like_comentario.Find(id);
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
        public bool Post(like_comentario item)
        {
            using (var db = new DBContextModel())
            {
                db.Configuration.ProxyCreationEnabled = false;
                try
                {
                    db.like_comentario.Add(item);
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
        public bool Put(int id, like_comentario item)
        {
            using (var db = new DBContextModel())
            {
                db.Configuration.ProxyCreationEnabled = false;
                like_comentario dbitem = db.like_comentario.Find(id);
                if (dbitem == null)
                {
                    return false;
                }
                //Intercambio los atributos del objeto viejo con los del nuevo}
                dbitem.id_user = item.id_user;
                dbitem.id_comentario = item.id_comentario;
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
                    var item = db.like_comentario.Find(id);
                    if (item == null)
                    {
                        return false;
                    }
                    db.like_comentario.Remove(item);
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