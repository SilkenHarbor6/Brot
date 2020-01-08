using BrotAPI_Final.Models;
using System;
using System.Collections.Generic;
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

        public IEnumerable<categoria> GetByUser(int id)
        {
            var lcategorias = db.usuario_categoria.ToList();
            var cat = from cate in lcategorias where cate.id_usuario.Equals(id) select cate;
            if (cat==null)
            {
                return null;
            }
            List<categoria> ca = new List<categoria>();
            foreach (var item in cat)
            {
                var i = db.categoria.Find(item.id_categoria);
                if (item.isPrimary.Equals(1))
                {
                    ca.Insert(0, i);
                }
                else
                {
                    ca.Add(i);
                }
            }
            return ca;
        }

        public RCategoria()
        {

        }
    }
}