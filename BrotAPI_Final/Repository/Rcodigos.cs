using BrotAPI_Final.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace BrotAPI_Final.Repository
{
    public class RCodigos
    {
        public bool Post(codigos item)
        {
            using (var db = new DBContextModel())
            {
                try
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    db.codigos.Add(item);
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

        public codigos Auth(string code)
        {
            using (var db = new DBContextModel())
            {
                try
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    return db.codigos.Find(code);
                }
                catch (Exception ex) { Debug.Print(ex.Message); return null; }
            }
        }

        public bool Delete(string code)
        {
            using (var db = new DBContextModel())
            {
                var obj = db.codigos.Find(code);
                if (obj == null)
                {
                    return false;
                }
                db.codigos.Remove(obj);
                db.SaveChanges();
                return true;
            }
        }
    }
}