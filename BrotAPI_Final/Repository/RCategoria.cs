using BrotAPI_Final.Models;
using System;
using System.Collections.Generic;
using BrotAPI_Final.Models;
using System.Linq;
using System.Web;

namespace BrotAPI_Final.Repository
{
    public class RCategoria : ICategoria
    {
        private SomeeDBBrotEntities db= new SomeeDBBrotEntities();
        public categoria ActualizarCategoria(categoria item)
        {
            var obj = db.categoria.Find(item.id_categoria);
            if (obj==null)
            {
                return null; 
            }
            obj.img = item.img;
            obj.nombre = item.nombre;
            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return obj;
        }

        public categoria AgregarCategoria(categoria item)
        {
            if (item==null)
            {
                return null;
            }
            db.categoria.Add(item);
            db.SaveChanges();
            return item;
        }

        public bool EliminarCategoria(categoria item)
        {
            var obj = db.categoria.Find(item.id_categoria);
            if (obj==null)
            {
                return false;
            }
            db.categoria.Remove(obj);
            db.SaveChanges();
            return true;
        }

        public IEnumerable<categoria> GetAll()
        {
            return db.categoria.ToList();
        }
    }
}