using BrotAPI_Final.Models;
using System;
using System.Diagnostics;

namespace BrotAPI_Final.Repository
{
    public class RusersDB : IRepositoryDB<users>
    {

        /// <summary>
        /// FILTRO DE la base de datos el objeto que tiene el mismo id que el que busco
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public users GetById(int id)
        {
            using (var db = new SomeeDBBrotEntities())
            {
                try
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    return db.users.Find(id);
                }
                catch (Exception) { return null; }
            }
        }


        /// <summary>
        /// Intento guardar en la base de datos y luego retorno true si se logra guardar
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Post(users item)
        {
            using (var db = new SomeeDBBrotEntities())
            {
                try
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.users.Add(item);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    
                    Debug.Print(e.Message);
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
        public bool Put(int id, users item)
        {
            using (var db = new SomeeDBBrotEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                users dbitem = db.users.Find(id);
                if (dbitem == null)
                {
                    return false;
                }
                //Intercambio los atributos del objeto viejo con los del nuevo}

                dbitem.apellido = item.apellido;
                dbitem.descripcion = item.descripcion;
                dbitem.dui = item.dui;
                dbitem.email = item.email;
                dbitem.img = item.img;
                dbitem.isActive = item.isActive;
                dbitem.isDeleted = item.isDeleted;
                dbitem.isVendor = item.isVendor;
                dbitem.nombre = item.nombre;
                dbitem.num_telefono = item.num_telefono;
                dbitem.pass = item.pass;
                dbitem.puesto_name = item.puesto_name;
                dbitem.puntaje = item.puntaje;
                dbitem.username = item.username;
                dbitem.xlat = item.xlat;
                dbitem.ylon = item.ylon;

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
                    var item = db.users.Find(id);
                    if (item == null)
                    {
                        return false;
                    }
                    db.users.Remove(item);
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